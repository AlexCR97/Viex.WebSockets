using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Viex.WebSockets.Api.Services
{
    public class WaitingRoomCountDownTaskQueue : IWaitingRoomCountDownTaskQueue
    {
        private ConcurrentQueue<string> _jobs = new ConcurrentQueue<string>();
        private SemaphoreSlim _signal = new SemaphoreSlim(0);

        public void EnqueueJob(string job)
        {
            if (job == null)
                throw new ArgumentNullException(nameof(job));

            _jobs.Enqueue(job);
            _signal.Release();
        }

        public async Task<string> DequeueAsync(CancellationToken cancellationToken)
        {
            await _signal.WaitAsync(cancellationToken);
            _jobs.TryDequeue(out var job);
            return job;
        }
    }
}
