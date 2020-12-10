using System;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace FunctionApp1
{
    public class Function1
    {
        private readonly ILogger<Function1> _logger;

        public Function1(ILogger<Function1> logger)
        {
            _logger = logger;
        }

        [FunctionName("Function1")]
        public void Run([QueueTrigger("%QueueName%", Connection = "QueueTrigger")]string myQueueItem)
        {
            if (myQueueItem.Contains("fail"))
            {
                throw new Exception($"Oh noes! Received {myQueueItem}");
            }

            _logger.LogInformation("Received {message}", myQueueItem);
        }
    }
}
