using ClientSuite.Models;
using ClientSuite.Repo;
using System.Linq;
namespace ClientSuite.Service
{
    public interface IContactsService
    {  
        DTResult<ContactsViewModel> GetGrid(DTParameters param);     
        Contacts Get(int id);
        IQueryable<Contacts> GetAll();
        void Insert(Contacts entity);
        void Update(Contacts entity);
        void Delete(Contacts entity);  
    }

}

