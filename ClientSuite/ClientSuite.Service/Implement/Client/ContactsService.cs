using ClientSuite.Models;
using ClientSuite.Repo;  
using ClientSuite.Data;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Linq.Dynamic.Core; 

namespace ClientSuite.Service
{
    public class ContactsService : IContactsService
    { 
        private readonly IRepository<Contacts> _contactsRepository; 
        public ContactsService(IRepository<Contacts> contactsRepository)
        {
            _contactsRepository = contactsRepository;
        }
 
        public DTResult<ContactsViewModel> GetGrid(DTParameters param)
        { 
            var tableDataSource= _contactsRepository.GetAll().Select(m => new ContactsViewModel { Id = m.Id,Email = m.Email,Mobile = m.Mobile,AddedBy = m.AddedBy,DateAdded = m.DateAdded,ModifiedBy = m.ModifiedBy,DateModified = m.DateModified,Name = m.Name,Address = m.Address,CategoryId = m.Category_CategoryId.Name }).AsQueryable();

            List<String> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            var filterdData= FilterResult(param.Search.Value, tableDataSource, columnSearch, param.SearchFromLength);
            List<ContactsViewModel> data = filterdData.OrderBy(param.SortOrder).Skip(param.Start).Take(param.Length).ToList();
            int count = filterdData.Count();

            DTResult<ContactsViewModel> result = new DTResult<ContactsViewModel>
            {
                draw = param.Draw,
                data = data,
                recordsFiltered = count,
                recordsTotal = count
            };

            return result;
             
        }

        private IQueryable<ContactsViewModel> FilterResult(string search, IQueryable<ContactsViewModel> dtResult, List<string> columnFilters, int searchTake = 500)
        {
            IQueryable<ContactsViewModel> results = dtResult;
            if (searchTake == 0)
                results = results.OrderByDescending(i => i.Id).AsQueryable();
            else
                results = results.OrderByDescending(i => i.Id).Take(searchTake).AsQueryable();

            if (!columnFilters.All(x => string.IsNullOrWhiteSpace(x)))
            {
                if (!string.IsNullOrEmpty(columnFilters[1]))
                    results = results.Where(p => p.Id.ToString().ToLower().Contains(columnFilters[1].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[2]))
                    results = results.Where(p => p.Email.ToString().ToLower().Contains(columnFilters[2].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[3]))
                    results = results.Where(p => p.Mobile.ToString().ToLower().Contains(columnFilters[3].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[4]))
                    results = results.Where(p => p.AddedBy.ToString().ToLower().Contains(columnFilters[4].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[5]))
                    results = results.Where(p => p.DateAdded.ToString().ToLower().Contains(columnFilters[5].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[6]))
                    results = results.Where(p => p.ModifiedBy.ToString().ToLower().Contains(columnFilters[6].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[7]))
                    results = results.Where(p => p.DateModified.ToString().ToLower().Contains(columnFilters[7].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[8]))
                    results = results.Where(p => p.Name.ToString().ToLower().Contains(columnFilters[8].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[9]))
                    results = results.Where(p => p.Address.ToString().ToLower().Contains(columnFilters[9].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[10]))
                    results = results.Where(p => p.CategoryId.ToString().ToLower().Contains(columnFilters[10].ToLower()));

            }
            return results.AsQueryable();
        }

        public Contacts Get(int id)
        {
            return _contactsRepository.Get(id);
        }
        public void Delete(Contacts entity)
        {
            _contactsRepository.Delete(entity);
        }

        public IQueryable<Contacts> GetAll()
        {
            return _contactsRepository.GetAll();
        }

        public void Insert(Contacts entity)
        {
            _contactsRepository.Insert(entity); 
        }

        public void Update(Contacts entity)
        {
            _contactsRepository.Update(entity); 
        }
    }
}

