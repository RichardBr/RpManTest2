using RpMan.Application.Interfaces;
using RpMan.Application.Notifications.Models;
using System.Threading.Tasks;

namespace RpMan.Infrastructure
{
    public class NotificationService : INotificationService
    {
        public Task SendAsync(Message message)
        {
            return Task.CompletedTask;
        }
    }
}
