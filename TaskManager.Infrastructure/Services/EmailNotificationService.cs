using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Infrastructure.Services;

public class EmailNotificationService : INotificationService
{
    /* Or put them in AppSettings */
    private const string HOST = "Some host..";
    private const int PORT = 587;
    private const string PASSWORD = "password";
    
    public void Send(string recipient, string sender, string title, string status)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(sender));
        email.To.Add(MailboxAddress.Parse(recipient));
        email.Subject = $"Task {title}";
        email.Body = new TextPart(TextFormat.Html) { Text = $"<i>{title} is {status}.<i>" };


        using var smtp = new SmtpClient();
        smtp.Connect(HOST, PORT, SecureSocketOptions.StartTls);
        smtp.Authenticate(sender, PASSWORD);
        smtp.Send(email);
        smtp.Disconnect(true);
    }
}