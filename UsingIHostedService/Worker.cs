using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Hosting;

namespace UsingIHostedService
{
    //Because we use a timer in the example we add IDisposable so we can Dispose of it afterwards
    internal class Worker : IHostedService, IDisposable
    {
        private Timer timer;

        public Worker() =>
            Console.WriteLine("Constructing Service");

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("StartAsync");
            //Register a timer that will call the method DoWork each fifth second
            timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("StopAsync");
            //Set the timer to stop trigger
            timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            Console.WriteLine("Dispose");

            timer?.Dispose();
        }

        //Using DateTimeOffset instead of DateTime to get UTC time
        private void DoWork(object state) =>
            Console.WriteLine($"{DateTimeOffset.Now} - Doing work");

    }
}
