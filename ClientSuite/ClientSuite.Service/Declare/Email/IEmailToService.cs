using ClientSuite.Models;
using ClientSuite.Repo;
using System.Linq;
namespace ClientSuite.Service
{
    public interface IEmailToService
    {  
        DTResult<EmailToViewModel> GetGrid(DTParameters param);     
        EmailTo Get(int id);
        IQueryable<EmailTo> GetAll();
        void Insert(EmailTo entity);
        void Update(EmailTo entity);
        void Delete(EmailTo entity);  
    }

}

