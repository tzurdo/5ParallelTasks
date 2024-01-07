using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace _5ParallelTasks
{
    internal class Program
    {
        static int MAX_PARALLEL_TASKS = 5;  

        static async Task Main(string[] args)
        {
            Console.WriteLine("Start");

            // The following section shall create 100 "jobs".
            // The jobs will be handled by 5 tasks that shall run in parallel.
            ConcurrentQueue<Action> actions = new ConcurrentQueue<Action>();

            for (int i = 0; i < 100; i++)
            {
                int index = i;

                Action a = () => Console.WriteLine($"Task with index: {index}");
                actions.Enqueue(a);
            }

            TaskRunner taskRunner = new TaskRunner(actions, MAX_PARALLEL_TASKS);
            await taskRunner.RunAsync();

            Console.WriteLine("Finish");
        }
    }
}
