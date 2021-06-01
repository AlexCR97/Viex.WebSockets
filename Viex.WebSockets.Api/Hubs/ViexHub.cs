using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Viex.WebSockets.Api.Services;
using Viex.WebSockets.Core.Models;
using Viex.WebSockets.Core.Payloads;
using Viex.WebSockets.Core.Repositories;

namespace Viex.WebSockets.Api.Hubs
{
    public partial class ViexHub : Hub
    {
        private readonly UserRepository _users;
        private readonly WaitingRoomRepository _waitingRooms;
        private readonly IWaitingRoomCountDownTaskQueue _tasks;

        public ViexHub(UserRepository users, WaitingRoomRepository waitingRooms, IWaitingRoomCountDownTaskQueue tasks)
        {
            _users = users;
            _waitingRooms = waitingRooms;
            _tasks = tasks;
        }

        public override async Task OnConnectedAsync()
        {

        }
    }
}
