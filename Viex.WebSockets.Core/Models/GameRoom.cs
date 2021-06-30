using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Viex.WebSockets.Core.Models
{
    public class GameRoom
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GameRoomId { get; set; }
        public string Password { get; set; }
        public int TotalRounds { get; set; }
        public int CurrentRound { get; set; }
        public char CurrentRoundLetter { get; set; }
        public int CurrentRoundRemainingSeconds { get; set; }

        public int HostId { get; set; }
        public User Host { get; set; }

        public IList<User> Players { get; set; }

        public IList<GameConcept> CurrentRoundGameConcepts { get; set; }
    }
}
