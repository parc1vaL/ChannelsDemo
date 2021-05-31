using System;
using Microsoft.Extensions.Logging;

namespace Channels
{
    // Register as Transient/Singleton
    public class JobScheduler
    {
        private readonly JobQueue jobQueue;
        private readonly ILogger<JobScheduler> logger;

        public JobScheduler(JobQueue jobQueue, ILogger<JobScheduler> logger)
        {
            this.jobQueue = jobQueue;
            this.logger = logger;
        }

        public void ScheduleJob(int job)
        {
            if (this.jobQueue.Writer.TryWrite(job))
            {
                this.logger.LogInformation("Job {Job} successfully scheduled.", job);
            }
            else
            {
                this.logger.LogError("Job {Job} could not be scheduled. Back pressure too high!", job);
                // or
                throw new Exception("Job could not be scheduled. Try again later.");
            }
        }
    }
}
