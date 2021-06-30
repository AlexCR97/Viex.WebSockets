using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Viex.WebSockets.Api.Services;
using Viex.WebSockets.Core.Repositories;

namespace Viex.WebSockets.Api.Hubs
{
    public partial class ViexHub : Hub
    {
        private readonly GameRoomRepository _gameRooms;
        private readonly UserRepository _users;
        private readonly WaitingRoomRepository _waitingRooms;
        private readonly IWaitingRoomCountDownTaskQueue _waitingRoomCountDownTasks;

        public ViexHub(GameRoomRepository gameRooms, UserRepository users, WaitingRoomRepository waitingRooms, IWaitingRoomCountDownTaskQueue waitingRoomCountDownTasks)
        {
            _gameRooms = gameRooms;
            _users = users;
            _waitingRooms = waitingRooms;
            _waitingRoomCountDownTasks = waitingRoomCountDownTasks;
        }

        public override async Task OnConnectedAsync()
        {
            // Handle socket connection
        }
    }
}
