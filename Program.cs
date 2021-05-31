using System;
using System.Linq;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Channels
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var channel = Channel.CreateUnbounded<int>();

            _ = Task.Run(async () =>
            {
                for (int i = 1; i <= 10; i++)
                {
                    await channel.Writer.WriteAsync(i);
                    await Task.Delay(1000);
                }

                channel.Writer.Complete();
            });

            await foreach (var item in channel.Reader.ReadAllAsync())
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("All done!");
        }
    }
}
