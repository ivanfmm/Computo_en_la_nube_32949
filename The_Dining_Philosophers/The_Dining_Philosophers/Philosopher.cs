using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace The_Dining_Philosophers
{
    internal class Philosopher
    {
        public int id { get; }
        public Fork leftHand { get; set; }
        public Fork rightHand { get; set; }
        private readonly Random _random;

        private const int minBackOff = 50;
        private const int maxBackOff = 200;

        private const int minThink = 10;
        private const int maxThink = 1000;

        private const int minEat = 100;
        private const int maxEat = 500;

        private const int time = 10;


        public Philosopher(int id, Fork leftFork, Fork rightFork)
        {
            this.id = id;
            _random = new Random(Guid.NewGuid().GetHashCode());
            this.leftHand = leftFork;
            this.rightHand = rightFork;
        }

        public void think()
        {
            Console.WriteLine($"Philosopher {this.id} is thinking");
            Thread.Sleep(_random.Next(minThink, maxThink) * time);
        }
        public void eat()
        {
            Console.WriteLine($"Philosopher {this.id} is eating");
            Thread.Sleep(_random.Next(minEat, maxEat) * time);
        }

        public void backoff()
        {
            Console.WriteLine($"Philosopher {this.id} backed off");
            Thread.Sleep(_random.Next(minBackOff, maxBackOff) * time);
        }

        public void pick()
        {
            while (true)
            {
                if (leftHand.TryTake())
                {
                    Console.WriteLine($"Philosopher {this.id} got first fork");
                    if (rightHand.TryTake())
                    {
                        Console.WriteLine($"Philosopher {this.id} got both Forks");
                        return;
                    }
                    else
                    {
                        Console.WriteLine($"Philosopher {this.id} failed to get second fork");
                        put(leftHand);
                        backoff();
                    }
                }
                else
                {
                    Console.WriteLine($"Philosopher {this.id} failed to get first fork");
                    backoff();
                }
            }
        }
        public void put(Fork fork)
        {
            Console.WriteLine($"Philosopher {this.id} released a fork");
            fork.Release();
        }
        public void Live()
        {
            while (true)
            {
                think();
                pick();
                eat();
                put(this.leftHand);
                put(this.rightHand);
            }
        }
    }
}
