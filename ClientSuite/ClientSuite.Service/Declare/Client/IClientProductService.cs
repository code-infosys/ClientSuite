using ClientSuite.Models;
using ClientSuite.Repo;
using System.Linq;
namespace ClientSuite.Service
{
    public interface IClientProductService
    {  
        DTResult<ClientProductViewModel> GetGrid(DTParameters param,int user,int role);     
        ClientProduct Get(int id);
        IQueryable<ClientProduct> GetAll();
        void Insert(ClientProduct entity);
        void Update(ClientProduct entity);
        void Delete(ClientProduct entity);  
    }

}

