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
        private static readonly string _fileName = "countries_and_capitals.txt.txt";
        private static readonly string _path = Path.Combine($@"{ Environment.CurrentDirectory}", @"Data\", _fileName);

        private static async Task<string> ReadFileAsync()
        {
            string output;

            try
            {
                using (StreamReader reader = new StreamReader(_path))
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

        public static async Task<Dictionary<string, string>> ConvertFileContentToDictAsync()
        {
            Dictionary<string, string> output = new Dictionary<string, string>();

            try
            {
                string fileContent = await ReadFileAsync();

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
    }
}
