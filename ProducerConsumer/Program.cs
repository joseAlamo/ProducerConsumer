using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProducerConsumer
{
    class Program
    {
        public static Queue<int> numbers = new Queue<int>();
        public static Random r = new Random();

        static void Main(string[] args)
        {
            Thread produce = new Thread(Producer);
            Thread consume = new Thread(Consumer);
            Thread consume2 = new Thread(Consumer);
            Thread consume3 = new Thread(Consumer);
            consume.Name = "Consumidor 1";
            consume2.Name = "Consumidor 2";
            consume3.Name = "Consumidor 3";

            produce.Start();
            consume.Start();
            consume2.Start();
            consume3.Start();
        }

        private static void Producer()
        {

            while (true)
            {
                lock (numbers)
                {
                    int next = r.Next(200);
                    numbers.Enqueue(next);
                    Console.WriteLine("Produced: {0}", next.ToString());
                    Thread.Sleep(500);
                }
            }
        }

        private static void Consumer()
        {
            while (true)
            {
                lock (numbers)
                {
                    if (numbers.Count != 0)
                    {
                        string numTook = numbers.Dequeue().ToString();
                        Console.WriteLine("Consumed: {0} by: {1}", numTook,Thread.CurrentThread.Name);
                    }
                }
            }
        }
    }
}
