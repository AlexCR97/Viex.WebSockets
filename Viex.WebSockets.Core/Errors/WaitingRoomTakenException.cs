using System;
using System.Collections.Generic;
using System.Text;

namespace Viex.WebSockets.Core.Errors
{
    public class WaitingRoomTakenException : Exception
    {
        public WaitingRoomTakenException(string roomPassword)
            : base($"WaitingRoom with password \"{roomPassword}\" is already taken.")
        {

        }
    }
}
