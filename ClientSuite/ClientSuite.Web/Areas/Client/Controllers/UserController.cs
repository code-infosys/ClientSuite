using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ClientSuite.Service;
using ClientSuite.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Mvc.Rendering; 
using ClientSuite.Models.RequestModels;

namespace ClientSuite.Web.Areas.Client.Controllers
{
    [Area("Client")]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly ICurrencyService _currencyService;
        private readonly ICountryService _countryService;
        private readonly IProductService _productService;
        private readonly IPurchaseStatusService _purchaseStatusService;
        private readonly ICategoryService _categoryService;
        private readonly IClientCategoryService _clientCategoryService;
        private readonly IClientProductService _clientProductService;
        private readonly IRoleUserService _roleUserService;
        private readonly IContactsService _contactsService;
        public UserController(IUserService userService, IRoleService roleService, ICurrencyService currencyService, ICountryService countryService
            , IProductService productService, IPurchaseStatusService purchaseStatusService, ICategoryService categoryService, IClientCategoryService clientCategoryService,
            IClientProductService clientProductService, IRoleUserService roleUserService, IContactsService contactsService)
        {
            this._userService = userService;
            this._roleService = roleService;
            this._currencyService = currencyService;
            this._countryService = countryService;
            this._productService = productService;
            this._purchaseStatusService = purchaseStatusService;
            this._categoryService = categoryService;
            this._clientCategoryService = clientCategoryService;
            this._clientProductService = clientProductService;
            this._roleUserService = roleUserService;
            this._contactsService = contactsService;
        }




        public IActionResult Index(int? role = null)
        {
            ViewBag.SelectedRole = role;
            return View();
        }

        public IActionResult GetGrid([FromBody] DTParameters param, int? role)
        {
            return Json(_userService.GetGrid(param, UserId(), RoleId(), role));
        }

        [HttpGet]
        public PartialViewResult Create(int? role = null)
        {
            User model = new User();
            ViewBag.Roles = new SelectList(_roleService.GetAll().ToArray(), "Id", "RoleName");
            ViewBag.Currencys = new SelectList(_currencyService.GetAll().ToArray(), "Id", "Name");
            ViewBag.Countrys = new SelectList(_countryService.GetAll().ToArray(), "Id", "Name");
            model.RoleId = role;
            return PartialView(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([FromForm] User model, IFormFile profilePicture, string profilePicture10, string NewPasswordConfirm)
        {
            AlertBack alert = new AlertBack();
            try
            {
                if (ModelState.IsValid)
                {
                    if (profilePicture != null)
                    {
                        ModelState.Clear();
                        model.ProfilePicture = Env.GetUploadedFilePath(profilePicture).Result;
                    }
                    else
                    {
                        model.ProfilePicture = profilePicture10;
                    }

                    if (!string.IsNullOrEmpty(model.Password) && !string.IsNullOrEmpty(NewPasswordConfirm))
                    {
                        if (model.Password == NewPasswordConfirm)
                        {
                            model.Password = SecurePwdHasherHelper.Hash(model.Password);
                        }
                        else
                        {
                            alert.Status = "warning";
                            alert.Message = "Confirm password mismatch";
                            return Json(new AlertBack { Status = alert.Status, Message = alert.Message });
                        }
                    }

                    _userService.Insert(model);

                    var roleUser = new RoleUser()
                    {
                        RoleId = model.RoleId.Value,
                        UserId = model.Id
                    };
                    _roleUserService.Insert(roleUser);

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

            return Json(alert);
        }

        [HttpGet]
        public PartialViewResult CreateClient(int? role = null)
        {
            ClientRequestModel model = new ClientRequestModel();
            ViewBag.Roles = new SelectList(_roleService.GetAll().ToArray(), "Id", "RoleName");
            ViewBag.Currencys = new SelectList(_currencyService.GetAll().ToArray(), "Id", "Name");
            ViewBag.Countrys = new SelectList(_countryService.GetAll().ToArray(), "Id", "Name");
            ViewBag.Products = new SelectList(_productService.GetAll().ToArray(), "Id", "Name");
            ViewBag.PurchaseStatuss = new SelectList(_purchaseStatusService.GetAll().ToArray(), "Id", "Name");
            ViewBag.Categorys = new SelectList(_categoryService.GetAll().ToArray(), "Id", "Name");
            model.RoleId = role;
            return PartialView(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateClient([FromForm] ClientRequestModel model, IFormFile profilePicture, string profilePicture10)
        {
            AlertBack alert = new AlertBack();
            try
            {
                if (ModelState.IsValid)
                {
                    if (profilePicture != null)
                    {
                        ModelState.Clear();
                        model.ProfilePicture = Env.GetUploadedFilePath(profilePicture).Result;
                    }
                    else
                    {
                        model.ProfilePicture = profilePicture10;
                    }

                    _userService.Insert(model);

                    var roleUser = new RoleUser()
                    {
                        RoleId = model.RoleId.Value,
                        UserId = model.Id
                    };
                    _roleUserService.Insert(roleUser);

                    var clientCategory = new ClientCategory()
                    {
                        AddedBy = UserId(),
                        DateAdded = DateTime.Now,
                        DateModified = DateTime.Now,
                        UserId = model.Id,
                        ModifiedBy = UserId(),
                        CategoryId = model.CategoryId
                    };

                    _clientCategoryService.Insert(clientCategory);

                    var clientProduct = new ClientProduct()
                    {
                        FollowUpDate = model.FollowUpDate,
                        AddedBy = UserId(),
                        DateAdded = DateTime.Now,
                        DateModified = DateTime.Now,
                        ModifiedBy = UserId(),
                        ProductId = model.ProductId,
                        PurchaseStatusId = model.PurchaseStatusId,
                        UserId = model.Id
                    };

                    _clientProductService.Insert(clientProduct);

                    var contacts = new Contacts()
                    {
                        AddedBy = UserId(),
                        DateAdded = DateTime.Now,
                        DateModified = DateTime.Now,
                        ModifiedBy = UserId(),
                        CategoryId = model.CategoryId,
                        Address = model.CompanyAddress,
                        Email = model.Email,
                        Mobile = model.MobileNumber,
                        Name = model.FullName
                    };
                    _contactsService.Insert(contacts);

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

            return Json(alert);
        }


        public PartialViewResult Edit(int id)
        {
            User ObjUser = _userService.Get(id);
            ViewBag.Roles = new SelectList(_roleService.GetAll().ToArray(), "Id", "RoleName");
            ViewBag.Currencys = new SelectList(_currencyService.GetAll().ToArray(), "Id", "Name");
            ViewBag.Countrys = new SelectList(_countryService.GetAll().ToArray(), "Id", "Name");

            return PartialView(ObjUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(User model, IFormFile profilePicture, string profilePicture10)
        {
            AlertBack alert = new AlertBack();
            try
            {
                if (ModelState.IsValid)
                {
                    if (profilePicture != null)
                    {
                        ModelState.Clear();
                        model.ProfilePicture = Env.GetUploadedFilePath(profilePicture).Result;
                    }
                    else
                    {
                        model.ProfilePicture = profilePicture10;
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

            return Json(alert);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            try
            {
                User ObjUser = _userService.Get(id);
                _userService.Delete(ObjUser);

                sb.Append("Sumitted");
                return Content(sb.ToString());

            }
            catch (Exception ex)
            {
                sb.Append("Error :" + ex.Message);
            }

            return Content(sb.ToString());
        }

 

    }
}

