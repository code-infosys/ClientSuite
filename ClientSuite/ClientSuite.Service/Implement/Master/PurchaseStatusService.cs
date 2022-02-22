using ClientSuite.Models;
using ClientSuite.Repo;  
using ClientSuite.Data;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Linq.Dynamic.Core; 

namespace ClientSuite.Service
{
    public class PurchaseStatusService : IPurchaseStatusService
    { 
        private readonly IRepository<PurchaseStatus> _purchaseStatusRepository; 
        public PurchaseStatusService(IRepository<PurchaseStatus> purchaseStatusRepository)
        {
            _purchaseStatusRepository = purchaseStatusRepository;
        }
 
        public DTResult<PurchaseStatusViewModel> GetGrid(DTParameters param)
        { 
            var tableDataSource= _purchaseStatusRepository.GetAll().Select(m => new PurchaseStatusViewModel { Id = m.Id,Name = m.Name }).AsQueryable();

            List<String> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            var filterdData= FilterResult(param.Search.Value, tableDataSource, columnSearch, param.SearchFromLength);
            List<PurchaseStatusViewModel> data = filterdData.OrderBy(param.SortOrder).Skip(param.Start).Take(param.Length).ToList();
            int count = filterdData.Count();

            DTResult<PurchaseStatusViewModel> result = new DTResult<PurchaseStatusViewModel>
            {
                draw = param.Draw,
                data = data,
                recordsFiltered = count,
                recordsTotal = count
            };

            return result;
             
        }

        private IQueryable<PurchaseStatusViewModel> FilterResult(string search, IQueryable<PurchaseStatusViewModel> dtResult, List<string> columnFilters, int searchTake = 500)
        {
            IQueryable<PurchaseStatusViewModel> results = dtResult;
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

        public PurchaseStatus Get(int id)
        {
            return _purchaseStatusRepository.Get(id);
        }
        public void Delete(PurchaseStatus entity)
        {
            _purchaseStatusRepository.Delete(entity);
        }

        public IQueryable<PurchaseStatus> GetAll()
        {
            return _purchaseStatusRepository.GetAll();
        }

        public void Insert(PurchaseStatus entity)
        {
            _purchaseStatusRepository.Insert(entity); 
        }

        public void Update(PurchaseStatus entity)
        {
            _purchaseStatusRepository.Update(entity); 
        }
    }
}

