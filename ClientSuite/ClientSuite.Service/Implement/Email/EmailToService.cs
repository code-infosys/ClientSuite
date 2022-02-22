using ClientSuite.Models;
using ClientSuite.Repo;  
using ClientSuite.Data;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Linq.Dynamic.Core; 

namespace ClientSuite.Service
{
    public class EmailToService : IEmailToService
    { 
        private readonly IRepository<EmailTo> _emailToRepository; 
        public EmailToService(IRepository<EmailTo> emailToRepository)
        {
            _emailToRepository = emailToRepository;
        }
 
        public DTResult<EmailToViewModel> GetGrid(DTParameters param)
        { 
            var tableDataSource= _emailToRepository.GetAll().Select(m => new EmailToViewModel { Id = m.Id,EmailMessageId = m.EmailMessageId,ToUserId = m.ToUserId,DateCreated = m.DateCreated }).AsQueryable();

            List<String> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            var filterdData= FilterResult(param.Search.Value, tableDataSource, columnSearch, param.SearchFromLength);
            List<EmailToViewModel> data = filterdData.OrderBy(param.SortOrder).Skip(param.Start).Take(param.Length).ToList();
            int count = filterdData.Count();

            DTResult<EmailToViewModel> result = new DTResult<EmailToViewModel>
            {
                draw = param.Draw,
                data = data,
                recordsFiltered = count,
                recordsTotal = count
            };

            return result;
             
        }

        private IQueryable<EmailToViewModel> FilterResult(string search, IQueryable<EmailToViewModel> dtResult, List<string> columnFilters, int searchTake = 500)
        {
            IQueryable<EmailToViewModel> results = dtResult;
            if (searchTake == 0)
                results = results.OrderByDescending(i => i.Id).AsQueryable();
            else
                results = results.OrderByDescending(i => i.Id).Take(searchTake).AsQueryable();

            if (!columnFilters.All(x => string.IsNullOrWhiteSpace(x)))
            {
                if (!string.IsNullOrEmpty(columnFilters[1]))
                    results = results.Where(p => p.Id.ToString().ToLower().Contains(columnFilters[1].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[2]))
                    results = results.Where(p => p.EmailMessageId.ToString().ToLower().Contains(columnFilters[2].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[3]))
                    results = results.Where(p => p.ToUserId.ToString().ToLower().Contains(columnFilters[3].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[4]))
                    results = results.Where(p => p.DateCreated.ToString().ToLower().Contains(columnFilters[4].ToLower()));

            }
            return results.AsQueryable();
        }

        public EmailTo Get(int id)
        {
            return _emailToRepository.Get(id);
        }
        public void Delete(EmailTo entity)
        {
            _emailToRepository.Delete(entity);
        }

        public IQueryable<EmailTo> GetAll()
        {
            return _emailToRepository.GetAll();
        }

        public void Insert(EmailTo entity)
        {
            _emailToRepository.Insert(entity); 
        }

        public void Update(EmailTo entity)
        {
            _emailToRepository.Update(entity); 
        }
    }
}

