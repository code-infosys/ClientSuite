using ClientSuite.Models;
using ClientSuite.Repo;  
using ClientSuite.Data;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Linq.Dynamic.Core; 

namespace ClientSuite.Service
{
    public class ProductService : IProductService
    { 
        private readonly IRepository<Product> _productRepository; 
        public ProductService(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }
 
        public DTResult<ProductViewModel> GetGrid(DTParameters param)
        { 
            var tableDataSource= _productRepository.GetAll().Select(m => new ProductViewModel { IsActive = m.IsActive,Id = m.Id,Name = m.Name,Picture = m.Picture,Price = m.Price,Description = m.Description }).AsQueryable();

            List<String> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            var filterdData= FilterResult(param.Search.Value, tableDataSource, columnSearch, param.SearchFromLength);
            List<ProductViewModel> data = filterdData.OrderBy(param.SortOrder).Skip(param.Start).Take(param.Length).ToList();
            int count = filterdData.Count();

            DTResult<ProductViewModel> result = new DTResult<ProductViewModel>
            {
                draw = param.Draw,
                data = data,
                recordsFiltered = count,
                recordsTotal = count
            };

            return result;
             
        }

        private IQueryable<ProductViewModel> FilterResult(string search, IQueryable<ProductViewModel> dtResult, List<string> columnFilters, int searchTake = 500)
        {
            IQueryable<ProductViewModel> results = dtResult;
            if (searchTake == 0)
                results = results.OrderByDescending(i => i.Id).AsQueryable();
            else
                results = results.OrderByDescending(i => i.Id).Take(searchTake).AsQueryable();

            if (!columnFilters.All(x => string.IsNullOrWhiteSpace(x)))
            {
                if (!string.IsNullOrEmpty(columnFilters[1]))
                    results = results.Where(p => p.IsActive.ToString().ToLower().Contains(columnFilters[1].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[2]))
                    results = results.Where(p => p.Id.ToString().ToLower().Contains(columnFilters[2].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[3]))
                    results = results.Where(p => p.Name.ToString().ToLower().Contains(columnFilters[3].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[4]))
                    results = results.Where(p => p.Picture.ToString().ToLower().Contains(columnFilters[4].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[5]))
                    results = results.Where(p => p.Price.ToString().ToLower().Contains(columnFilters[5].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[6]))
                    results = results.Where(p => p.Description.ToString().ToLower().Contains(columnFilters[6].ToLower()));

            }
            return results.AsQueryable();
        }

        public Product Get(int id)
        {
            return _productRepository.Get(id);
        }
        public void Delete(Product entity)
        {
            _productRepository.Delete(entity);
        }

        public IQueryable<Product> GetAll()
        {
            return _productRepository.GetAll();
        }

        public void Insert(Product entity)
        {
            _productRepository.Insert(entity); 
        }

        public void Update(Product entity)
        {
            _productRepository.Update(entity); 
        }
    }
}

