using ClientSuite.Models;
using ClientSuite.Repo;
using System.Linq;
namespace ClientSuite.Service
{
    public interface IInvoiceService
    {  
        DTResult<InvoiceViewModel> GetGrid(DTParameters param,int user,int role, bool isQuote);     
        Invoice Get(int id);
        IQueryable<Invoice> GetAll();
        void Insert(Invoice entity);
        void Update(Invoice entity);
        void Delete(Invoice entity);  
    }

}

