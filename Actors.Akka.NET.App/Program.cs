using System;
using Actors.AkkaNET.App.Actors;
using Akka.Actor;

namespace Actors.AkkaNET.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Akka1();
            Console.WriteLine("Press any key!");
            Console.ReadKey();
        }

        private static void Akka1()
        {
            using (var system = ActorSystem.Create("iot-system"))
            {
                // Create top level supervisor
                var supervisor = system.ActorOf(Props.Create<IotSupervisor>(), "iot-supervisor");

                supervisor.Tell("");

                // Exit the system after ENTER is pressed
                Console.WriteLine("Press key for exit");
                Console.ReadLine();
            }
        }
    }
}
