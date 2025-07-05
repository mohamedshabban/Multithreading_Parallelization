namespace Demo
{
    /*
     5 - CancellationTokenDemo
     */
    public class CancellationTokenDemo
    {
        static void CancellationTokenDemoFn()
        {
            CancellationTokenSource cancellationToken = new CancellationTokenSource();

            var task = new Task(() =>
            {
                int i = 0;
                while (true)
                {
                    //if (cancellationToken.IsCancellationRequested)
                    //    //break;
                    //    throw new OperationCanceledException();
                    cancellationToken.Token.ThrowIfCancellationRequested();//like break;
                    Console.WriteLine($"{i++}\t");
                }
            }, cancellationToken.Token);
            task.Start();
            Console.ReadKey();
            cancellationToken.Cancel();
            Console.WriteLine("Task cancelled");
        }
    }
}
