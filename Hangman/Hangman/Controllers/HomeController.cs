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
using System.Text;

namespace Hangman.Controllers
{
    public class HomeController : Controller
    {
        [BindProperty]
        public Round Round { get; set; }

        public async Task<IActionResult> Index()
        {

            KeyValuePair<string, string> countryCityPair = new KeyValuePair<string, string>();
            countryCityPair = Randoms.GetRandomEntryFromDictAsync(await FileData.ConvertFileContentToDictAsync(FileData._countryCapitalFile));

            Round = new Round
            {
                Capital = countryCityPair.Value.Trim().Replace(" ", "").ToLower(),
                Country = countryCityPair.Key.Trim(),
                CapitalPlaceholder = new String('_', countryCityPair.Value.Trim().Replace(" ", "").Length),
                //TODO : make placeholder more visible
                //CapitalPlaceholder = String.Concat(Enumerable.Repeat("_ ", countryCityPair.Value.Trim().Replace(" ", "").Length)).Trim()
            };

            return View(Round);
        }

        [HttpPost]
        public IActionResult GameEngine(Round round)
        {
            if (ModelState.IsValid == false)
            {
                return NotFound();
            }

            int inputLength = round.UserGuess.Length;

            if (inputLength == 1)
            {
                if (StringManipulations.ContainsCharacter(round.Capital, round.UserGuess))
                {
                    round.CapitalPlaceholder = StringManipulations.ReplaceCharactersInWord(round.Capital, round.CapitalPlaceholder, round.UserGuess);
                    if (round.CapitalPlaceholder == round.Capital)
                    {
                        round.RoundState = "won";                     
                    }
                }
                else
                {
                    round.WrongGuess = round.UserGuess;
                    round.PlayersLifes -= 1;                   
                }
            }
            else
            {
                if (round.UserGuess == round.Capital)
                {
                    round.RoundState = "won";
                }
                else
                {
                    round.WrongGuess = round.UserGuess;
                    round.PlayersLifes -= 2;
                }
            }

            if (round.PlayersLifes <= 0)
            {
                round.RoundState = "lost";
            }
            else if (round.PlayersLifes == 1)
            {
                round.Hint = $"The capital of {round.Country}";
            }

            round.UserGuess = null;
            round.NumberOfGuesses += 1;

            return Json(round);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
