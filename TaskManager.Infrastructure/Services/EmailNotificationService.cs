using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using Serilog;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Infrastructure.Services;

    /// <summary>
    /// Code below is commented out, because I had test it with fake emails and it works.
    /// Without real data it doesnt work.
    /// </summary>

public class EmailNotificationService : INotificationService
{
    private readonly IConfiguration _config;
    private readonly ILogger _logger;

    public EmailNotificationService(IConfiguration config, ILogger logger)
    {
        _config = config;
        _logger = logger;
    }
    public void Send(string recipient, string sender, string title, string? status)
    {
        try
        {
            var port = int.Parse(_config["Mailer:Port"]);
            var host = _config["Mailer:Host"];
            var password = _config["Mailer:Password"];
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(sender));
            email.To.Add(MailboxAddress.Parse(recipient));
            email.Subject = $"Task {title}";
            if (status != null)
            {
                email.Body = new TextPart(TextFormat.Html) { Text = $"<i>{title} is {status}.<i>" };
            }
            else
            {
                email.Body = new TextPart(TextFormat.Html) { Text = $"<i>You have been assigned to {title}.<i>" };
            }

            Console.WriteLine($"{sender}sent an Email to {recipient}");

            using var smtp = new SmtpClient();
            smtp.Connect(host, port, SecureSocketOptions.StartTls);
            smtp.Authenticate(sender, password);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            throw;
        }
    }
}