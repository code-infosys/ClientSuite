using ClientSuite.Models;
using ClientSuite.Repo;  
using ClientSuite.Data;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Linq.Dynamic.Core; 

namespace ClientSuite.Service
{
    public class MenuPermissionService : IMenuPermissionService
    { 
        private readonly IRepository<MenuPermission> _menuPermissionRepository; 
        public MenuPermissionService(IRepository<MenuPermission> menuPermissionRepository)
        {
            _menuPermissionRepository = menuPermissionRepository;
        }
 
        public DTResult<MenuPermissionViewModel> GetGrid(DTParameters param,int user,int role)
        { 
            var conditionData = _menuPermissionRepository.GetAll().AsQueryable();
            if (role == Constants.AdminRole)
                conditionData = _menuPermissionRepository.GetAll().AsQueryable();
            else
            {
                conditionData = _menuPermissionRepository.GetAll().Where(i => i.UserId == user).AsQueryable();
            }

            var tableDataSource = conditionData.Select(m => new MenuPermissionViewModel { Id = m.Id,SortOrder = m.SortOrder,IsCreate = m.IsCreate,IsRead = m.IsRead,IsUpdate = m.IsUpdate,IsDelete = m.IsDelete,IsActive = m.IsActive,RoleId = m.Role_RoleId.RoleName,UserId = m.User_UserId.UserName,MenuId = m.Menu_MenuId.MenuText }).AsQueryable();

            List<String> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            var filterdData= FilterResult(param.Search.Value, tableDataSource, columnSearch, param.SearchFromLength);
            List<MenuPermissionViewModel> data = filterdData.OrderBy(param.SortOrder).Skip(param.Start).Take(param.Length).ToList();
            int count = filterdData.Count();

            DTResult<MenuPermissionViewModel> result = new DTResult<MenuPermissionViewModel>
            {
                draw = param.Draw,
                data = data,
                recordsFiltered = count,
                recordsTotal = count
            };

            return result;
             
        }

        private IQueryable<MenuPermissionViewModel> FilterResult(string search, IQueryable<MenuPermissionViewModel> dtResult, List<string> columnFilters, int searchTake = 500)
        {
            IQueryable<MenuPermissionViewModel> results = dtResult;
            if (searchTake == 0)
                results = results.OrderByDescending(i => i.Id).AsQueryable();
            else
                results = results.OrderByDescending(i => i.Id).Take(searchTake).AsQueryable();

            if (!columnFilters.All(x => string.IsNullOrWhiteSpace(x)))
            {
                if (!string.IsNullOrEmpty(columnFilters[1]))
                    results = results.Where(p => p.Id.ToString().ToLower().Contains(columnFilters[1].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[2]))
                    results = results.Where(p => p.SortOrder.ToString().ToLower().Contains(columnFilters[2].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[3]))
                    results = results.Where(p => p.IsCreate.ToString().ToLower().Contains(columnFilters[3].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[4]))
                    results = results.Where(p => p.IsRead.ToString().ToLower().Contains(columnFilters[4].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[5]))
                    results = results.Where(p => p.IsUpdate.ToString().ToLower().Contains(columnFilters[5].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[6]))
                    results = results.Where(p => p.IsDelete.ToString().ToLower().Contains(columnFilters[6].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[7]))
                    results = results.Where(p => p.IsActive.ToString().ToLower().Contains(columnFilters[7].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[8]))
                    results = results.Where(p => p.RoleId.ToString().ToLower().Contains(columnFilters[8].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[9]))
                    results = results.Where(p => p.UserId.ToString().ToLower().Contains(columnFilters[9].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[10]))
                    results = results.Where(p => p.MenuId.ToString().ToLower().Contains(columnFilters[10].ToLower()));

            }
            return results.AsQueryable();
        }

        public MenuPermission Get(int id)
        {
            return _menuPermissionRepository.Get(id);
        }
        public void Delete(MenuPermission entity)
        {
            _menuPermissionRepository.Delete(entity);
        }

        public IQueryable<MenuPermission> GetAll()
        {
            return _menuPermissionRepository.GetAll();
        }

        public void Insert(MenuPermission entity)
        {
            _menuPermissionRepository.Insert(entity); 
        }

        public void Update(MenuPermission entity)
        {
            _menuPermissionRepository.Update(entity); 
        }
    }
}

