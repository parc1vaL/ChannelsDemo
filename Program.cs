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
            });

            while (true)
            {
                var item = await channel.Reader.ReadAsync();
                Console.WriteLine(item);
            }
        }
    }
}
