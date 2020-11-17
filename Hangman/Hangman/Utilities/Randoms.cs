using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Hangman.Utilities
{
    public static class Randoms
    {
        private readonly static RNGCryptoServiceProvider _provider = new RNGCryptoServiceProvider();

        public static KeyValuePair<string, string> GetRandomEntryFromDictAsync(Dictionary<string, string> countryCityPair)
        {
            uint scale = uint.MaxValue;
            while (scale == uint.MaxValue)
            {
                byte[] byteArray = new byte[4];
                _provider.GetBytes(byteArray);

                scale = BitConverter.ToUInt32(byteArray, 0);
            }

            int randomIndex = (int)(0 + (countryCityPair.Count() - 0) * (scale / (double)uint.MaxValue));

            string country = countryCityPair.ElementAt(randomIndex).Key;

            return new KeyValuePair<string, string>(country, countryCityPair[country]);
        }
    }
}
