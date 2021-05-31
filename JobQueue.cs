using System;
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
            var channelOptions = new BoundedChannelOptions(Capacity)
            {
                FullMode = BoundedChannelFullMode.DropWrite,
            };

            this.channel = Channel.CreateBounded<int>(channelOptions);
        }

        public ChannelReader<int> Reader => this.channel.Reader;

        public ChannelWriter<int> Writer => this.channel.Writer;
    }
}
