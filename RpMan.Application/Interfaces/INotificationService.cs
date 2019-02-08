using RpMan.Application.Notifications.Models;
using System.Threading.Tasks;

namespace RpMan.Application.Interfaces
{
    public interface INotificationService
    {
        Task SendAsync(Message message);
    }
}
