using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace BackgroundTask.Demo.Services
{
    public class DerivedBackgroundPrinterService : BackgroundService
    {
        private readonly IWorker _worker;

        public DerivedBackgroundPrinterService(IWorker worker)
        {
            _worker = worker;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _worker.DoWorkAsync(stoppingToken);
        }
    }
}
