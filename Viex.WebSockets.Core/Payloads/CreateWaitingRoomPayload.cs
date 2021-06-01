using System;
using System.Collections.Generic;
using System.Text;

namespace Viex.WebSockets.Core.Payloads
{
    public class CreateWaitingRoomPayload
    {
        public string Username { get; set; }
        public string RoomPassword { get; set; }
    }
}
