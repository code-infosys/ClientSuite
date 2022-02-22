using ClientSuite.Models; 
using ClientSuite.Repo;

namespace ClientSuite.Service
{
    public interface IMenuBarService : IRepository<MenuPermission>
    {
        MenuPermission[] GetMenuBarlist(int RoleId,int UserId);
        MenuPermission[] GetMenuBarlist();
    }
}


