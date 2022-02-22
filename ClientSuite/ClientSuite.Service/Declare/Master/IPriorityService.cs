using ClientSuite.Models;
using ClientSuite.Repo;
using System.Linq;
namespace ClientSuite.Service
{
    public interface IPriorityService
    {  
        DTResult<PriorityViewModel> GetGrid(DTParameters param);     
        Priority Get(int id);
        IQueryable<Priority> GetAll();
        void Insert(Priority entity);
        void Update(Priority entity);
        void Delete(Priority entity);  
    }

}

