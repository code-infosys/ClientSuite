using ClientSuite.Models;
using ClientSuite.Repo;
using System.Linq;
namespace ClientSuite.Service
{
    public interface IMenuPermissionService
    {  
        DTResult<MenuPermissionViewModel> GetGrid(DTParameters param,int user,int role);     
        MenuPermission Get(int id);
        IQueryable<MenuPermission> GetAll();
        void Insert(MenuPermission entity);
        void Update(MenuPermission entity);
        void Delete(MenuPermission entity);  
    }

}

