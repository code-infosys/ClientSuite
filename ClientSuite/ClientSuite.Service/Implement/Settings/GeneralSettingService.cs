using ClientSuite.Models;
using ClientSuite.Repo;  
using ClientSuite.Data;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Linq.Dynamic.Core; 

namespace ClientSuite.Service
{
    public class GeneralSettingService : IGeneralSettingService
    { 
        private readonly IRepository<GeneralSetting> _generalSettingRepository; 
        public GeneralSettingService(IRepository<GeneralSetting> generalSettingRepository)
        {
            _generalSettingRepository = generalSettingRepository;
        }
 
        public DTResult<GeneralSettingViewModel> GetGrid(DTParameters param)
        { 
            var tableDataSource= _generalSettingRepository.GetAll().Select(m => new GeneralSettingViewModel { Id = m.Id,SettingKey = m.SettingKey,SettingValue = m.SettingValue,Description = m.Description,SettingGroup = m.SettingGroup,FieldType = m.FieldType }).AsQueryable();

            List<String> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            var filterdData= FilterResult(param.Search.Value, tableDataSource, columnSearch, param.SearchFromLength);
            List<GeneralSettingViewModel> data = filterdData.OrderBy(param.SortOrder).Skip(param.Start).Take(param.Length).ToList();
            int count = filterdData.Count();

            DTResult<GeneralSettingViewModel> result = new DTResult<GeneralSettingViewModel>
            {
                draw = param.Draw,
                data = data,
                recordsFiltered = count,
                recordsTotal = count
            };

            return result;
             
        }

        private IQueryable<GeneralSettingViewModel> FilterResult(string search, IQueryable<GeneralSettingViewModel> dtResult, List<string> columnFilters, int searchTake = 500)
        {
            IQueryable<GeneralSettingViewModel> results = dtResult;
            if (searchTake == 0)
                results = results.OrderByDescending(i => i.Id).AsQueryable();
            else
                results = results.OrderByDescending(i => i.Id).Take(searchTake).AsQueryable();

            if (!columnFilters.All(x => string.IsNullOrWhiteSpace(x)))
            {
                if (!string.IsNullOrEmpty(columnFilters[1]))
                    results = results.Where(p => p.Id.ToString().ToLower().Contains(columnFilters[1].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[2]))
                    results = results.Where(p => p.SettingKey.ToString().ToLower().Contains(columnFilters[2].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[3]))
                    results = results.Where(p => p.SettingValue.ToString().ToLower().Contains(columnFilters[3].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[4]))
                    results = results.Where(p => p.Description.ToString().ToLower().Contains(columnFilters[4].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[5]))
                    results = results.Where(p => p.SettingGroup.ToString().ToLower().Contains(columnFilters[5].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[6]))
                    results = results.Where(p => p.FieldType.ToString().ToLower().Contains(columnFilters[6].ToLower()));

            }
            return results.AsQueryable();
        }

        public GeneralSetting Get(int id)
        {
            return _generalSettingRepository.Get(id);
        }
        public void Delete(GeneralSetting entity)
        {
            _generalSettingRepository.Delete(entity);
        }

        public IQueryable<GeneralSetting> GetAll()
        {
            return _generalSettingRepository.GetAll();
        }

        public void Insert(GeneralSetting entity)
        {
            _generalSettingRepository.Insert(entity); 
        }

        public void Update(GeneralSetting entity)
        {
            _generalSettingRepository.Update(entity); 
        }
    }
}

