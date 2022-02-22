using ClientSuite.Models;
using ClientSuite.Repo;  
using ClientSuite.Data;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Linq.Dynamic.Core; 

namespace ClientSuite.Service
{
    public class CategoryService : ICategoryService
    { 
        private readonly IRepository<Category> _categoryRepository; 
        public CategoryService(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
 
        public DTResult<CategoryViewModel> GetGrid(DTParameters param)
        { 
            var tableDataSource= _categoryRepository.GetAll().Select(m => new CategoryViewModel { Id = m.Id,Name = m.Name }).AsQueryable();

            List<String> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            var filterdData= FilterResult(param.Search.Value, tableDataSource, columnSearch, param.SearchFromLength);
            List<CategoryViewModel> data = filterdData.OrderBy(param.SortOrder).Skip(param.Start).Take(param.Length).ToList();
            int count = filterdData.Count();

            DTResult<CategoryViewModel> result = new DTResult<CategoryViewModel>
            {
                draw = param.Draw,
                data = data,
                recordsFiltered = count,
                recordsTotal = count
            };

            return result;
             
        }

        private IQueryable<CategoryViewModel> FilterResult(string search, IQueryable<CategoryViewModel> dtResult, List<string> columnFilters, int searchTake = 500)
        {
            IQueryable<CategoryViewModel> results = dtResult;
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

        public Category Get(int id)
        {
            return _categoryRepository.Get(id);
        }
        public void Delete(Category entity)
        {
            _categoryRepository.Delete(entity);
        }

        public IQueryable<Category> GetAll()
        {
            return _categoryRepository.GetAll();
        }

        public void Insert(Category entity)
        {
            _categoryRepository.Insert(entity); 
        }

        public void Update(Category entity)
        {
            _categoryRepository.Update(entity); 
        }
    }
}

