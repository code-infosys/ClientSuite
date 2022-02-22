using ClientSuite.Models;
using ClientSuite.Repo;  
using ClientSuite.Data;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Linq.Dynamic.Core; 

namespace ClientSuite.Service
{
    public class ProductDocumentationService : IProductDocumentationService
    { 
        private readonly IRepository<ProductDocumentation> _productDocumentationRepository; 
        public ProductDocumentationService(IRepository<ProductDocumentation> productDocumentationRepository)
        {
            _productDocumentationRepository = productDocumentationRepository;
        }
 
        public DTResult<ProductDocumentationViewModel> GetGrid(DTParameters param)
        { 
            var tableDataSource= _productDocumentationRepository.GetAll().Select(m => new ProductDocumentationViewModel { Id = m.Id,Details = m.Details,AddedBy = m.AddedBy,DateAdded = m.DateAdded,ModifiedBy = m.ModifiedBy,DateModified = m.DateModified,Title = m.Title,ProductId = m.Product_ProductId.Name,ParentId = m.ProductDocumentation2.Title }).AsQueryable();

            List<String> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            var filterdData= FilterResult(param.Search.Value, tableDataSource, columnSearch, param.SearchFromLength);
            List<ProductDocumentationViewModel> data = filterdData.OrderBy(param.SortOrder).Skip(param.Start).Take(param.Length).ToList();
            int count = filterdData.Count();

            DTResult<ProductDocumentationViewModel> result = new DTResult<ProductDocumentationViewModel>
            {
                draw = param.Draw,
                data = data,
                recordsFiltered = count,
                recordsTotal = count
            };

            return result;
             
        }

        private IQueryable<ProductDocumentationViewModel> FilterResult(string search, IQueryable<ProductDocumentationViewModel> dtResult, List<string> columnFilters, int searchTake = 500)
        {
            IQueryable<ProductDocumentationViewModel> results = dtResult;
            if (searchTake == 0)
                results = results.OrderByDescending(i => i.Id).AsQueryable();
            else
                results = results.OrderByDescending(i => i.Id).Take(searchTake).AsQueryable();

            if (!columnFilters.All(x => string.IsNullOrWhiteSpace(x)))
            {
                if (!string.IsNullOrEmpty(columnFilters[1]))
                    results = results.Where(p => p.Id.ToString().ToLower().Contains(columnFilters[1].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[2]))
                    results = results.Where(p => p.Details.ToString().ToLower().Contains(columnFilters[2].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[3]))
                    results = results.Where(p => p.AddedBy.ToString().ToLower().Contains(columnFilters[3].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[4]))
                    results = results.Where(p => p.DateAdded.ToString().ToLower().Contains(columnFilters[4].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[5]))
                    results = results.Where(p => p.ModifiedBy.ToString().ToLower().Contains(columnFilters[5].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[6]))
                    results = results.Where(p => p.DateModified.ToString().ToLower().Contains(columnFilters[6].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[7]))
                    results = results.Where(p => p.Title.ToString().ToLower().Contains(columnFilters[7].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[8]))
                    results = results.Where(p => p.ProductId.ToString().ToLower().Contains(columnFilters[8].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[9]))
                    results = results.Where(p => p.ParentId.ToString().ToLower().Contains(columnFilters[9].ToLower()));

            }
            return results.AsQueryable();
        }

        public ProductDocumentation Get(int id)
        {
            return _productDocumentationRepository.Get(id);
        }
        public void Delete(ProductDocumentation entity)
        {
            _productDocumentationRepository.Delete(entity);
        }

        public IQueryable<ProductDocumentation> GetAll()
        {
            return _productDocumentationRepository.GetAll();
        }

        public void Insert(ProductDocumentation entity)
        {
            _productDocumentationRepository.Insert(entity); 
        }

        public void Update(ProductDocumentation entity)
        {
            _productDocumentationRepository.Update(entity); 
        }
    }
}

