using ClientSuite.Models;
using ClientSuite.Repo;
using System.Linq;
namespace ClientSuite.Service
{
    public interface IRoleService
    {  
        DTResult<RoleViewModel> GetGrid(DTParameters param);     
        Role Get(int id);
        IQueryable<Role> GetAll();
        void Insert(Role entity);
        void Update(Role entity);
        void Delete(Role entity);  
    }

}

