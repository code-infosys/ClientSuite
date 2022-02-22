using ClientSuite.Models;
using ClientSuite.Repo;
using System.Linq;
namespace ClientSuite.Service
{
    public interface INotificationService
    {  
        DTResult<NotificationViewModel> GetGrid(DTParameters param);     
        Notification Get(int id);
        IQueryable<Notification> GetAll();
        void Insert(Notification entity);
        void Update(Notification entity);
        void Delete(Notification entity);  
    }

}

