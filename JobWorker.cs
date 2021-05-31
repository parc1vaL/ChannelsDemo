using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Channels
{
    // Register as background service
    public class JobWorker : BackgroundService
    {
        private readonly JobQueue jobQueue;
        private readonly ILogger<JobWorker> logger;

        public JobWorker(JobQueue jobQueue, ILogger<JobWorker> logger)
        {
            this.jobQueue = jobQueue;
            this.logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken = default)
        {
            await foreach (var job in this.jobQueue.Reader.ReadAllAsync().WithCancellation(stoppingToken))
            {
                // handle job - example
                this.logger.LogInformation("Executing job {Job}.", job);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
