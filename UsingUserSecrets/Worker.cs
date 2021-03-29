using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace UsingUserSecrets
{
    internal class Worker : BackgroundService
    {
        private readonly ILogger<Worker> logger;

        //Could either store the whole object or only the string:
        private readonly SampleUserSecret sampleUserSecret;

        private readonly string mySecret;

        public Worker(ILogger<Worker> logger, IOptions<SampleUserSecret> options)
        {
            this.logger = logger;
            sampleUserSecret = options.Value;
            mySecret = options.Value.MySecret;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                logger.LogInformation($"Worker running at: {DateTimeOffset.Now}, MySecret is: {mySecret}");
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}