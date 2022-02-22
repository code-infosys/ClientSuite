using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ClientSuite.Service; 
using ClientSuite.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClientSuite.Web.Areas.Master.Controllers
{
    [Area("Master")]
    public class PriorityController : BaseController
    {
        private readonly IPriorityService _priorityService;

        public PriorityController(IPriorityService priorityService)
        {
            this._priorityService = priorityService;

        }

        public IActionResult Index()
        {
            return View();
        }
 
        public IActionResult GetGrid([FromBody]DTParameters param)
        {
             return Json(_priorityService.GetGrid(param)); 
        }
 
        [HttpGet]
        public PartialViewResult Create()
        {
            Priority model = new Priority();

            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public IActionResult Create([FromForm]Priority model)
        {
            AlertBack alert = new AlertBack();
            try
            {
                if (ModelState.IsValid)
                {
                    
                    _priorityService.Insert(model);
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
            Priority ObjPriority = _priorityService.Get(id);

            return PartialView(ObjPriority);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Priority model)
        {
            AlertBack alert = new AlertBack(); 
            try
            {
                if (ModelState.IsValid)
                {
                    
                   _priorityService.Update(model);
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
                Priority ObjPriority = _priorityService.Get(id); 
               _priorityService.Delete(ObjPriority);

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

