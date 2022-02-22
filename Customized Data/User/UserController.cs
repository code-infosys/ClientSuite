using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ClientSuite.Service;
using System.Linq.Dynamic.Core;
using ClientSuite.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace ClientSuite.Web.Controllerss
{
    public class UserController : BaseController
    {
        private readonly IUserService _UserSer;

        public UserController(IUserService UserSer)
        {
            this._UserSer = UserSer;

        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult GetGrid([FromBody]DTParameters param)
        {
            try
            {
                var dtsource = _UserSer.GetUser().Select(m => new UserViewModel { Id = m.Id,UserName = m.UserName, FirstName = m.FirstName, Email = m.Email,ContactNumber = m.ContactNumber,   AddedBy = m.AddedBy,DateAdded = m.DateAdded ,IsActive = m.IsActive }).AsQueryable();

                List<String> columnSearch = new List<string>();

                foreach (var col in param.Columns)
                {
                    columnSearch.Add(col.Search.Value);
                }

                var dataa = FilterResult(param.Search.Value, dtsource, columnSearch, param.SearchFromLength);
                List<UserViewModel> data = dataa.OrderBy(param.SortOrder).Skip(param.Start).Take(param.Length).ToList();
                int count = dataa.Count();

                DTResult<UserViewModel> result = new DTResult<UserViewModel>
                {
                    draw = param.Draw,
                    data = data,
                    recordsFiltered = count,
                    recordsTotal = count
                };

                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }

        private IQueryable<UserViewModel> FilterResult(string search, IQueryable<UserViewModel> dtResult, List<string> columnFilters, int SearchTake = 500)
        {
            IQueryable<UserViewModel> results = dtResult;
            if (SearchTake == 0)
                results = results.OrderByDescending(i => i.Id).AsQueryable();
            else
                results = results.OrderByDescending(i => i.Id).Take(SearchTake).AsQueryable();

            if (!columnFilters.All(x => string.IsNullOrWhiteSpace(x)))
            {
                if (!string.IsNullOrEmpty(columnFilters[1]))
                    results = results.Where(p => p.Id.ToString().ToLower().Contains(columnFilters[1].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[2]))
                    results = results.Where(p => p.UserName.ToString().ToLower().Contains(columnFilters[2].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[3]))
                    results = results.Where(p => p.FirstName.ToString().ToLower().Contains(columnFilters[3].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[4]))
                    results = results.Where(p => p.Email.ToString().ToLower().Contains(columnFilters[4].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[5]))
                    results = results.Where(p => p.ContactNumber.ToString().ToLower().Contains(columnFilters[5].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[6]))
                    results = results.Where(p => p.AddedBy.ToString().ToLower().Contains(columnFilters[6].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[7]))
                    results = results.Where(p => p.DateAdded.ToString().ToLower().Contains(columnFilters[7].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[8]))
                    results = results.Where(p => p.IsActive.ToString().ToLower().Contains(columnFilters[8].ToLower()));
                  
            }
            return results.AsQueryable();
        }


        [HttpGet]
        public PartialViewResult Create()
        {
            User model = new User();

            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public IActionResult Create([FromForm]User model, string NewPasswordConfirm)
        {
            AlertBack alert = new AlertBack();
            try
            {
                if (ModelState.IsValid)
                {
                    if (!string.IsNullOrEmpty(model.Password) && !string.IsNullOrEmpty(NewPasswordConfirm))
                    {
                        if (model.Password == NewPasswordConfirm)
                        {
                            model.Password = SecurePwdHasherHelper.Hash(model.Password);
                        }
                        else
                        {
                            alert.Status = "warning";
                            alert.Message="Confirm password mismatch";
                            return Json(new AlertBack { Status = alert.Status, Message = alert.Message });
                        }
                    }

                    _userService.Insert(model);
                    alert.Status = "success";
                    alert.Message = "Register Successfully";
                }
                else
                {
                    alert.Status = "warning";
                    foreach (var key in this.ViewData.ModelState.Keys)
                    {
                        foreach (var err in this.ViewData.ModelState[key].Errors)
                        {
                            alert.Message += err.ErrorMessage + "<br/>";
                        }
                    } 
                }
            }
            catch (Exception ex)
            {
                alert.Status = "error";
                alert.Message = ex.Message; 
            }

            return Json(new AlertBack { Status = alert.Status , Message= alert.Message }); 
        }

        [HttpPost]
        public IActionResult Copy(int id)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder(); 
            try
            {
                User ObjUser = _UserSer.GetUserById(id);
                ObjUser.Id = _UserSer.GetUser().Max(i=>i.Id)+1;
                ObjUser.UserName = "Copy_" + ObjUser.UserName;
                _UserSer.InsertUser(ObjUser);
                 
                sb.Append("Sumitted");
                return Content(sb.ToString());

            }
            catch (Exception ex)
            {
                sb.Append("Error :" + ex.Message);
            }

            return Content(sb.ToString()); 
        }

         
        public PartialViewResult Edit(int id)
        { 
            User ObjUser = _UserSer.GetUserById(id);

            return PartialView(ObjUser);
        }
         
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(User model, string NewPassword, string newPasswordConfirm)
        {
            AlertBack alert = new AlertBack(); 
            try
            {
                if (ModelState.IsValid)
                {

                    if (!string.IsNullOrEmpty(NewPassword) && !string.IsNullOrEmpty(newPasswordConfirm))
                    {
                        if (NewPassword == newPasswordConfirm)
                        {
                            model.Password = SecurePwdHasherHelper.Hash(newPasswordConfirm);
                        }
                        else
                        {
                            alert.Status = "warning";
                            alert.Message = "Confirm password mismatch";
                            return Json(new AlertBack { Status = alert.Status, Message = alert.Message });
                        }
                    }

                    _userService.Update(model);
                    alert.Status = "success";
                    alert.Message = "Register Successfully";
                }
                else
                {
                    alert.Status = "warning";
                    foreach (var key in this.ViewData.ModelState.Keys)
                    {
                        foreach (var err in this.ViewData.ModelState[key].Errors)
                        {
                            alert.Message += err.ErrorMessage + "<br/>";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                alert.Status = "error";
                alert.Message = ex.Message; 
            }
             
            return Json(new AlertBack { Status = alert.Status , Message= alert.Message }); 
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            try
            {
                User ObjUser = _UserSer.GetUserById(id); 
                _UserSer.DeleteUser(ObjUser);

                sb.Append("Sumitted");
                return Content(sb.ToString());

            }
            catch (Exception ex)
            {
                sb.Append("Error :" + ex.Message);
            }

            return Content(sb.ToString());
        }

        public PartialViewResult Details(int id)
        {
            User ObjUser = _userService.Get(id);
            return PartialView(ObjUser);
        }

    }
}


