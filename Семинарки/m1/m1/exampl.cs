using System;
using System.Threading;

class Program
{
    static SharedResource resource = new SharedResource();
    static Random rnd = new Random();
    static volatile bool stopRequested = false;

    static void Main()
    {
        for (int i = 0; i < 3; i++)
        {
            int readerId = i + 1;
            new Thread(() =>
            {
                for (int j = 0; j < 3 && !stopRequested; j++)
                {
                    Thread.Sleep(rnd.Next(300, 800));
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[Reader {readerId}] wants to read...");
                    Console.ResetColor();

                    resource.Read();
                }
            }).Start();
        }

        new Thread(() =>
        {
            int writerId = 1;
            int val = 0;
            for (int i = 0; i < 3 && !stopRequested; i++)
            {
                Thread.Sleep(rnd.Next(1000, 2000));
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"[Writer {writerId}] wants to write...");
                Console.ResetColor();

                resource.Write(val++);
            }
        }).Start();

        Console.WriteLine("Press any key to stop...");
        Console.ReadKey();
        stopRequested = true;
    }
}
