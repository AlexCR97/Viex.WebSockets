using System;
using System.Collections.Generic;
using System.Text;

namespace Viex.WebSockets.Core.Errors
{
    public class GameRoomNotFoundException : Exception
    {
        public GameRoomNotFoundException(int id)
            : base($"Could not find GameRoom with GameRoomId {id}")
        {

        }

        public GameRoomNotFoundException(string roomPassword)
            : base($"Could not find GameRoom with Password {roomPassword}")
        {

        }
    }
}
