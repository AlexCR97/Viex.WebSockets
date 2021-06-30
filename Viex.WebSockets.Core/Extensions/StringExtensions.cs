using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Viex.WebSockets.Core.Extensions
{
    public static class StringExtensions
    {
        public static string ToSentenceCase(this string str)
        {
            return Regex.Replace(str, "(\\B[A-Z])", " $1");
        }
    }
}
