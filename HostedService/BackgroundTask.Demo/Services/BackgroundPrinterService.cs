using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace BackgroundTask.Demo.Services
{
    public class BackgroundPrinterService : IHostedService
    {
        private int number = 0;
        private Timer timer;

        private readonly ILogger<BackgroundPrinterService> _logger;
        private readonly IWorker _worker;

        public BackgroundPrinterService(ILogger<BackgroundPrinterService> logger, IWorker worker)
        {
            _logger = logger;
            _worker = worker;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _worker.DoWorkAsync(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping BackgroundPrinterService");
            return Task.CompletedTask;
        }
    }
}
