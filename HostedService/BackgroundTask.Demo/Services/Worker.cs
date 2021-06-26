using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BackgroundTask.Demo.Services
{
    public class Worker : IWorker
    {
        private int number = 0;

        public ILogger<Worker> _Logger { get; }

        public Worker(ILogger<Worker> logger)
        {
            _Logger = logger;
        }

        public async Task DoWorkAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                Interlocked.Increment(ref number);
                _Logger.LogInformation($"Worker {number}");
                await Task.Delay(TimeSpan.FromSeconds(5));
            }
        }
    }
}
