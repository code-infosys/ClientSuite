using ClientSuite.Models; 
using System.Threading.Tasks;
using ClientSuite.Repo;
namespace ClientSuite.Service
{
    public interface IAuthLoginService : IRepository<User>
    { 
        Task<(bool, User)> ValidateUserCredentialsAsync(string username, string password);
        Task<(bool, User)> ValidateUserCredentialsAsync(string username);
    }
}


