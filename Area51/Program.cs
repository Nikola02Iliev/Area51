using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Area51
{
    class Program
    {


        static void Main()
        {
            Elevator elevator = new Elevator();

            var agentThreads = new List<Thread>();

            for(int i = 0; i <100; i++)
            {
                Agent a = new Agent(elevator)
                {

                    Id = "Agent " + i.ToString()
                };

                var t = new Thread(a.AgentsMovements);
                t.Start();
                agentThreads.Add(t);





            }

            Thread elevatorThread = new Thread(elevator.Work);
            elevatorThread.Start();

            foreach (var t in agentThreads) t.Join();
            Console.WriteLine("Elevator is not working.");


            Console.ReadLine();
        }
    }
}
