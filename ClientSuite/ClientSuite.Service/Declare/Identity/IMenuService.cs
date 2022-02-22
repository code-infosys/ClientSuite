using ClientSuite.Models;
using ClientSuite.Repo;
using System.Linq;
namespace ClientSuite.Service
{
    public interface IMenuService
    {  
        DTResult<MenuViewModel> GetGrid(DTParameters param);     
        Menu Get(int id);
        IQueryable<Menu> GetAll();
        void Insert(Menu entity);
        void Update(Menu entity);
        void Delete(Menu entity);  
    }

}

