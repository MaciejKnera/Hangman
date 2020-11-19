using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hangman.Models
{
    public class Round
    {
        public string Capital { get; set; }
        public string Country { get; set; }
        public string CapitalPlaceholder { get; set; }
        public string UserGuess { get; set; }
        public string WrongGuess { get; set; }
        public int PlayersLifes { get; set; } = 5;
        public int NumberOfGuesses { get; set; }
        public string RoundTime { get; set; }
        public string Hint { get; set; }
        public string RoundState { get; set; } = String.Empty;
    }
}
