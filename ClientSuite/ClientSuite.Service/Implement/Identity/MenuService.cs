using ClientSuite.Models;
using ClientSuite.Repo;  
using ClientSuite.Data;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Linq.Dynamic.Core; 

namespace ClientSuite.Service
{
    public class MenuService : IMenuService
    { 
        private readonly IRepository<Menu> _menuRepository; 
        public MenuService(IRepository<Menu> menuRepository)
        {
            _menuRepository = menuRepository;
        }
 
        public DTResult<MenuViewModel> GetGrid(DTParameters param)
        { 
            var tableDataSource= _menuRepository.GetAll().Select(m => new MenuViewModel { Id = m.Id,MenuText = m.MenuText,MenuURL = m.MenuURL,SortOrder = m.SortOrder,MenuIcon = m.MenuIcon,ParentId = m.Menu2.MenuText }).AsQueryable();

            List<String> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            var filterdData= FilterResult(param.Search.Value, tableDataSource, columnSearch, param.SearchFromLength);
            List<MenuViewModel> data = filterdData.OrderBy(param.SortOrder).Skip(param.Start).Take(param.Length).ToList();
            int count = filterdData.Count();

            DTResult<MenuViewModel> result = new DTResult<MenuViewModel>
            {
                draw = param.Draw,
                data = data,
                recordsFiltered = count,
                recordsTotal = count
            };

            return result;
             
        }

        private IQueryable<MenuViewModel> FilterResult(string search, IQueryable<MenuViewModel> dtResult, List<string> columnFilters, int searchTake = 500)
        {
            IQueryable<MenuViewModel> results = dtResult;
            if (searchTake == 0)
                results = results.OrderByDescending(i => i.Id).AsQueryable();
            else
                results = results.OrderByDescending(i => i.Id).Take(searchTake).AsQueryable();

            if (!columnFilters.All(x => string.IsNullOrWhiteSpace(x)))
            {
                if (!string.IsNullOrEmpty(columnFilters[1]))
                    results = results.Where(p => p.Id.ToString().ToLower().Contains(columnFilters[1].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[2]))
                    results = results.Where(p => p.MenuText.ToString().ToLower().Contains(columnFilters[2].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[3]))
                    results = results.Where(p => p.MenuURL.ToString().ToLower().Contains(columnFilters[3].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[4]))
                    results = results.Where(p => p.SortOrder.ToString().ToLower().Contains(columnFilters[4].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[5]))
                    results = results.Where(p => p.MenuIcon.ToString().ToLower().Contains(columnFilters[5].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[6]))
                    results = results.Where(p => p.ParentId.ToString().ToLower().Contains(columnFilters[6].ToLower()));

            }
            return results.AsQueryable();
        }

        public Menu Get(int id)
        {
            return _menuRepository.Get(id);
        }
        public void Delete(Menu entity)
        {
            _menuRepository.Delete(entity);
        }

        public IQueryable<Menu> GetAll()
        {
            return _menuRepository.GetAll();
        }

        public void Insert(Menu entity)
        {
            _menuRepository.Insert(entity); 
        }

        public void Update(Menu entity)
        {
            _menuRepository.Update(entity); 
        }
    }
}

