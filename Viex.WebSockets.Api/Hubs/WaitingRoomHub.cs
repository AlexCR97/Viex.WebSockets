using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;
using Viex.WebSockets.Api.Services;
using Viex.WebSockets.Core.Payloads;

namespace Viex.WebSockets.Api.Hubs
{
    public partial class ViexHub
    {
        public async Task JoinWaitingRoom(JoinWaitingRoomPayload payload)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, payload.RoomPassword);
            await Clients.Group(payload.RoomPassword).SendAsync("GuestJoinedWaitingRoom", payload.Username);
        }

        public async Task LeaveWaitingRoom(LeaveWaitingRoomPayload payload)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, payload.RoomPassword);
            await Clients.Group(payload.RoomPassword).SendAsync("GuestLeftWaitingRoom", payload.Username);
        }

        public Task StartCountDown(string waitingRoomPassword)
        {
            _waitingRoomCountDownTasks.EnqueueJob(waitingRoomPassword);
            return Task.CompletedTask;
        }
    }
}
