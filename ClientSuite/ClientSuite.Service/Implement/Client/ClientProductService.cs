using ClientSuite.Models;
using ClientSuite.Repo;  
using ClientSuite.Data;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Linq.Dynamic.Core; 

namespace ClientSuite.Service
{
    public class ClientProductService : IClientProductService
    { 
        private readonly IRepository<ClientProduct> _clientProductRepository; 
        public ClientProductService(IRepository<ClientProduct> clientProductRepository)
        {
            _clientProductRepository = clientProductRepository;
        }
 
        public DTResult<ClientProductViewModel> GetGrid(DTParameters param,int user,int role)
        { 
            var conditionData = _clientProductRepository.GetAll().AsQueryable();
            if (role == Constants.AdminRole)
                conditionData = _clientProductRepository.GetAll().AsQueryable();
            else
            {
                conditionData = _clientProductRepository.GetAll().Where(i => i.UserId == user).AsQueryable();
            }

            var tableDataSource = conditionData.Select(m => new ClientProductViewModel { Id = m.Id,AddedBy = m.AddedBy,DateAdded = m.DateAdded,ModifiedBy = m.ModifiedBy,DateModified = m.DateModified,FollowUpDate = m.FollowUpDate,UserId = m.User_UserId.UserName,ProductId = m.Product_ProductId.Name,PurchaseStatusId = m.PurchaseStatus_PurchaseStatusId.Name }).AsQueryable();

            List<String> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            var filterdData= FilterResult(param.Search.Value, tableDataSource, columnSearch, param.SearchFromLength);
            List<ClientProductViewModel> data = filterdData.OrderBy(param.SortOrder).Skip(param.Start).Take(param.Length).ToList();
            int count = filterdData.Count();

            DTResult<ClientProductViewModel> result = new DTResult<ClientProductViewModel>
            {
                draw = param.Draw,
                data = data,
                recordsFiltered = count,
                recordsTotal = count
            };

            return result;
             
        }

        private IQueryable<ClientProductViewModel> FilterResult(string search, IQueryable<ClientProductViewModel> dtResult, List<string> columnFilters, int searchTake = 500)
        {
            IQueryable<ClientProductViewModel> results = dtResult;
            if (searchTake == 0)
                results = results.OrderByDescending(i => i.Id).AsQueryable();
            else
                results = results.OrderByDescending(i => i.Id).Take(searchTake).AsQueryable();

            if (!columnFilters.All(x => string.IsNullOrWhiteSpace(x)))
            {
                if (!string.IsNullOrEmpty(columnFilters[1]))
                    results = results.Where(p => p.Id.ToString().ToLower().Contains(columnFilters[1].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[2]))
                    results = results.Where(p => p.AddedBy.ToString().ToLower().Contains(columnFilters[2].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[3]))
                    results = results.Where(p => p.DateAdded.ToString().ToLower().Contains(columnFilters[3].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[4]))
                    results = results.Where(p => p.ModifiedBy.ToString().ToLower().Contains(columnFilters[4].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[5]))
                    results = results.Where(p => p.DateModified.ToString().ToLower().Contains(columnFilters[5].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[6]))
                    results = results.Where(p => p.FollowUpDate.ToString().ToLower().Contains(columnFilters[6].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[7]))
                    results = results.Where(p => p.UserId.ToString().ToLower().Contains(columnFilters[7].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[8]))
                    results = results.Where(p => p.ProductId.ToString().ToLower().Contains(columnFilters[8].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[9]))
                    results = results.Where(p => p.PurchaseStatusId.ToString().ToLower().Contains(columnFilters[9].ToLower()));

            }
            return results.AsQueryable();
        }

        public ClientProduct Get(int id)
        {
            return _clientProductRepository.Get(id);
        }
        public void Delete(ClientProduct entity)
        {
            _clientProductRepository.Delete(entity);
        }

        public IQueryable<ClientProduct> GetAll()
        {
            return _clientProductRepository.GetAll();
        }

        public void Insert(ClientProduct entity)
        {
            _clientProductRepository.Insert(entity); 
        }

        public void Update(ClientProduct entity)
        {
            _clientProductRepository.Update(entity); 
        }
    }
}

