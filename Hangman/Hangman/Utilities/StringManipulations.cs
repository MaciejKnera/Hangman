using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman.Utilities
{
    public static class StringManipulations
    {
        public static bool ContainsCharacter(string word, string character)
        {
            if (word.IndexOf(character) != -1)
            {
                return true;
            }

            return false;
        }

        public static string ReplaceCharactersInWord(string word, string wordToChange, string character)
        {
            var indexList = new List<int>();
            for (int i = word.IndexOf(character); i > -1; i = word.IndexOf(character, i + 1))
            {
                indexList.Add(i);
            }

            StringBuilder stringBuilder = new StringBuilder(wordToChange);
            for (int i = 0; i < indexList.Count; i++)
            {
                stringBuilder.Replace(stringBuilder[indexList[i]], Convert.ToChar(character), indexList[i], 1);
            }
            wordToChange = stringBuilder.ToString();

            return wordToChange;
        }
    }
}
