using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace lr3._1
{
    public class ThreadManager
    {
        public delegate void MessageHandler(string message);
        public event MessageHandler OnMessage;

        private readonly List<Thread> _threads = new List<Thread>();
        private readonly Dictionary<int, Thread> _threadDictionary = new Dictionary<int, Thread>();

        public void CreateThreads(int[] array, int numberOfThreads)
        {
            ClearThreads();
            for (int i = 0; i < numberOfThreads; i++)
            {
                Thread thread = CreateMinFindingThread(array);
                OnMessage?.Invoke($"Створено поток з ID {thread.ManagedThreadId}");
                _threads.Add(thread);
                _threadDictionary[thread.ManagedThreadId] = thread;
            }
        }

        public void FindMinInParallel()
        {
            foreach (var thread in _threads)
            {
                thread.Start();
               
            }
        }

        private Thread CreateMinFindingThread(int[] array)
        {
            var thread = new Thread(() =>
            {
                int minValue = array.Min();
                OnMessage?.Invoke($"Thread {Thread.CurrentThread.ManagedThreadId} (priority: {Thread.CurrentThread.Priority}) found minimum: {minValue}");
            });
           

            return thread;
        }

       

        public void ClearThreads()
        {
            _threads.Clear();
            _threadDictionary.Clear();
        }

        public IEnumerable<Thread> GetThreads()
        {
            return _threads.AsReadOnly();
        }

        public void ChangeThreadPriority(int id, ThreadPriority priority)
        {
            if (_threadDictionary.TryGetValue(id, out var thread))
            {
                thread.Priority = priority;
                OnMessage?.Invoke($"Змінено пріоритет потоку {id} на {priority}");
            }
        }
    }
}
