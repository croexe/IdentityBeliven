namespace TaskManager.Domain.Interfaces;

public interface INotificationService
{
    void Send(string recipient, string sender, string title, string status);
}