using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace _5ParallelTasks;

internal class TaskRunner
{
    public TaskRunner(ConcurrentQueue<Action> actions, int maxCount)
    {
        _actions = actions;
        _semaphoreSlim = new SemaphoreSlim(1, maxCount);
    }

    public async Task RunAsync()
    {
        while (_actions.TryPeek(out _))
        {
            try
            {
                await _semaphoreSlim.WaitAsync();
                Console.WriteLine($"Semaphore CurrentCount: {_semaphoreSlim.CurrentCount}");

                _actions.TryDequeue(out Action action);
                await Task.Run(action);
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }
    }

    private ConcurrentQueue<Action> _actions = null;
    private SemaphoreSlim _semaphoreSlim = null;
}
