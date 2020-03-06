using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace IntroducingUserSecrets
{
    internal class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly string connectionString;

        public Worker(ILogger<Worker> logger, IOptions<SampleUserSecret> options)
        {
            _logger = logger;
            connectionString = options.Value.ConnectionString;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation($"Worker running at: {DateTimeOffset.Now}, connectionString is: {connectionString}");
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
