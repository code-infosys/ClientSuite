using ClientSuite.Models;
using ClientSuite.Repo;  
using ClientSuite.Data;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Linq.Dynamic.Core; 

namespace ClientSuite.Service
{
    public class InvoiceService : IInvoiceService
    { 
        private readonly IRepository<Invoice> _invoiceRepository; 
        public InvoiceService(IRepository<Invoice> invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }
 
        public DTResult<InvoiceViewModel> GetGrid(DTParameters param,int user,int role, bool isQuote)
        { 
            var conditionData = _invoiceRepository.GetAll().Where(i=>i.IsQuote==isQuote).AsQueryable();
            if (role == Constants.AdminRole)
                conditionData = _invoiceRepository.GetAll().Where(i => i.IsQuote == isQuote).AsQueryable();
            else
            {
                conditionData = _invoiceRepository.GetAll().Where(i => i.UserId == user && i.IsQuote == isQuote).AsQueryable();
            }

            var tableDataSource = conditionData.Select(m => new InvoiceViewModel { Id = m.Id,Name = m.Name,BusinessLogo = m.BusinessLogo,BusinessDetails = m.BusinessDetails,BillTo = m.BillTo,ClientDetails = m.ClientDetails, Currency = m.Currency, Dated = m.Dated,DueDate = m.DueDate,ItemJson = m.ItemJson,SubTotal = m.SubTotal,Tax = m.Tax,TotalDue = m.TotalDue,PaidAmount = m.PaidAmount,BalanceDue = m.BalanceDue,IsPaid = m.IsPaid,AddedBy = m.AddedBy,DateAdded = m.DateAdded,ModifiedBy = m.ModifiedBy,DateModified = m.DateModified,IsQuote = m.IsQuote,Note = m.Note,TermsAndCondition = m.TermsAndCondition,Sign = m.Sign,UserId = m.User_UserId.UserName }).AsQueryable();

            List<String> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            var filterdData= FilterResult(param.Search.Value, tableDataSource, columnSearch, param.SearchFromLength);
            List<InvoiceViewModel> data = filterdData.OrderBy(param.SortOrder).Skip(param.Start).Take(param.Length).ToList();
            int count = filterdData.Count();

            DTResult<InvoiceViewModel> result = new DTResult<InvoiceViewModel>
            {
                draw = param.Draw,
                data = data,
                recordsFiltered = count,
                recordsTotal = count
            };

            return result;
             
        }

        private IQueryable<InvoiceViewModel> FilterResult(string search, IQueryable<InvoiceViewModel> dtResult, List<string> columnFilters, int searchTake = 500)
        {
            IQueryable<InvoiceViewModel> results = dtResult;
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
                if (!string.IsNullOrEmpty(columnFilters[3]))
                    results = results.Where(p => p.BusinessLogo.ToString().ToLower().Contains(columnFilters[3].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[4]))
                    results = results.Where(p => p.BusinessDetails.ToString().ToLower().Contains(columnFilters[4].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[5]))
                    results = results.Where(p => p.BillTo.ToString().ToLower().Contains(columnFilters[5].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[6]))
                    results = results.Where(p => p.ClientDetails.ToString().ToLower().Contains(columnFilters[6].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[7]))
                    results = results.Where(p => p.Currency.ToString().ToLower().Contains(columnFilters[7].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[8]))
                    results = results.Where(p => p.Dated.ToString().ToLower().Contains(columnFilters[8].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[9]))
                    results = results.Where(p => p.DueDate.ToString().ToLower().Contains(columnFilters[9].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[10]))
                    results = results.Where(p => p.ItemJson.ToString().ToLower().Contains(columnFilters[10].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[11]))
                    results = results.Where(p => p.SubTotal.ToString().ToLower().Contains(columnFilters[11].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[12]))
                    results = results.Where(p => p.Tax.ToString().ToLower().Contains(columnFilters[12].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[13]))
                    results = results.Where(p => p.TotalDue.ToString().ToLower().Contains(columnFilters[13].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[14]))
                    results = results.Where(p => p.PaidAmount.ToString().ToLower().Contains(columnFilters[14].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[15]))
                    results = results.Where(p => p.BalanceDue.ToString().ToLower().Contains(columnFilters[15].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[16]))
                    results = results.Where(p => p.IsPaid.ToString().ToLower().Contains(columnFilters[16].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[17]))
                    results = results.Where(p => p.AddedBy.ToString().ToLower().Contains(columnFilters[17].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[18]))
                    results = results.Where(p => p.DateAdded.ToString().ToLower().Contains(columnFilters[18].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[19]))
                    results = results.Where(p => p.ModifiedBy.ToString().ToLower().Contains(columnFilters[19].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[20]))
                    results = results.Where(p => p.DateModified.ToString().ToLower().Contains(columnFilters[20].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[21]))
                    results = results.Where(p => p.IsQuote.ToString().ToLower().Contains(columnFilters[21].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[22]))
                    results = results.Where(p => p.Note.ToString().ToLower().Contains(columnFilters[22].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[23]))
                    results = results.Where(p => p.TermsAndCondition.ToString().ToLower().Contains(columnFilters[23].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[24]))
                    results = results.Where(p => p.Sign.ToString().ToLower().Contains(columnFilters[24].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[25]))
                    results = results.Where(p => p.UserId.ToString().ToLower().Contains(columnFilters[25].ToLower()));

            }
            return results.AsQueryable();
        }

        public Invoice Get(int id)
        {
            return _invoiceRepository.Get(id);
        }
        public void Delete(Invoice entity)
        {
            _invoiceRepository.Delete(entity);
        }

        public IQueryable<Invoice> GetAll()
        {
            return _invoiceRepository.GetAll();
        }

        public void Insert(Invoice entity)
        {
            _invoiceRepository.Insert(entity); 
        }

        public void Update(Invoice entity)
        {
            _invoiceRepository.Update(entity); 
        }
    }
}

