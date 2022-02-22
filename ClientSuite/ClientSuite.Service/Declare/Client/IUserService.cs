using ClientSuite.Models;
using ClientSuite.Repo;
using System.Linq;
namespace ClientSuite.Service
{
    public interface IUserService
    {  
        DTResult<UserViewModel> GetGrid(DTParameters param,int user,int role, int? selectRole);     
        User Get(int id);
        IQueryable<User> GetAll();
        void Insert(User entity);
        void Update(User entity);
        void Delete(User entity);  
    }

}

