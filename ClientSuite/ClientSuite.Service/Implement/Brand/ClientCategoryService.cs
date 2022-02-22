using ClientSuite.Models;
using ClientSuite.Repo;  
using ClientSuite.Data;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Linq.Dynamic.Core; 

namespace ClientSuite.Service
{
    public class ClientCategoryService : IClientCategoryService
    { 
        private readonly IRepository<ClientCategory> _clientCategoryRepository; 
        public ClientCategoryService(IRepository<ClientCategory> clientCategoryRepository)
        {
            _clientCategoryRepository = clientCategoryRepository;
        }
 
        public DTResult<ClientCategoryViewModel> GetGrid(DTParameters param,int user,int role)
        { 
            var conditionData = _clientCategoryRepository.GetAll().AsQueryable();
            if (role == Constants.AdminRole)
                conditionData = _clientCategoryRepository.GetAll().AsQueryable();
            else
            {
                conditionData = _clientCategoryRepository.GetAll().Where(i => i.UserId == user).AsQueryable();
            }

            var tableDataSource = conditionData.Select(m => new ClientCategoryViewModel { Id = m.Id,AddedBy = m.AddedBy,DateAdded = m.DateAdded,ModifiedBy = m.ModifiedBy,DateModified = m.DateModified,UserId = m.User_UserId.UserName,CategoryId = m.Category_CategoryId.Name }).AsQueryable();

            List<String> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            var filterdData= FilterResult(param.Search.Value, tableDataSource, columnSearch, param.SearchFromLength);
            List<ClientCategoryViewModel> data = filterdData.OrderBy(param.SortOrder).Skip(param.Start).Take(param.Length).ToList();
            int count = filterdData.Count();

            DTResult<ClientCategoryViewModel> result = new DTResult<ClientCategoryViewModel>
            {
                draw = param.Draw,
                data = data,
                recordsFiltered = count,
                recordsTotal = count
            };

            return result;
             
        }

        private IQueryable<ClientCategoryViewModel> FilterResult(string search, IQueryable<ClientCategoryViewModel> dtResult, List<string> columnFilters, int searchTake = 500)
        {
            IQueryable<ClientCategoryViewModel> results = dtResult;
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
                    results = results.Where(p => p.UserId.ToString().ToLower().Contains(columnFilters[6].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[7]))
                    results = results.Where(p => p.CategoryId.ToString().ToLower().Contains(columnFilters[7].ToLower()));

            }
            return results.AsQueryable();
        }

        public ClientCategory Get(int id)
        {
            return _clientCategoryRepository.Get(id);
        }
        public void Delete(ClientCategory entity)
        {
            _clientCategoryRepository.Delete(entity);
        }

        public IQueryable<ClientCategory> GetAll()
        {
            return _clientCategoryRepository.GetAll();
        }

        public void Insert(ClientCategory entity)
        {
            _clientCategoryRepository.Insert(entity); 
        }

        public void Update(ClientCategory entity)
        {
            _clientCategoryRepository.Update(entity); 
        }
    }
}

