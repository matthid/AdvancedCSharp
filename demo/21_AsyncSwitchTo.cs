#if NET6_0_OR_GREATER
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace Demo;

class AsyncSwitchTo{

    public class WorkerThread : IDisposable {
        private readonly Thread _thread;
        private readonly CancellationTokenSource _token = new CancellationTokenSource();
        private readonly ConcurrentQueue<Action> _work = new ConcurrentQueue<Action>();

        public WorkerThread(){
            _thread = new Thread(OnStart);
            _thread.IsBackground = true;
            _thread.Start();
        }

        private void OnStart(object? obj)
        {
            Console.WriteLine($"Started new thread {Thread.CurrentThread.ManagedThreadId}...");
            while (!_token.IsCancellationRequested){
                if (_work.TryDequeue(out var action)){
                    action();
                } else {
                    Thread.Sleep(1);
                }
            }
        }

        public Thread ManagedThread => _thread;

        public void PutWork(Action work){
            _work.Enqueue(work);
        }
        
        public void Dispose(){
            Console.WriteLine($"Disposing thread in {Thread.CurrentThread.ManagedThreadId}...");
            _token.Cancel();
            if (Thread.CurrentThread.ManagedThreadId != _thread.ManagedThreadId){
                _thread.Join();
            }
        }

        public SwitchToThreadAwaiter GetAwaiter(){
            return new SwitchToThreadAwaiter(this);
        }
    }

    public struct SwitchToThreadAwaiter : INotifyCompletion
    {
        private readonly WorkerThread _thread;
        public SwitchToThreadAwaiter(WorkerThread thread)
        {
            _thread = thread;
        }

        public bool IsCompleted => _thread.ManagedThread.ManagedThreadId == Thread.CurrentThread.ManagedThreadId;

        public void OnCompleted(Action continuation) => _thread.PutWork(continuation);

        public void GetResult() { }
    }


    public struct SwitchToThreadPoolAwaiter : INotifyCompletion
    {
        public SwitchToThreadPoolAwaiter()
        {
        }

        public bool IsCompleted => false;

        public void OnCompleted(Action continuation) => Task.Run(continuation);

        public void GetResult() { }

        public SwitchToThreadPoolAwaiter GetAwaiter(){
            return this;
        }
    }

    public static SwitchToThreadPoolAwaiter SwitchToThreadPool(){
        return new SwitchToThreadPoolAwaiter();
    }


    public static async Task RunWithSwitchToThread(){
        Console.WriteLine($"RunWithSwitchToThread ThreadId: {Thread.CurrentThread.ManagedThreadId}");
        var thread = new WorkerThread();
        await thread; // Switch to thread.
        Console.WriteLine($"RunWithSwitchToThread (after await) ThreadId: {Thread.CurrentThread.ManagedThreadId}");

        await SwitchToThreadPool();
        thread.Dispose();
    }

    public static async Task RunAsync(){
        Console.WriteLine($"RunAsync ThreadId: {Thread.CurrentThread.ManagedThreadId}");
        await RunWithSwitchToThread().ConfigureAwait(true); // Switch to thread.
        Console.WriteLine($"RunAsync (after await) ThreadId: {Thread.CurrentThread.ManagedThreadId}");
    }


    public static void AsyncSwitchToDemo(){
        Console.WriteLine($"AsyncSwitchToDemo ThreadId: {Thread.CurrentThread.ManagedThreadId}");
        RunAsync().Wait();
        Console.WriteLine($"AsyncSwitchToDemo (after await) ThreadId: {Thread.CurrentThread.ManagedThreadId}");
    }
}
#endif