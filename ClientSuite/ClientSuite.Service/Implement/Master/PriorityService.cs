using ClientSuite.Models;
using ClientSuite.Repo;  
using ClientSuite.Data;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Linq.Dynamic.Core; 

namespace ClientSuite.Service
{
    public class PriorityService : IPriorityService
    { 
        private readonly IRepository<Priority> _priorityRepository; 
        public PriorityService(IRepository<Priority> priorityRepository)
        {
            _priorityRepository = priorityRepository;
        }
 
        public DTResult<PriorityViewModel> GetGrid(DTParameters param)
        { 
            var tableDataSource= _priorityRepository.GetAll().Select(m => new PriorityViewModel { Id = m.Id,Name = m.Name }).AsQueryable();

            List<String> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            var filterdData= FilterResult(param.Search.Value, tableDataSource, columnSearch, param.SearchFromLength);
            List<PriorityViewModel> data = filterdData.OrderBy(param.SortOrder).Skip(param.Start).Take(param.Length).ToList();
            int count = filterdData.Count();

            DTResult<PriorityViewModel> result = new DTResult<PriorityViewModel>
            {
                draw = param.Draw,
                data = data,
                recordsFiltered = count,
                recordsTotal = count
            };

            return result;
             
        }

        private IQueryable<PriorityViewModel> FilterResult(string search, IQueryable<PriorityViewModel> dtResult, List<string> columnFilters, int searchTake = 500)
        {
            IQueryable<PriorityViewModel> results = dtResult;
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

            }
            return results.AsQueryable();
        }

        public Priority Get(int id)
        {
            return _priorityRepository.Get(id);
        }
        public void Delete(Priority entity)
        {
            _priorityRepository.Delete(entity);
        }

        public IQueryable<Priority> GetAll()
        {
            return _priorityRepository.GetAll();
        }

        public void Insert(Priority entity)
        {
            _priorityRepository.Insert(entity); 
        }

        public void Update(Priority entity)
        {
            _priorityRepository.Update(entity); 
        }
    }
}

