using ClientSuite.Models;
using ClientSuite.Repo;
using System.Linq;
namespace ClientSuite.Service
{
    public interface ITicketService
    {  
        DTResult<TicketViewModel> GetGrid(DTParameters param);     
        Ticket Get(int id);
        IQueryable<Ticket> GetAll();
        void Insert(Ticket entity);
        void Update(Ticket entity);
        void Delete(Ticket entity);  
    }

}

