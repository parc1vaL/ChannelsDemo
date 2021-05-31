using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Channels
{
    // Register as background service
    public class JobWorker : BackgroundService
    {
        private readonly JobQueue jobQueue;

        public JobWorker(JobQueue jobQueue)
        {
            this.jobQueue = jobQueue;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken = default)
        {
            await foreach (var job in this.jobQueue.Reader.ReadAllAsync().WithCancellation(stoppingToken))
            {
                // handle job - example
                Console.WriteLine(job);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
