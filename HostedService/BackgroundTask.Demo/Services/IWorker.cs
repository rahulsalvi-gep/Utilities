using System.Threading;
using System.Threading.Tasks;

namespace BackgroundTask.Demo.Services
{
    public interface IWorker
    {
        Task DoWorkAsync(CancellationToken cancellationToken);
    }
}