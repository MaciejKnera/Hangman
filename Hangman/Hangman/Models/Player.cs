using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hangman.Models
{
    public class Player
    {
        [Required]
        [RegularExpression(@"^(?! )[A-Za-z\s]+$")]
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan RoundTime { get; set; }
        public int GuessingTries { get; set; }
        public string GuessedWord { get; set; }
    }
}
