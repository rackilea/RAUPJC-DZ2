﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace zad6
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            LongOperation("A");
            LongOperation("B");
            LongOperation("C");
            LongOperation("D");
            LongOperation("E");
            stopwatch.Stop();
            Console.WriteLine(" Synchronous long operation calls finished {0} sec.", stopwatch.Elapsed.TotalSeconds);

            stopwatch.Restart();
            Parallel.Invoke(() => LongOperation("A"),
            () => LongOperation("B"),
            () => LongOperation("C"),
            () => LongOperation("D"),
            () => LongOperation("E"));
            stopwatch.Stop();
            Console.WriteLine(" Parallel long operation calls finished {0} sec.", stopwatch.Elapsed.TotalSeconds);

            stopwatch.Restart();
            Parallel.For(0, 1000, (i) =>
            {
                var x = 2;
                var y = 2;
                var sum = x + y;
            });
            stopwatch.Stop();
            Console.WriteLine(" Parallel calls finished {0} ms.", stopwatch.Elapsed.TotalMilliseconds);

            stopwatch.Restart();
            for (int i = 0; i < 1000; i++)
            {
                int x = 2;
                int y = 2;
                int sum = x + y;
            }
            stopwatch.Stop();
            Console.WriteLine(" Sync operation calls finished {0} ms.", stopwatch.Elapsed.TotalMilliseconds);

            int counter = 0;
            Parallel.For(0, 100, (i) =>
            {
                Thread.Sleep(1);
                counter += 1;
            });
            Console.WriteLine(" Counter should be 100. Counter is {0}", counter);

            object lockObject = new object();
            List<int> results = new List<int>();
            Parallel.For(0, 100, (i) =>
            {
                Thread.Sleep(1);
                lock (lockObject)
                {
                    results.Add(i * i);
                }
            });
            Console.WriteLine("Bag length should be 100. Length is {0}", results.Count);

            ConcurrentBag<int> iterations = new ConcurrentBag<int>();
            Parallel.For(0, 100, (i) =>
            {
                Thread.Sleep(1);
                iterations.Add(i);
            });
            Console.WriteLine("Bag length should be 100. Length is {0}", iterations.Count);

        }

        public static void LongOperation(string taskName)
        {
            Thread.Sleep(1000);
            Console.WriteLine("{0} Finished . Executing Thread : {1}", taskName,
            Thread.CurrentThread.ManagedThreadId);
        }
    }
}
