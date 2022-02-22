using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ClientSuite.Service; 
using ClientSuite.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClientSuite.Web.Areas.Quote.Controllers
{
    [Area("Quote")]
    public class InvoiceController : BaseController
    {
        private readonly IInvoiceService _invoiceService;
        private readonly IUserService _userService;
        private readonly ICurrencyService _currencyService;

        public InvoiceController(IInvoiceService invoiceService,IUserService userService, ICurrencyService currencyService)
        {
            this._invoiceService = invoiceService;
            this._userService = userService;
            _currencyService = currencyService;
        }

        public IActionResult Index()
        {
            ViewBag.Type = true;
            return View();
        }
 
        public IActionResult GetGrid([FromBody]DTParameters param, bool isQuote)
        {
             return Json(_invoiceService.GetGrid(param,UserId(), RoleId(), isQuote)); 
        }

        public IActionResult InvoiceList()
        {
            ViewBag.Type = false;
            return View();
        }
         
        public IActionResult Generate(string type= "invoice")
        {
            Invoice invoice = new Invoice();
            ViewBag.Type = type;
            ViewBag.Currency = new SelectList(_currencyService.GetAll().Select(i => new { Id = i.Code, Name = i.Name }).ToArray(), "Id", "Name");
            return View(invoice);
        }



        [HttpGet]
        public PartialViewResult Create()
        {
            Invoice model = new Invoice();
            ViewBag.Users = new SelectList(_userService.GetAll().ToArray(), "Id", "UserName");

            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public IActionResult Create([FromForm]Invoice model,IFormFile businessLogo, string businessLogo3)
        {
            AlertBack alert = new AlertBack();
            try
            {
                if (ModelState.IsValid)
                {
                    if (businessLogo != null)
                    { 
                        ModelState.Clear();
                        model.BusinessLogo = Env.GetUploadedFilePath(businessLogo).Result; 
                    }
                    else
                    {
                        model.BusinessLogo = businessLogo3;
                    }

                    _invoiceService.Insert(model);
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
            Invoice ObjInvoice = _invoiceService.Get(id);
            ViewBag.Users = new SelectList(_userService.GetAll().ToArray(), "Id", "UserName");

            return PartialView(ObjInvoice);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Invoice model,IFormFile businessLogo, string businessLogo3)
        {
            AlertBack alert = new AlertBack(); 
            try
            {
                if (ModelState.IsValid)
                {
                    if (businessLogo != null)
                    { 
                        ModelState.Clear();
                        model.BusinessLogo = Env.GetUploadedFilePath(businessLogo).Result; 
                    }
                    else
                    {
                        model.BusinessLogo = businessLogo3;
                    }

                   _invoiceService.Update(model);
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
                Invoice ObjInvoice = _invoiceService.Get(id); 
               _invoiceService.Delete(ObjInvoice);

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

