using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Viex.WebSockets.Core.Extensions
{
    public static class ListExtensions
    {
        private static readonly Random random = new Random();

        public static IList<T> Shuffle<T>(this IList<T> list)
        {
            var n = list.Count;
            var copy = list.ToList();

            while (n > 1)
            {
                n--;
                var index = random.Next(n + 1);
                var value = copy[index];
                copy[index] = copy[n];
                copy[n] = value;
            }

            return copy;
        }
    }
}
