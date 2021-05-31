using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Channels
{
    // Register as Singleton!
    public class Worker
    {
        private readonly ConcurrentQueue<int> jobQueue;

        private readonly Task worker;

        public Worker()
        {
            this.jobQueue = new ConcurrentQueue<int>();

            this.worker = Task.Run(HandleWork);
        }

        public void ScheduleJob(int job)
        {
            this.jobQueue.Enqueue(job);
        }

        private async Task HandleWork()
        {
            while (true)
            {
                while (this.jobQueue.TryDequeue(out var job))
                {
                    // handle job - example.
                    await Task.Delay(1000);
                    Console.WriteLine(job);
                }

                await Task.Delay(1000);
            }
        }
    }
}
