using ClientSuite.Models;
using ClientSuite.Repo;  
using ClientSuite.Data;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Linq.Dynamic.Core; 

namespace ClientSuite.Service
{
    public class RoleService : IRoleService
    { 
        private readonly IRepository<Role> _roleRepository; 
        public RoleService(IRepository<Role> roleRepository)
        {
            _roleRepository = roleRepository;
        }
 
        public DTResult<RoleViewModel> GetGrid(DTParameters param)
        { 
            var tableDataSource= _roleRepository.GetAll().Select(m => new RoleViewModel { Id = m.Id,RoleName = m.RoleName,IsActive = m.IsActive }).AsQueryable();

            List<String> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            var filterdData= FilterResult(param.Search.Value, tableDataSource, columnSearch, param.SearchFromLength);
            List<RoleViewModel> data = filterdData.OrderBy(param.SortOrder).Skip(param.Start).Take(param.Length).ToList();
            int count = filterdData.Count();

            DTResult<RoleViewModel> result = new DTResult<RoleViewModel>
            {
                draw = param.Draw,
                data = data,
                recordsFiltered = count,
                recordsTotal = count
            };

            return result;
             
        }

        private IQueryable<RoleViewModel> FilterResult(string search, IQueryable<RoleViewModel> dtResult, List<string> columnFilters, int searchTake = 500)
        {
            IQueryable<RoleViewModel> results = dtResult;
            if (searchTake == 0)
                results = results.OrderByDescending(i => i.Id).AsQueryable();
            else
                results = results.OrderByDescending(i => i.Id).Take(searchTake).AsQueryable();

            if (!columnFilters.All(x => string.IsNullOrWhiteSpace(x)))
            {
                if (!string.IsNullOrEmpty(columnFilters[1]))
                    results = results.Where(p => p.Id.ToString().ToLower().Contains(columnFilters[1].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[2]))
                    results = results.Where(p => p.RoleName.ToString().ToLower().Contains(columnFilters[2].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[3]))
                    results = results.Where(p => p.IsActive.ToString().ToLower().Contains(columnFilters[3].ToLower()));

            }
            return results.AsQueryable();
        }

        public Role Get(int id)
        {
            return _roleRepository.Get(id);
        }
        public void Delete(Role entity)
        {
            _roleRepository.Delete(entity);
        }

        public IQueryable<Role> GetAll()
        {
            return _roleRepository.GetAll();
        }

        public void Insert(Role entity)
        {
            _roleRepository.Insert(entity); 
        }

        public void Update(Role entity)
        {
            _roleRepository.Update(entity); 
        }
    }
}

