using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace _5ParallelTasks
{
    internal class Program
    {
        private const int MaxCount = 5;

        static async Task Main(string[] args)
        {
            Console.WriteLine("Start");

            await RunTasks();

            Console.WriteLine("Finish");
        }

        private static async Task RunTasks()
        {
            ConcurrentQueue<Action> actions = new ConcurrentQueue<Action>();

            for (int i = 0; i < 100; i++)
            {
                int index = i;

                Action a = () => Console.WriteLine($"Task with index: {index}");
                actions.Enqueue(a);
            }

            TaskRunner taskRunner = new TaskRunner(actions, MaxCount);
            await taskRunner.RunAsync();
        }
    }
}
