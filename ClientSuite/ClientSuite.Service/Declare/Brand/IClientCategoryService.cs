using ClientSuite.Models;
using ClientSuite.Repo;
using System.Linq;
namespace ClientSuite.Service
{
    public interface IClientCategoryService
    {  
        DTResult<ClientCategoryViewModel> GetGrid(DTParameters param,int user,int role);     
        ClientCategory Get(int id);
        IQueryable<ClientCategory> GetAll();
        void Insert(ClientCategory entity);
        void Update(ClientCategory entity);
        void Delete(ClientCategory entity);  
    }

}

