using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Area51
{
    internal class Agent
    {

        enum Floors { GFloor, SFloor, T1Floor, T2Floor };

        static Random random = new Random();



        private bool Throw(int chance)
        {
            int dice = random.Next(100);
            return dice < chance;
        }

        Floors currentFloor;
        Elevator elevator;

        public Agent(Elevator elevator)
        {
            this.elevator = elevator;
        }

        public string Id { get; set; }


        public void AgentsMovements()
        {
            while (true)
            {
                Thread.Sleep(200);
                if (Throw(40))
                {
                    if (elevator.LeaveTheBuildingSignal.WaitOne(0))
                    {
                        Console.WriteLine($"{Id} is entering the building.");
                        break;
                    }
                    elevator.TryEnter(this);
                    while (true)
                    {
                        if (Throw(40))
                        {
                            Console.WriteLine($"{Id} is going to G floor...");
                        }
                        else
                        if (elevator.LeaveTheBuildingSignal.WaitOne(0) ||
                            Throw(20))
                        {
                            Console.WriteLine($"{Id} is going to S floor.");
                            elevator.Leave(this);
                            break;
                        }
                        else
                        {
                            Console.WriteLine($"{Id} is going to T1 floor.");
                        }
                        Thread.Sleep(200);
                    }
                }
                else
                if (Throw(30))
                {
                    Console.WriteLine($"{Id} is going to T2 floor.");
                    break;
                }
                else
                {
                    Console.WriteLine($"{Id} is leaving the building.");
                }

            }



        }




    }
}
