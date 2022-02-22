using ClientSuite.Models;
using ClientSuite.Repo;
using System.Linq;
namespace ClientSuite.Service
{
    public interface IGeneralSettingService
    {  
        DTResult<GeneralSettingViewModel> GetGrid(DTParameters param);     
        GeneralSetting Get(int id);
        IQueryable<GeneralSetting> GetAll();
        void Insert(GeneralSetting entity);
        void Update(GeneralSetting entity);
        void Delete(GeneralSetting entity);  
    }

}

