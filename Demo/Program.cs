namespace Demo
{
    public class Program
    {
        static void Main(string[] args)
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
