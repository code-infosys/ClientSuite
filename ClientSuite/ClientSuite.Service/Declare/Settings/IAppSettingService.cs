using ClientSuite.Models;
using ClientSuite.Repo;
using System.Linq;
namespace ClientSuite.Service
{
    public interface IAppSettingService
    {  
        DTResult<AppSettingViewModel> GetGrid(DTParameters param);     
        AppSetting Get(int id);
        IQueryable<AppSetting> GetAll();
        void Insert(AppSetting entity);
        void Update(AppSetting entity);
        void Delete(AppSetting entity);  
    }

}

