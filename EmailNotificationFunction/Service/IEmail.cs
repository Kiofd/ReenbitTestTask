namespace EmailNotificationFunction.Service;

public interface IEmail
{
    Task SendEmail(string email, string fileName);
}