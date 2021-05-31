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
            var channelOptions = new BoundedChannelOptions(5)
            {
                FullMode = BoundedChannelFullMode.Wait, // Default-Verhalten: Warten, bis Kapazität frei ist
                // FullMode = BoundedChannelFullMode.DropNewest, // Entferne das neueste (hinterste) Element des Channels, um Platz für das neu hinzu zu fügende Element zu schaffen
                // FullMode = BoundedChannelFullMode.DropOldest, // Entferne das älteste (vorderste) Element des Channels, um Platz für das neu hinzu zu fügende Element zu schaffen
                // FullMode = BoundedChannelFullMode.DropWrite, // Ignoriere ("droppe") das neu hinzu kommende Element
            };

            var channel = Channel.CreateBounded<int>(channelOptions);

            for (int i = 1; i <= 10; i++)
            {
                await channel.Writer.WriteAsync(i);
                Console.WriteLine($"{i} eingefügt.");

                // Weitere Schreib-Operationen:
                // var success = channel.Writer.TryComplete(); // Wirft keine Exception, wenn der Channel bereits als abgeschlossen markiert ist.
                // var success = channel.Writer.TryWrite(i); // Synchrones Schreiben - Liefert false, falls keine Kapazität frei ist (falls BoundedChannelFullMode.Wait) oder der Channel als abgeschlossen markiert ist
                // var success = await channel.Writer.WaitToWriteAsync(); // Wartet bis Kapazität frei ist, ohne zu schreiben. Liefert false, falls der Channel vorzeitig als abgeschlossen markiert wird.
            }

            channel.Writer.Complete();

            await foreach (var item in channel.Reader.ReadAllAsync())
            {
                Console.WriteLine($"{item} abgeholt.");
                await Task.Delay(1000);
            }

            // Weitere Lese-Operationen
            // var success = channel.Reader.TryRead(out var newItem); // Synchrones Lesen - Liefert false, falls keine Elemente vorhanden sind oder der Channel als abgeschlossen markiert ist
            // var success = channel.Reader.WaitToReadAsync(); // Wartet, bis ein Element im Channel zur Verfügung steht. Liefert false, falls der Channel vorzeitig als abgeschlossen markiert wird.

            Console.WriteLine("All done!");
        }
    }
}
