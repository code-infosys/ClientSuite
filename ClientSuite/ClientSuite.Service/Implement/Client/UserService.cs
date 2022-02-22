using ClientSuite.Models;
using ClientSuite.Repo;  
using ClientSuite.Data;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Linq.Dynamic.Core; 

namespace ClientSuite.Service
{
    public class UserService : IUserService
    { 
        private readonly IRepository<User> _userRepository; 
        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }
 
        public DTResult<UserViewModel> GetGrid(DTParameters param,int user,int role, int? selectRole)
        { 
            var conditionData = _userRepository.GetAll().AsQueryable();
            if (role == Constants.AdminRole)
                conditionData = _userRepository.GetAll().AsQueryable();
            else
            {
                conditionData = _userRepository.GetAll().Where(i => i.Id == user).AsQueryable();
            }

            if(selectRole!=null)
            {
                conditionData = conditionData.Where(i => i.RoleId == selectRole).AsQueryable();
            }

            var tableDataSource = conditionData.Select(m => new UserViewModel { DateAdded = m.DateAdded,DateModified = m.DateModified,FullName = m.FullName,Email = m.Email,MobileNumber = m.MobileNumber,IsActive = m.IsActive, IsConfirm = m.IsConfirm,ProfilePicture = m.ProfilePicture,Id = m.Id,UserName = m.UserName, CompanyName = m.CompanyName,CompanyPhone = m.CompanyPhone,CompanyMobile = m.CompanyMobile,CompanyAddress = m.CompanyAddress,RoleId = m.Role_RoleId.RoleName,CurrencyId = m.Currency_CurrencyId.Name,CountryId = m.Country_CountryId.Name }).AsQueryable();

            List<String> columnSearch = new List<string>();

            foreach (var col in param.Columns)
            {
                columnSearch.Add(col.Search.Value);
            }

            var filterdData= FilterResult(param.Search.Value, tableDataSource, columnSearch, param.SearchFromLength);
            List<UserViewModel> data = filterdData.OrderBy(param.SortOrder).Skip(param.Start).Take(param.Length).ToList();
            int count = filterdData.Count();

            DTResult<UserViewModel> result = new DTResult<UserViewModel>
            {
                draw = param.Draw,
                data = data,
                recordsFiltered = count,
                recordsTotal = count
            };

            return result;
             
        }

        private IQueryable<UserViewModel> FilterResult(string search, IQueryable<UserViewModel> dtResult, List<string> columnFilters, int searchTake = 500)
        {
            IQueryable<UserViewModel> results = dtResult;
            if (searchTake == 0)
                results = results.OrderByDescending(i => i.Id).AsQueryable();
            else
                results = results.OrderByDescending(i => i.Id).Take(searchTake).AsQueryable();

            if (!columnFilters.All(x => string.IsNullOrWhiteSpace(x)))
            {
                if (!string.IsNullOrEmpty(columnFilters[1]))
                    results = results.Where(p => p.DateAdded.ToString().ToLower().Contains(columnFilters[1].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[2]))
                    results = results.Where(p => p.DateModified.ToString().ToLower().Contains(columnFilters[2].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[3]))
                    results = results.Where(p => p.FullName.ToString().ToLower().Contains(columnFilters[3].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[4]))
                    results = results.Where(p => p.Email.ToString().ToLower().Contains(columnFilters[4].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[5]))
                    results = results.Where(p => p.MobileNumber.ToString().ToLower().Contains(columnFilters[5].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[6]))
                    results = results.Where(p => p.IsActive.ToString().ToLower().Contains(columnFilters[6].ToLower())); 
                if (!string.IsNullOrEmpty(columnFilters[7]))
                    results = results.Where(p => p.IsConfirm.ToString().ToLower().Contains(columnFilters[7].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[8]))
                    results = results.Where(p => p.ProfilePicture.ToString().ToLower().Contains(columnFilters[8].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[9]))
                    results = results.Where(p => p.Id.ToString().ToLower().Contains(columnFilters[9].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[10]))
                    results = results.Where(p => p.UserName.ToString().ToLower().Contains(columnFilters[10].ToLower()));    
                if (!string.IsNullOrEmpty(columnFilters[11]))
                    results = results.Where(p => p.CompanyName.ToString().ToLower().Contains(columnFilters[11].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[12]))
                    results = results.Where(p => p.CompanyPhone.ToString().ToLower().Contains(columnFilters[12].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[13]))
                    results = results.Where(p => p.CompanyMobile.ToString().ToLower().Contains(columnFilters[13].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[14]))
                    results = results.Where(p => p.CompanyAddress.ToString().ToLower().Contains(columnFilters[14].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[15]))
                    results = results.Where(p => p.RoleId.ToString().ToLower().Contains(columnFilters[15].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[16]))
                    results = results.Where(p => p.CurrencyId.ToString().ToLower().Contains(columnFilters[16].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[17]))
                    results = results.Where(p => p.CountryId.ToString().ToLower().Contains(columnFilters[17].ToLower()));

            }
            return results.AsQueryable();
        }

        public User Get(int id)
        {
            return _userRepository.Get(id);
        }
        public void Delete(User entity)
        {
            _userRepository.Delete(entity);
        }

        public IQueryable<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        public void Insert(User entity)
        {
            _userRepository.Insert(entity); 
        }

        public void Update(User entity)
        {
            _userRepository.Update(entity); 
        }
    }
}

