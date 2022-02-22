using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ClientSuite.Service; 
using ClientSuite.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClientSuite.Web.Areas.Brand.Controllers
{
    [Area("Brand")]
    public class ClientCategoryController : BaseController
    {
        private readonly IClientCategoryService _clientCategoryService;
        private readonly IUserService _userService;
        private readonly ICategoryService _categoryService;

        public ClientCategoryController(IClientCategoryService clientCategoryService,IUserService userService,ICategoryService categoryService)
        {
            this._clientCategoryService = clientCategoryService;
            this._userService = userService;
            this._categoryService = categoryService;

        }

        public IActionResult Index()
        {
            return View();
        }
 
        public IActionResult GetGrid([FromBody]DTParameters param)
        {
             return Json(_clientCategoryService.GetGrid(param,UserId(),RoleId())); 
        }
 
        [HttpGet]
        public PartialViewResult Create()
        {
            ClientCategory model = new ClientCategory();
            ViewBag.Users = new SelectList(_userService.GetAll().ToArray(), "Id", "UserName");
            ViewBag.Categorys = new SelectList(_categoryService.GetAll().ToArray(), "Id", "Name");

            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public IActionResult Create([FromForm]ClientCategory model)
        {
            AlertBack alert = new AlertBack();
            try
            {
                if (ModelState.IsValid)
                {
                    
                    _clientCategoryService.Insert(model);
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
            ClientCategory ObjClientCategory = _clientCategoryService.Get(id);
            ViewBag.Users = new SelectList(_userService.GetAll().ToArray(), "Id", "UserName");
            ViewBag.Categorys = new SelectList(_categoryService.GetAll().ToArray(), "Id", "Name");

            return PartialView(ObjClientCategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ClientCategory model)
        {
            AlertBack alert = new AlertBack(); 
            try
            {
                if (ModelState.IsValid)
                {
                    
                   _clientCategoryService.Update(model);
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
                ClientCategory ObjClientCategory = _clientCategoryService.Get(id); 
               _clientCategoryService.Delete(ObjClientCategory);

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

