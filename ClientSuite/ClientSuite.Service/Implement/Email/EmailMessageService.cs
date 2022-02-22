using ClientSuite.Models;
using ClientSuite.Repo;  
using ClientSuite.Data;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Linq.Dynamic.Core; 

namespace ClientSuite.Service
{
    public class EmailMessageService : IEmailMessageService
    { 
        private readonly IRepository<EmailMessage> _emailMessageRepository; 
        public EmailMessageService(IRepository<EmailMessage> emailMessageRepository)
        {
            _emailMessageRepository = emailMessageRepository;
        }
 
        public DTResult<EmailMessageViewModel> GetGrid(DTParameters param)
        { 
            var tableDataSource= _emailMessageRepository.GetAll().Select(m => new EmailMessageViewModel { Id = m.Id,FromUserId = m.FromUserId,Subject = m.Subject,Body = m.Body,SentDate = m.SentDate,AttachmentJson = m.AttachmentJson }).AsQueryable();

            List<String> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            var filterdData= FilterResult(param.Search.Value, tableDataSource, columnSearch, param.SearchFromLength);
            List<EmailMessageViewModel> data = filterdData.OrderBy(param.SortOrder).Skip(param.Start).Take(param.Length).ToList();
            int count = filterdData.Count();

            DTResult<EmailMessageViewModel> result = new DTResult<EmailMessageViewModel>
            {
                draw = param.Draw,
                data = data,
                recordsFiltered = count,
                recordsTotal = count
            };

            return result;
             
        }

        private IQueryable<EmailMessageViewModel> FilterResult(string search, IQueryable<EmailMessageViewModel> dtResult, List<string> columnFilters, int searchTake = 500)
        {
            IQueryable<EmailMessageViewModel> results = dtResult;
            if (searchTake == 0)
                results = results.OrderByDescending(i => i.Id).AsQueryable();
            else
                results = results.OrderByDescending(i => i.Id).Take(searchTake).AsQueryable();

            if (!columnFilters.All(x => string.IsNullOrWhiteSpace(x)))
            {
                if (!string.IsNullOrEmpty(columnFilters[1]))
                    results = results.Where(p => p.Id.ToString().ToLower().Contains(columnFilters[1].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[2]))
                    results = results.Where(p => p.FromUserId.ToString().ToLower().Contains(columnFilters[2].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[3]))
                    results = results.Where(p => p.Subject.ToString().ToLower().Contains(columnFilters[3].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[4]))
                    results = results.Where(p => p.Body.ToString().ToLower().Contains(columnFilters[4].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[5]))
                    results = results.Where(p => p.SentDate.ToString().ToLower().Contains(columnFilters[5].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[6]))
                    results = results.Where(p => p.AttachmentJson.ToString().ToLower().Contains(columnFilters[6].ToLower()));

            }
            return results.AsQueryable();
        }

        public EmailMessage Get(int id)
        {
            return _emailMessageRepository.Get(id);
        }
        public void Delete(EmailMessage entity)
        {
            _emailMessageRepository.Delete(entity);
        }

        public IQueryable<EmailMessage> GetAll()
        {
            return _emailMessageRepository.GetAll();
        }

        public void Insert(EmailMessage entity)
        {
            _emailMessageRepository.Insert(entity); 
        }

        public void Update(EmailMessage entity)
        {
            _emailMessageRepository.Update(entity); 
        }
    }
}

