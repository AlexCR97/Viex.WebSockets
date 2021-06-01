using System;
using System.Collections.Generic;
using System.Text;

namespace Viex.WebSockets.Core.Errors
{
    public class UsernameTakenException : Exception
    {
        public UsernameTakenException(string username)
            : base($"A User with the Username \"{username}\" already exists.")
        {

        }
    }
}
