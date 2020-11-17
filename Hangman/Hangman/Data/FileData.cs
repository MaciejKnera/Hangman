using Hangman.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hangman.Data
{
    public static class FileData
    {
        public static readonly string _countryCapitalFile = "countries_and_capitals.txt.txt", _scoreFile = "score.txt";
        
        private static string ReturnRelativePathToFile(string  fileName)
        {
            return Path.Combine($@"{ Environment.CurrentDirectory}", @"Data\", fileName);
        }

        private static async Task<string> ReadFileAsync(string path)
        {
            string output;

            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    output = await reader.ReadToEndAsync();
                }
            }
            catch (Exception)
            {

                throw;
            }


            return output;
        }

        public static async Task<Dictionary<string, string>> ConvertFileContentToDictAsync(string fileName)
        {
            Dictionary<string, string> output = new Dictionary<string, string>();

            try
            {
                string fileContent = await ReadFileAsync(ReturnRelativePathToFile(fileName));

                string[] lines = fileContent.Split("\n");
                foreach (string line in lines)
                {
                    string[] countryCityPair = line.Split("|");

                    if (output.ContainsKey(countryCityPair[0]) == false)
                    {
                        output[countryCityPair[0]] = countryCityPair[1];
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return output;
        } 

        public static async Task WriteToFileAsync(Player player, string fileName)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(ReturnRelativePathToFile(fileName), true))
                {
                    await writer.WriteLineAsync($"{player.Name} | {player.Date} | {player.RoundTime} | {player.GuessingTries} | {player.GuessedWord}");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static async Task<List<Player>> ReturnTopScoresFromFileAsync(int scoresNumber)
        {
            List<Player> output = new List<Player>();

            try
            {
                using (StreamReader reader = new StreamReader(ReturnRelativePathToFile(_scoreFile)))
                {
                    string fileContent = await reader.ReadToEndAsync();
                    string[] playersScores = fileContent.Split("/n");

                    foreach (var score in playersScores)
                    {
                        string[] playerScore = score.Split("|");
                        output.Add(new Player()
                        {
                            Name = playerScore[0],
                            Date = Convert.ToDateTime(playerScore[1]),
                            RoundTime = TimeSpan.Parse(playerScore[2]),
                            GuessingTries = Convert.ToInt32(playerScore[3]),
                            GuessedWord = playerScore[4]
                        });
                    }
                }

                output = output.OrderBy(player => player.RoundTime).ThenBy(player => player.GuessingTries).Take(scoresNumber).ToList();
            }
            catch (Exception)
            {

                throw;
            }

            return output;

        }
    }
}
