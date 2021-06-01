using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Viex.WebSockets.Api.Services
{
    public interface IWaitingRoomCountDownTaskQueue
    {
        void EnqueueJob(string waitingRoomPassword);
        Task<string> DequeueAsync(CancellationToken cancellationToken);
    }
}
