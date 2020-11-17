using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Hangman.Models;
using Hangman.Data;
using Hangman.Utilities;

namespace Hangman.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            KeyValuePair<string, string> keyValuePair = new KeyValuePair<string, string>();
            keyValuePair = Randoms.GetRandomEntryFromDictAsync(await FileData.ConvertFileContentToDictAsync(FileData._countryCapitalFile));
            string country = keyValuePair.Key;

            Player player = new Player()
            {
                Name = "Maciej",
                Date = DateTime.Now,
                RoundTime = TimeSpan.FromSeconds(45),
                GuessedWord = "Warsaw",
                GuessingTries = 12
            };

            await FileData.WriteToFileAsync(player, FileData._scoreFile);
            var listOfScores = await FileData.ReturnTopScoresFromFileAsync(10);

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
