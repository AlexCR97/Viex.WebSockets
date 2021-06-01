using System;
using System.Collections.Generic;
using System.Text;

namespace Viex.WebSockets.Core.Errors
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(int id)
            : base($"Could not find User with UserId {id}")
        {

        }
    }
}
