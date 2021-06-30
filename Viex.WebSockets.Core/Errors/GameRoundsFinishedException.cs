using System;
using System.Collections.Generic;
using System.Text;

namespace Viex.WebSockets.Core.Errors
{
    public class GameRoundsFinishedException : Exception
    {
        public GameRoundsFinishedException()
            : base("Cannot start next round because game has no rounds left.")
        {

        }
    }
}
