using System;
using System.Collections.Generic;
using System.Text;

namespace Viex.WebSockets.Core.Errors
{
    public class GuestAlreadyInWaitingRoomException : Exception
    {
        public GuestAlreadyInWaitingRoomException(string username, string roomPassword)
            : base($"User with Username \"{username}\" has already joined WaitingRoom with Password \"{roomPassword}\"")
        {

        }
    }
}
