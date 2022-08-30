namespace TaskManager.Domain.Interfaces;

public interface INotificationService
{
    void SendAsync(string recipient, string sender, string title, string? message);
}