using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangman.Data;
using Hangman.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hangman.Controllers
{
    [BindProperties]
    public class PlayerController : Controller
    {
        public PlayerController()
        {
            Players = new List<Player>();
        }

        public List<Player> Players { get; set; }
        public Player Player { get; set; }

        public async Task<IActionResult> Index()
        {
            Players = await FileData.ReturnTopScoresFromFileAsync(10);

            return View(Players);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Round round, string name)
        {
            Player Player = new Player()
            {
                Name = name,
                Date = DateTime.UtcNow,
                RoundTime = TimeSpan.FromSeconds(Convert.ToDouble(round.RoundTime, System.Globalization.CultureInfo.InvariantCulture)),
                GuessedWord = round.Capital,
                GuessingTries = round.NumberOfGuesses
            };          
                                  
            await FileData.WriteToFileAsync(Player, FileData._scoreFile);

            return Json(new {data = true });
        }
    }
}
