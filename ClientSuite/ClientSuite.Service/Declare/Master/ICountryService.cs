using ClientSuite.Models;
using ClientSuite.Repo;
using System.Linq;
namespace ClientSuite.Service
{
    public interface ICountryService
    {  
        DTResult<CountryViewModel> GetGrid(DTParameters param);     
        Country Get(int id);
        IQueryable<Country> GetAll();
        void Insert(Country entity);
        void Update(Country entity);
        void Delete(Country entity);  
    }

}

