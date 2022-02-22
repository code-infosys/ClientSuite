using ClientSuite.Models;
using ClientSuite.Repo;  
using ClientSuite.Data;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Linq.Dynamic.Core; 

namespace ClientSuite.Service
{
    public class CurrencyService : ICurrencyService
    { 
        private readonly IRepository<Currency> _currencyRepository; 
        public CurrencyService(IRepository<Currency> currencyRepository)
        {
            _currencyRepository = currencyRepository;
        }
 
        public DTResult<CurrencyViewModel> GetGrid(DTParameters param)
        { 
            var tableDataSource= _currencyRepository.GetAll().Select(m => new CurrencyViewModel { Id = m.Id,Name = m.Name,Code = m.Code }).AsQueryable();

            List<String> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            var filterdData= FilterResult(param.Search.Value, tableDataSource, columnSearch, param.SearchFromLength);
            List<CurrencyViewModel> data = filterdData.OrderBy(param.SortOrder).Skip(param.Start).Take(param.Length).ToList();
            int count = filterdData.Count();

            DTResult<CurrencyViewModel> result = new DTResult<CurrencyViewModel>
            {
                draw = param.Draw,
                data = data,
                recordsFiltered = count,
                recordsTotal = count
            };

            return result;
             
        }

        private IQueryable<CurrencyViewModel> FilterResult(string search, IQueryable<CurrencyViewModel> dtResult, List<string> columnFilters, int searchTake = 500)
        {
            IQueryable<CurrencyViewModel> results = dtResult;
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

        public Currency Get(int id)
        {
            return _currencyRepository.Get(id);
        }
        public void Delete(Currency entity)
        {
            _currencyRepository.Delete(entity);
        }

        public IQueryable<Currency> GetAll()
        {
            return _currencyRepository.GetAll();
        }

        public void Insert(Currency entity)
        {
            _currencyRepository.Insert(entity); 
        }

        public void Update(Currency entity)
        {
            _currencyRepository.Update(entity); 
        }
    }
}

