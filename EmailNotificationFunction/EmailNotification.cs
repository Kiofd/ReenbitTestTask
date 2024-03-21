using EmailNotificationFunction.Service;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace EmailNotificationFunction
{
    public class EmailNotification
    {
        private readonly ILogger<EmailNotification> _logger;
        private readonly IEmail email;


        public EmailNotification(ILogger<EmailNotification> logger, IEmail email)
        {
            _logger = logger;
            this.email = email;
        }

        [Function(nameof(EmailNotification))]
        public async Task Run([BlobTrigger("testcontainer/{name}", Connection = "")] Stream stream, string name,
            IDictionary<string, string> metaData)
        {
            //Email email = new();
            await email.SendEmail(metaData["Email"], name);
        }
    }
}