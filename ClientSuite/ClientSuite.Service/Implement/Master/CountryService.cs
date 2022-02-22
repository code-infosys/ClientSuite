using ClientSuite.Models;
using ClientSuite.Repo;  
using ClientSuite.Data;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Linq.Dynamic.Core; 

namespace ClientSuite.Service
{
    public class CountryService : ICountryService
    { 
        private readonly IRepository<Country> _countryRepository; 
        public CountryService(IRepository<Country> countryRepository)
        {
            _countryRepository = countryRepository;
        }
 
        public DTResult<CountryViewModel> GetGrid(DTParameters param)
        { 
            var tableDataSource= _countryRepository.GetAll().Select(m => new CountryViewModel { Id = m.Id,Name = m.Name,Code = m.Code }).AsQueryable();

            List<String> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            var filterdData= FilterResult(param.Search.Value, tableDataSource, columnSearch, param.SearchFromLength);
            List<CountryViewModel> data = filterdData.OrderBy(param.SortOrder).Skip(param.Start).Take(param.Length).ToList();
            int count = filterdData.Count();

            DTResult<CountryViewModel> result = new DTResult<CountryViewModel>
            {
                draw = param.Draw,
                data = data,
                recordsFiltered = count,
                recordsTotal = count
            };

            return result;
             
        }

        private IQueryable<CountryViewModel> FilterResult(string search, IQueryable<CountryViewModel> dtResult, List<string> columnFilters, int searchTake = 500)
        {
            IQueryable<CountryViewModel> results = dtResult;
            if (searchTake == 0)
                results = results.OrderByDescending(i => i.Id).AsQueryable();
            else
                results = results.OrderByDescending(i => i.Id).Take(searchTake).AsQueryable();

            if (!columnFilters.All(x => string.IsNullOrWhiteSpace(x)))
            {
                if (!string.IsNullOrEmpty(columnFilters[1]))
                    results = results.Where(p => p.Id.ToString().ToLower().Contains(columnFilters[1].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[2]))
                    results = results.Where(p => p.Name.ToString().ToLower().Contains(columnFilters[2].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[3]))
                    results = results.Where(p => p.Code.ToString().ToLower().Contains(columnFilters[3].ToLower()));

            }
            return results.AsQueryable();
        }

        public Country Get(int id)
        {
            return _countryRepository.Get(id);
        }
        public void Delete(Country entity)
        {
            _countryRepository.Delete(entity);
        }

        public IQueryable<Country> GetAll()
        {
            return _countryRepository.GetAll();
        }

        public void Insert(Country entity)
        {
            _countryRepository.Insert(entity); 
        }

        public void Update(Country entity)
        {
            _countryRepository.Update(entity); 
        }
    }
}

