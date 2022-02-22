using ClientSuite.Models;
using ClientSuite.Repo;
using System.Linq;
namespace ClientSuite.Service
{
    public interface IProductService
    {  
        DTResult<ProductViewModel> GetGrid(DTParameters param);     
        Product Get(int id);
        IQueryable<Product> GetAll();
        void Insert(Product entity);
        void Update(Product entity);
        void Delete(Product entity);  
    }

}

