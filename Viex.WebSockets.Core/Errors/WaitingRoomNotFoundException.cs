using System;
using System.Collections.Generic;
using System.Text;

namespace Viex.WebSockets.Core.Errors
{
    public class WaitingRoomNotFoundException : Exception
    {
        public WaitingRoomNotFoundException(int id)
            : base($"Could not find WaitingRoom with WaitingRoomId {id}")
        {

        }

        public WaitingRoomNotFoundException(string roomPassword)
            : base($"Could not find WaitingRoom with Password \"{roomPassword}\"")
        {

        }
    }
}
