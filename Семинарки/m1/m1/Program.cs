using System;
using System.Threading;

class SharedResource
{
    private static Mutex readMutex = new Mutex();
    private static object writeLock = new object();
    private static int readCount = 0;

    private int data = 0;

    public void Read()
    {
        readMutex.WaitOne();
        readCount++;
        readMutex.ReleaseMutex();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"[Reader {Thread.CurrentThread.ManagedThreadId}] Reading: {data}");
        Console.ResetColor();
        Thread.Sleep(100);

        readMutex.WaitOne();
        readCount--;
        if (readCount == 0)
        {
            lock (writeLock)
            {
                Monitor.PulseAll(writeLock);
            }
        }
        readMutex.ReleaseMutex();
    }

    public void Write(int newData)
    {
        lock (writeLock)
        {
            while (true)
            {
                readMutex.WaitOne();
                bool hasReaders = readCount > 0;
                readMutex.ReleaseMutex();

                if (!hasReaders) break;

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"[Writer {Thread.CurrentThread.ManagedThreadId}] Waiting for readers to finish...");
                Console.ResetColor();

                Monitor.Wait(writeLock);
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[Writer {Thread.CurrentThread.ManagedThreadId}] Writing: {newData}");
            Console.ResetColor();
            data = newData;
            Thread.Sleep(200);
        }
    }
}
