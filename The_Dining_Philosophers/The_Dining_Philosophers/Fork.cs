using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Dining_Philosophers
{
    internal class Fork
    {
        public int id { get; }
        private int taken;

        public Fork(int id)
        {
            this.id = id;
            taken = 0;
        }

        public bool TryTake()
        {
            int original = Interlocked.CompareExchange(ref this.taken, 1, 0);
            return original == 0;
        }

        public void Release()
        {
            Interlocked.Exchange(ref taken, 0);
        }
    }
}
//Thread.Sleep(random.Next(minMs, maxMs))