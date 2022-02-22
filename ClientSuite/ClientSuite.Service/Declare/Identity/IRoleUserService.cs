using ClientSuite.Models;
using ClientSuite.Repo;
using System.Linq;
namespace ClientSuite.Service
{
    public interface IRoleUserService
    {  
        DTResult<RoleUserViewModel> GetGrid(DTParameters param,int user,int role);     
        RoleUser Get(int id);
        IQueryable<RoleUser> GetAll();
        void Insert(RoleUser entity);
        void Update(RoleUser entity);
        void Delete(RoleUser entity);  
    }

}

