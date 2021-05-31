using System;
using System.Threading.Tasks;

namespace Channels
{
    // Register as Transient/Singleton
    public class JobScheduler
    {
        private readonly JobQueue jobQueue;

        public JobScheduler(JobQueue jobQueue)
        {
            this.jobQueue = jobQueue;
        }

        public async Task ScheduleJob(int job)
        {
            Console.WriteLine($"Scheduling: {job}");
            await this.jobQueue.Writer.WriteAsync(job);
        }
    }
}
