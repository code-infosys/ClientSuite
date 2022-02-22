using ClientSuite.Models;
using ClientSuite.Repo;
using System.Linq;
namespace ClientSuite.Service
{
    public interface IEmailMessageService
    {  
        DTResult<EmailMessageViewModel> GetGrid(DTParameters param);     
        EmailMessage Get(int id);
        IQueryable<EmailMessage> GetAll();
        void Insert(EmailMessage entity);
        void Update(EmailMessage entity);
        void Delete(EmailMessage entity);  
    }

}

