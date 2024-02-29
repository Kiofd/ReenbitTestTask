using System.Net;
using System.Net.Mail;

namespace EmailNotificationFunction.Service
{
    class Email
    {
        private readonly int _port = 587;
        private readonly string _smtpPassword = "bgfstzlzqjetcmbc";
        private readonly string _smtpEmail = "noreply.maksym.sheremeta@gmail.com";

        public async Task SendEmail(string email, string fileName)
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