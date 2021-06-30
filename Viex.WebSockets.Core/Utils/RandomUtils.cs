using System;
using System.Collections.Generic;
using System.Text;

namespace Viex.WebSockets.Core.Utils
{
    public static class RandomUtils
    {
        private static readonly string UpperCaseLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public static int Int(int min, int max)
        {
            var random = new Random();
            return random.Next(min, max);
        }

        public static char UpperCaseLetter()
        {
            var index = Int(0, UpperCaseLetters.Length - 1);
            return UpperCaseLetters[index];
        }
    }
}
