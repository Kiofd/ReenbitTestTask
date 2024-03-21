using System.Net;
using System.Net.Mail;

namespace EmailNotificationFunction.Service
{
    class Email : IEmail
    {
        private readonly int _smtpPort;
        private readonly string? _smtpPassword;
        private readonly string _smtpEmail;

        public Email(string smtpEmail, string smtpPassword, int smtpPort)
        {
            _smtpEmail = smtpEmail;
            _smtpPassword = smtpPassword;
            _smtpPort = smtpPort;
        }
        public Task SendEmail(string email, string fileName)
        {
            MailMessage message = new MailMessage();

            string name = fileName.Substring(fileName.IndexOf('_') + 1); // _ is a seperator
            
            message.From = new MailAddress(_smtpEmail);
            message.To.Add(email);
            message.Subject = "Notifications";
            message.Body = $"Your file \"{name}\" has successfully uploaded";

            SmtpClient smtpServer = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = _port,
                EnableSsl = true,
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(_smtpEmail, _smtpPassword)
            };

            try
            {
                smtpServer.SendAsync(message, null);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}