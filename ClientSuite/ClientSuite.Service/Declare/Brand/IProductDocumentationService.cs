using ClientSuite.Models;
using ClientSuite.Repo;
using System.Linq;
namespace ClientSuite.Service
{
    public interface IProductDocumentationService
    {  
        DTResult<ProductDocumentationViewModel> GetGrid(DTParameters param);     
        ProductDocumentation Get(int id);
        IQueryable<ProductDocumentation> GetAll();
        void Insert(ProductDocumentation entity);
        void Update(ProductDocumentation entity);
        void Delete(ProductDocumentation entity);  
    }

}

