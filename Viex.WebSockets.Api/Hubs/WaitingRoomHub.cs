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
            _tasks.EnqueueJob(waitingRoomPassword);
            return Task.CompletedTask;

            //await Task.Run(() =>
            //{
            //    var timer = new Timer
            //    {
            //        AutoReset = true,
            //        Interval = 1000,
            //    };

            //    timer.Elapsed += async (sender, ev) =>
            //    {
            //        var waitingRoom = await _waitingRooms.GetFirst(room => room.Password == waitingRoomPassword);
            //        var remainingSeconds = waitingRoom.RemainingSeconds - 1;

            //        await _waitingRooms.Edit(waitingRoom.WaitingRoomId, waitingRoom);
            //        await Clients.Group(waitingRoomPassword).SendAsync("WaitingRoomRemainingSecondsElapsed", remainingSeconds);

            //        if (remainingSeconds <= 0)
            //        {
            //            timer.Stop();
            //        }
            //    };

            //    timer.Start();
            //});
        }
    }
}
