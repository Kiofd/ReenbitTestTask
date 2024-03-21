using EmailNotificationFunction.Service;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddSingleton<IEmail, Email>(p => new Email(
            Environment.GetEnvironmentVariable("SmtpEmail")?? string.Empty,
            Environment.GetEnvironmentVariable("SmtpPassword") ?? string.Empty,
            Int32.Parse(Environment.GetEnvironmentVariable("SmtpPort")))
             );
    })
    .Build();

host.Run();