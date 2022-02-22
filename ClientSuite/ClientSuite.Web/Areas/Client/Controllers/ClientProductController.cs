using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ClientSuite.Service; 
using ClientSuite.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClientSuite.Web.Areas.Client.Controllers
{
    [Area("Client")]
    public class ClientProductController : BaseController
    {
        private readonly IClientProductService _clientProductService;
        private readonly IUserService _userService;
        private readonly IProductService _productService;
        private readonly IPurchaseStatusService _purchaseStatusService;

        public ClientProductController(IClientProductService clientProductService,IUserService userService,IProductService productService,IPurchaseStatusService purchaseStatusService)
        {
            this._clientProductService = clientProductService;
            this._userService = userService;
            this._productService = productService;
            this._purchaseStatusService = purchaseStatusService;

        }

        public IActionResult Index()
        {
            return View();
        }
  
        public IActionResult GetGrid([FromBody]DTParameters param)
        {
             return Json(_clientProductService.GetGrid(param,UserId(),RoleId())); 
        }
 
        [HttpGet]
        public PartialViewResult Create()
        {
            ClientProduct model = new ClientProduct();
            ViewBag.Users = new SelectList(_userService.GetAll().ToArray(), "Id", "UserName");
            ViewBag.Products = new SelectList(_productService.GetAll().ToArray(), "Id", "Name");
            ViewBag.PurchaseStatuss = new SelectList(_purchaseStatusService.GetAll().ToArray(), "Id", "Name");

            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public IActionResult Create([FromForm]ClientProduct model)
        {
            AlertBack alert = new AlertBack();
            try
            {
                if (ModelState.IsValid)
                {
                    
                    _clientProductService.Insert(model);
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
            ClientProduct ObjClientProduct = _clientProductService.Get(id);
            ViewBag.Users = new SelectList(_userService.GetAll().ToArray(), "Id", "UserName");
            ViewBag.Products = new SelectList(_productService.GetAll().ToArray(), "Id", "Name");
            ViewBag.PurchaseStatuss = new SelectList(_purchaseStatusService.GetAll().ToArray(), "Id", "Name");

            return PartialView(ObjClientProduct);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ClientProduct model)
        {
            AlertBack alert = new AlertBack(); 
            try
            {
                if (ModelState.IsValid)
                {
                    
                   _clientProductService.Update(model);
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
                ClientProduct ObjClientProduct = _clientProductService.Get(id); 
               _clientProductService.Delete(ObjClientProduct);

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

