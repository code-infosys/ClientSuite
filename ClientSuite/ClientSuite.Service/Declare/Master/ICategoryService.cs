using ClientSuite.Models;
using ClientSuite.Repo;
using System.Linq;
namespace ClientSuite.Service
{
    public interface ICategoryService
    {  
        DTResult<CategoryViewModel> GetGrid(DTParameters param);     
        Category Get(int id);
        IQueryable<Category> GetAll();
        void Insert(Category entity);
        void Update(Category entity);
        void Delete(Category entity);  
    }

}

