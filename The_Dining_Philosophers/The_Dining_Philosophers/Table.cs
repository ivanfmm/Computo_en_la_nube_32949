using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace The_Dining_Philosophers
{
    internal class Table
    {
        public List<Philosopher> Philosophers = new List<Philosopher>();
        public List<Fork> Forks = new List<Fork>();
        public Table(int num)
        {
            for (int i = 0; i < num; i++)
            {
                Fork f = new Fork(i);
                Forks.Add(f);
            }
            for (int i = 0; i < num; i++)
            {
                if (i == 0)
                {
                    Philosopher p = new Philosopher(i, Forks[num - 1], Forks[i]);
                    Philosophers.Add(p);
                }
                else
                {
                    Philosopher p = new Philosopher(i, Forks[i], Forks[i - 1]);
                    Philosophers.Add(p);
                }
            }
        }
        public void start()
        {
            foreach (var philosopher in Philosophers)
            {
                Thread t = new Thread(philosopher.Live);
                t.Start();
            }
        }
    }
}
