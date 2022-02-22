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
    public class ProductDocumentationController : BaseController
    {
        private readonly IProductDocumentationService _productDocumentationService;
        private readonly IProductService _productService;

        public ProductDocumentationController(IProductDocumentationService productDocumentationService,IProductService productService)
        {
            this._productDocumentationService = productDocumentationService;
            this._productService = productService;

        }

        public IActionResult Index()
        {
            return View();
        }
 
        public IActionResult GetGrid([FromBody]DTParameters param)
        {
             return Json(_productDocumentationService.GetGrid(param)); 
        }
 
        [HttpGet]
        public PartialViewResult Create()
        {
            ProductDocumentation model = new ProductDocumentation();
            ViewBag.Products = new SelectList(_productService.GetAll().ToArray(), "Id", "Name");
            ViewBag.ProductDocumentations = new SelectList(_productDocumentationService.GetAll().ToArray(), "Id", "Title");

            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public IActionResult Create([FromForm]ProductDocumentation model)
        {
            AlertBack alert = new AlertBack();
            try
            {
                if (ModelState.IsValid)
                {
                    
                    _productDocumentationService.Insert(model);
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
            ProductDocumentation ObjProductDocumentation = _productDocumentationService.Get(id);
            ViewBag.Products = new SelectList(_productService.GetAll().ToArray(), "Id", "Name");
            ViewBag.ProductDocumentations = new SelectList(_productDocumentationService.GetAll().ToArray(), "Id", "Title");

            return PartialView(ObjProductDocumentation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductDocumentation model)
        {
            AlertBack alert = new AlertBack(); 
            try
            {
                if (ModelState.IsValid)
                {
                    
                   _productDocumentationService.Update(model);
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
                ProductDocumentation ObjProductDocumentation = _productDocumentationService.Get(id); 
               _productDocumentationService.Delete(ObjProductDocumentation);

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

