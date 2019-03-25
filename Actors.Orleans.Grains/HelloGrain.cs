using System;
using System.Threading.Tasks;

namespace Actors.Orlean.Grains
{
    public class HelloGrain : Orleans.Grain, IHello
    {
        private int _counter = 0;

        Task<string> IHello.SayHello(string greeting)
        {
            _counter++;
            //logger.LogInformation($"SayHello message received: greeting = '{greeting}'");
            Console.WriteLine($"SayHello message received: greeting = '{greeting}'");
            return Task.FromResult($"You said: '{greeting}', I say: Hello! #{_counter}");
        }
    }
}