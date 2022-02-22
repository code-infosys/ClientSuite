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
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            this._productService = productService;

        }

        public IActionResult Index()
        {
            return View();
        }
 
        public IActionResult GetGrid([FromBody]DTParameters param)
        {
             return Json(_productService.GetGrid(param)); 
        }
 
        [HttpGet]
        public PartialViewResult Create()
        {
            Product model = new Product();

            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public IActionResult Create([FromForm]Product model,IFormFile picture, string picture4)
        {
            AlertBack alert = new AlertBack();
            try
            {
                if (ModelState.IsValid)
                {
                    if (picture != null)
                    { 
                        ModelState.Clear();
                        model.Picture = Env.GetUploadedFilePath(picture).Result; 
                    }
                    else
                    {
                        model.Picture = picture4;
                    }

                    _productService.Insert(model);
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
            Product ObjProduct = _productService.Get(id);

            return PartialView(ObjProduct);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product model,IFormFile picture, string picture4)
        {
            AlertBack alert = new AlertBack(); 
            try
            {
                if (ModelState.IsValid)
                {
                    if (picture != null)
                    { 
                        ModelState.Clear();
                        model.Picture = Env.GetUploadedFilePath(picture).Result; 
                    }
                    else
                    {
                        model.Picture = picture4;
                    }

                   _productService.Update(model);
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
                Product ObjProduct = _productService.Get(id); 
               _productService.Delete(ObjProduct);

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

