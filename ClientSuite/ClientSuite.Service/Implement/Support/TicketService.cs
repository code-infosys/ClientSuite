using ClientSuite.Models;
using ClientSuite.Repo;  
using ClientSuite.Data;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Linq.Dynamic.Core; 

namespace ClientSuite.Service
{
    public class TicketService : ITicketService
    { 
        private readonly IRepository<Ticket> _ticketRepository; 
        public TicketService(IRepository<Ticket> ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }
 
        public DTResult<TicketViewModel> GetGrid(DTParameters param)
        { 
            var tableDataSource= _ticketRepository.GetAll().Select(m => new TicketViewModel { Id = m.Id,TicketSubject = m.TicketSubject,TicketBody = m.TicketBody,AddedBy = m.AddedBy,DateAdded = m.DateAdded,ModifiedBy = m.ModifiedBy,Attachment = m.Attachment,IsClose = m.IsClose,DateModified = m.DateModified,IsKnowledgeBase = m.IsKnowledgeBase,ProductId = m.Product_ProductId.Name,ParentId = m.Ticket2.TicketSubject,PriorityId = m.Priority_PriorityId.Name }).AsQueryable();

            List<String> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            var filterdData= FilterResult(param.Search.Value, tableDataSource, columnSearch, param.SearchFromLength);
            List<TicketViewModel> data = filterdData.OrderBy(param.SortOrder).Skip(param.Start).Take(param.Length).ToList();
            int count = filterdData.Count();

            DTResult<TicketViewModel> result = new DTResult<TicketViewModel>
            {
                draw = param.Draw,
                data = data,
                recordsFiltered = count,
                recordsTotal = count
            };

            return result;
             
        }

        private IQueryable<TicketViewModel> FilterResult(string search, IQueryable<TicketViewModel> dtResult, List<string> columnFilters, int searchTake = 500)
        {
            IQueryable<TicketViewModel> results = dtResult;
            if (searchTake == 0)
                results = results.OrderByDescending(i => i.Id).AsQueryable();
            else
                results = results.OrderByDescending(i => i.Id).Take(searchTake).AsQueryable();

            if (!columnFilters.All(x => string.IsNullOrWhiteSpace(x)))
            {
                if (!string.IsNullOrEmpty(columnFilters[1]))
                    results = results.Where(p => p.Id.ToString().ToLower().Contains(columnFilters[1].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[2]))
                    results = results.Where(p => p.TicketSubject.ToString().ToLower().Contains(columnFilters[2].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[3]))
                    results = results.Where(p => p.TicketBody.ToString().ToLower().Contains(columnFilters[3].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[4]))
                    results = results.Where(p => p.AddedBy.ToString().ToLower().Contains(columnFilters[4].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[5]))
                    results = results.Where(p => p.DateAdded.ToString().ToLower().Contains(columnFilters[5].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[6]))
                    results = results.Where(p => p.ModifiedBy.ToString().ToLower().Contains(columnFilters[6].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[7]))
                    results = results.Where(p => p.Attachment.ToString().ToLower().Contains(columnFilters[7].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[8]))
                    results = results.Where(p => p.IsClose.ToString().ToLower().Contains(columnFilters[8].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[9]))
                    results = results.Where(p => p.DateModified.ToString().ToLower().Contains(columnFilters[9].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[10]))
                    results = results.Where(p => p.IsKnowledgeBase.ToString().ToLower().Contains(columnFilters[10].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[11]))
                    results = results.Where(p => p.ProductId.ToString().ToLower().Contains(columnFilters[11].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[12]))
                    results = results.Where(p => p.ParentId.ToString().ToLower().Contains(columnFilters[12].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[13]))
                    results = results.Where(p => p.PriorityId.ToString().ToLower().Contains(columnFilters[13].ToLower()));

            }
            return results.AsQueryable();
        }

        public Ticket Get(int id)
        {
            return _ticketRepository.Get(id);
        }
        public void Delete(Ticket entity)
        {
            _ticketRepository.Delete(entity);
        }

        public IQueryable<Ticket> GetAll()
        {
            return _ticketRepository.GetAll();
        }

        public void Insert(Ticket entity)
        {
            _ticketRepository.Insert(entity); 
        }

        public void Update(Ticket entity)
        {
            _ticketRepository.Update(entity); 
        }
    }
}

