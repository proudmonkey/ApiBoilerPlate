using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CoreBoilerPlate.Worker
{
    public class MessageQueueProcessorService: BackgroundService
    {
        private readonly ILogger<MessageQueueProcessorService> _logger;

        public MessageQueueProcessorService(ILogger<MessageQueueProcessorService> logger)
        {
            _logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogDebug($"MessageQueueProcessorService is starting.");

            stoppingToken.Register(() => _logger.LogDebug($" MessageQueueProcessorService background task is stopping."));

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogDebug($"MessageQueueProcessorService task doing background work.");

                //TO DO:
                //PubSub/Message Queue subcription and process message
                //Save to DB

                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }

            _logger.LogDebug($"MessageQueueProcessorService background task is stopping.");
        }
    }
}
