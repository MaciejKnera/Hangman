using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hangman.Models
{
    public class Round
    {
        public string Word { get; set; }
        public string GuessedWord { get; set; }
        public char GuessedLetter { get; set; }
        public string[] WrongGuesses { get; set; }
        public int PlayersLifes { get; set; }
        public int NumberOfGuesses { get; set; }
        public TimeSpan RoundTime { get; set; }
    }
}
