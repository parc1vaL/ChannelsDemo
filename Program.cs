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

            for (int i = 1; i <= 10; i++)
            {
                await channel.Writer.WriteAsync(i);
            }

            while (true)
            {
                var item = await channel.Reader.ReadAsync();

                Console.WriteLine(item);
            }
        }
    }
}
