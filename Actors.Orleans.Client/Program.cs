using System;
using System.Net;
using System.Threading.Tasks;
using Actors.Orlean.Grains;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;

namespace Actors.Orleans.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Press any key to start client!");
            Console.ReadKey();
            
            var client = new ClientBuilder()
                .UseLocalhostClustering()
                // Clustering information
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "dev";
                    options.ServiceId = "HelloWorldApp";
                })
                //.Configure<EndpointOptions>(options => options.AdvertisedIPAddress = IPAddress.Loopback)
                // Clustering provider
                // Application parts: just reference one of the grain interfaces that we use
                //.ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(HelloGrain).Assembly))
                .ConfigureLogging(logging => logging.AddConsole())
                .Build();

            await client.Connect();
            Console.WriteLine("Client successfully connect to silo host");

            await DoClientWork(client);

            Console.WriteLine("Press any key!");
            Console.ReadKey();
        }

        private static async Task DoClientWork(IClusterClient client)
        {
            // example of calling grains from the initialized client
            var friend = client.GetGrain<IHello>(0);
            var response = await friend.SayHello("Good morning, my friend!");
            Console.WriteLine("\n{0}\n", response);
            Console.WriteLine("\n{0}\n", await friend.SayHello("Send 2"));
            Console.WriteLine("\n{0}\n", await friend.SayHello("Send 3"));
        }
    }
}
