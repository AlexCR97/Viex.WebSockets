using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Viex.WebSockets.Api.Hubs;
using Viex.WebSockets.Core.Repositories;

namespace Viex.WebSockets.Api.Services
{
    public class WaitingRoomCountDownService : BackgroundService
    {
        private readonly IHubContext<ViexHub> _hub;
        private readonly IWaitingRoomCountDownTaskQueue _queue;
        private readonly WaitingRoomRepository _waitingRooms;

        public WaitingRoomCountDownService(IHubContext<ViexHub> hub, IWaitingRoomCountDownTaskQueue queue, WaitingRoomRepository waitingRooms)
        {
            _hub = hub;
            _queue = queue;
            _waitingRooms = waitingRooms;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var job = await _queue.DequeueAsync(stoppingToken);
                await StartWaitingRoomCountDown(job);
            }
        }

        private Task StartWaitingRoomCountDown(string waitingRoomPassword)
        {
            var timer = new System.Timers.Timer
            {
                AutoReset = true,
                Interval = 1000,
            };

            timer.Elapsed += async (sender, ev) =>
            {
                var waitingRoom = await _waitingRooms.GetFirst(room => room.Password == waitingRoomPassword);
                waitingRoom.RemainingSeconds -= 1;

                await _waitingRooms.Edit(waitingRoom.WaitingRoomId, waitingRoom);
                await _hub.Clients.Group(waitingRoomPassword).SendAsync("WaitingRoomRemainingSecondsElapsed", waitingRoom.RemainingSeconds);

                if (waitingRoom.RemainingSeconds <= 0)
                {
                    await _hub.Clients.Group(waitingRoomPassword).SendAsync("WaitingRoomTimeUp");
                    timer.Stop();
                }
            };

            timer.Start();

            return Task.CompletedTask;
        }
    }
}
