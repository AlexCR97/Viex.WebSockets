using System;
using System.Collections.Generic;
using System.Text;

namespace Viex.WebSockets.Core.Models
{
    public class WaitingRoom
    {
        public int WaitingRoomId { get; set; }
        public string Password { get; set; }
        public int RemainingSeconds { get; set; }

        public int HostId { get; set; }
        public User Host { get; set; }

        public IList<User> Guests { get; set; }
    }
}
