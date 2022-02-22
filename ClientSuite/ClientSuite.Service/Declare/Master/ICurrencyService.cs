using ClientSuite.Models;
using ClientSuite.Repo;
using System.Linq;
namespace ClientSuite.Service
{
    public interface ICurrencyService
    {  
        DTResult<CurrencyViewModel> GetGrid(DTParameters param);     
        Currency Get(int id);
        IQueryable<Currency> GetAll();
        void Insert(Currency entity);
        void Update(Currency entity);
        void Delete(Currency entity);  
    }

}

