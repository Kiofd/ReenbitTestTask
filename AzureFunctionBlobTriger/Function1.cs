using System;
using System.IO;
using System.Net.Mail;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace AzureFunctionBlobTriger
{
    public static class Function1
    {
        [FunctionName("BlobTriggerFunction")]
        public static async Task Run(
                [BlobTrigger("testcontainer/{name}", Connection = "AzureWebJobsStorage")] Stream blobStream,
                string name,
                ILogger log)
        {
            log.LogInformation($"Blob trigger function executed for blob: {name}");

            // Send email notification to user
            string userEmail = "max.sheremeta.work@gmail.com";
            string subject = "File Uploaded Notification";
            string message = $"Your file '{name}' has been successfully uploaded to Blob Storage.";

            await SendEmailNotification(userEmail, subject, message);
        }

        private static async Task SendEmailNotification(string userEmail, string subject, string message)
        {
            var apiKey = Environment.GetEnvironmentVariable("SendGridApiKey");
            var client = new SendGridClient(apiKey);

            var from = new EmailAddress("max.sheremeta228@gmail.com", "Sender Name");
            var to = new EmailAddress(userEmail);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, message, message);

            var response = await client.SendEmailAsync(msg);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception($"Failed to send email notification: {response.StatusCode}");
            }
        }
    }
}
