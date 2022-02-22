using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ClientSuite.Service; 
using ClientSuite.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClientSuite.Web.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class MenuPermissionController : BaseController
    {
        private readonly IMenuPermissionService _menuPermissionService;
        private readonly IRoleService _roleService;
        private readonly IUserService _userService;
        private readonly IMenuService _menuService;

        public MenuPermissionController(IMenuPermissionService menuPermissionService,IRoleService roleService,IUserService userService,IMenuService menuService)
        {
            this._menuPermissionService = menuPermissionService;
            this._roleService = roleService;
            this._userService = userService;
            this._menuService = menuService;

        }

        public IActionResult Index()
        {
            return View();
        }
 
        public IActionResult GetGrid([FromBody]DTParameters param)
        {
             return Json(_menuPermissionService.GetGrid(param,UserId(),RoleId())); 
        }
 
        [HttpGet]
        public PartialViewResult Create()
        {
            MenuPermission model = new MenuPermission();
            ViewBag.Roles = new SelectList(_roleService.GetAll().ToArray(), "Id", "RoleName");
            ViewBag.Users = new SelectList(_userService.GetAll().ToArray(), "Id", "UserName");
            ViewBag.Menus = new SelectList(_menuService.GetAll().ToArray(), "Id", "MenuText");

            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public IActionResult Create([FromForm]MenuPermission model)
        {
            AlertBack alert = new AlertBack();
            try
            {
                if (ModelState.IsValid)
                {
                    
                    _menuPermissionService.Insert(model);
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
            MenuPermission ObjMenuPermission = _menuPermissionService.Get(id);
            ViewBag.Roles = new SelectList(_roleService.GetAll().ToArray(), "Id", "RoleName");
            ViewBag.Users = new SelectList(_userService.GetAll().ToArray(), "Id", "UserName");
            ViewBag.Menus = new SelectList(_menuService.GetAll().ToArray(), "Id", "MenuText");

            return PartialView(ObjMenuPermission);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(MenuPermission model)
        {
            AlertBack alert = new AlertBack(); 
            try
            {
                if (ModelState.IsValid)
                {
                    
                   _menuPermissionService.Update(model);
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
                MenuPermission ObjMenuPermission = _menuPermissionService.Get(id); 
               _menuPermissionService.Delete(ObjMenuPermission);

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

