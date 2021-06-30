using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Viex.WebSockets.Api.Hubs
{
    public partial class ViexHub
    {
        public async Task StartGame(string roomPassword)
        {
            await Clients.Group(roomPassword).SendAsync("GameStarted");
        }

        public async Task StartRound(string roomPassword)
        {
            var room = await _gameRooms.GetFirst(a => a.Password == roomPassword);
            await Clients.Group(roomPassword).SendAsync("RoundStarted", room);
        }
    }
}
