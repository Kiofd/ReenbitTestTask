using EmailNotificationFunction.Service;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace EmailNotificationFunction
{
    public class EmailNotification
    {
        private readonly ILogger<EmailNotification> _logger;

        public EmailNotification(ILogger<EmailNotification> logger)
        {
            _logger = logger;
        }

        [Function(nameof(EmailNotification))]
        public async Task Run([BlobTrigger("testcontainer/{name}", Connection = "")] Stream stream, string name,
            IDictionary<string, string> metaData)
        {
            Email email = new();
            await email.SendEmail(metaData["Email"], name);
        }
    }
}