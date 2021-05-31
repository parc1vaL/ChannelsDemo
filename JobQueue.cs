using System;
using System.Threading.Channels;

namespace Channels
{
    // Register as Singleton
    public class JobQueue
    {
        private readonly Channel<int> channel;

        public JobQueue()
        {
            this.channel = Channel.CreateUnbounded<int>();
        }

        public ChannelReader<int> Reader => this.channel.Reader;

        public ChannelWriter<int> Writer => this.channel.Writer;
    }
}
