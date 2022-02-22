using ClientSuite.Models;
using ClientSuite.Repo;
using System.Linq;
namespace ClientSuite.Service
{
    public interface IPurchaseStatusService
    {  
        DTResult<PurchaseStatusViewModel> GetGrid(DTParameters param);     
        PurchaseStatus Get(int id);
        IQueryable<PurchaseStatus> GetAll();
        void Insert(PurchaseStatus entity);
        void Update(PurchaseStatus entity);
        void Delete(PurchaseStatus entity);  
    }

}

