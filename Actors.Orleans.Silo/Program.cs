using System;
using System.Net;
using System.Threading.Tasks;
using Actors.Orlean.Grains;
using Microsoft.Extensions.Logging;
using Orleans.ApplicationParts;
using Orleans.Configuration;
using Orleans.Hosting;

namespace Actors.Orleans.Silo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Press any key to start silo!");
            Console.ReadKey();

            await StartSilo();
            Console.WriteLine("Press any key!");
            Console.ReadKey();
        }

        private static async Task<ISiloHost> StartSilo()
        {
            // define the cluster configuration
            var builder = new SiloHostBuilder()
                .UseLocalhostClustering()
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "dev";
                    options.ServiceId = "HelloWorldApp";
                })
                .Configure<EndpointOptions>(options => options.AdvertisedIPAddress = IPAddress.Loopback)
                .ConfigureApplicationParts(parts => parts.AddApplicationPart(new AssemblyPart(typeof(HelloGrain).Assembly)))
                .ConfigureLogging(logging => logging.AddConsole());

            var host = builder.Build();
            await host.StartAsync();
            return host;
        }
    }
}
