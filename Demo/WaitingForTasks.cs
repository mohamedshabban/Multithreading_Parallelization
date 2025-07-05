namespace Demo
{
    /* 6 - WaitingForTasks*/
    public class WaitingForTasks
    {
        public static void TaskFn()
        {
            CancellationTokenSource ctx = new CancellationTokenSource();
            var token = ctx.Token;
            Task t1 = new Task(() =>
            {
                Console.WriteLine($"Task has started");
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine($"Task 1: operation {i}");
                    token.ThrowIfCancellationRequested();
                    Thread.Sleep(1000);
                }
                Console.WriteLine($"Task has completed");
            }, token);
            t1.Start();

            Task t2 = Task.Factory.StartNew(() =>
            {
                Console.WriteLine($"Task 2: executed");
                Thread.Sleep(3000);
            }, token);
            Console.WriteLine($"Before: ------------------------------------");
            Console.WriteLine($"T1 status is {t1.Status}");
            Console.WriteLine($"T2 status is {t2.Status}");
            Console.WriteLine($"--------------------------------------------");

            Console.ReadKey();
            Console.WriteLine();
            ctx.Cancel();

            Task.WaitAll(t1, t2);
            Console.WriteLine($"After: --------------------------------------");

            Console.WriteLine($"T1 status is {t1.Status}");
            Console.WriteLine($"T2 status is {t2.Status}");
            Console.WriteLine($"--------------------------------------------");

            Console.WriteLine("Main program ended!");

        }
    }
    public class TaskException
    {
        public static void TaskFn()
        {
            Task t1 = Task.Factory.StartNew(() =>
            throw new InvalidOperationException("Can't Do Anything!!") { Source = "T1" });
            Task t2 = Task.Factory.StartNew(() =>
            throw new AccessViolationException("AccessViolationException!!") { Source = "T2" });

            try
            {
                Task.WaitAll(t1, t2);
            }
            catch (AggregateException ex)
            {
                foreach (var inner in ex.InnerExceptions)
                    Console.WriteLine($"Exception {ex.GetType()} from Source {inner.Source}");
            }
            Console.WriteLine("Main Program has been Done!!");
            Console.ReadKey();
        }
    }
}
