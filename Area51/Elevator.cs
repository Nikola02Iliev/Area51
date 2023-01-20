using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Area51
{
    internal class Elevator
    {
        const int capacity = 1;
        Semaphore semaphore;
        List<Agent> agents;

        public ManualResetEvent LeaveTheBuildingSignal { get; private set; }
       
        public Elevator()
        {

            semaphore = new Semaphore(capacity, capacity);
            agents = new List<Agent>();
            LeaveTheBuildingSignal = new ManualResetEvent(false);
        }

        public bool TryEnter(Agent a)
        {
            if (semaphore.WaitOne())
            {
                lock (agents) agents.Add(a);
                return true;
            }
            else
                return false;
        }


        public void Leave(Agent a)
        {
            lock (agents) agents.Remove(a);
            semaphore.Release();
        }

        public void Work()
        {
            Console.WriteLine("Elevator is working!");
            Thread.Sleep(10000);
            LeaveTheBuildingSignal.Set();
        }

    }
}
