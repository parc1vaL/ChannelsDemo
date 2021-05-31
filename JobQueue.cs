using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Channels;

namespace Channels
{
    // Register as Singleton
    public class JobQueue
    {
        private const int Capacity = 250;

        private readonly Channel<int> channel;

        public JobQueue()
        {
            this.channel = Channel.CreateBounded<int>(Capacity);
        }

        public bool TryWrite(int job) => this.channel.Writer.TryWrite(job);

        public IAsyncEnumerable<int> ReadAllAsync() => this.channel.Reader.ReadAllAsync();
    }
}
