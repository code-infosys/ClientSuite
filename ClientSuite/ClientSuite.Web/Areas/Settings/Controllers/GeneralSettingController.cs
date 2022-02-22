using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ClientSuite.Service; 
using ClientSuite.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClientSuite.Web.Areas.Settings.Controllers
{
    [Area("Settings")]
    public class GeneralSettingController : BaseController
    {
        private readonly IGeneralSettingService _generalSettingService;

        public GeneralSettingController(IGeneralSettingService generalSettingService)
        {
            this._generalSettingService = generalSettingService;

        }

        public IActionResult Index()
        {
            return View();
        }
 
        public IActionResult GetGrid([FromBody]DTParameters param)
        {
             return Json(_generalSettingService.GetGrid(param)); 
        }
 
        [HttpGet]
        public PartialViewResult Create()
        {
            GeneralSetting model = new GeneralSetting();

            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public IActionResult Create([FromForm]GeneralSetting model)
        {
            AlertBack alert = new AlertBack();
            try
            {
                if (ModelState.IsValid)
                { 
                    _generalSettingService.Insert(model);
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
            GeneralSetting ObjGeneralSetting = _generalSettingService.Get(id);

            return PartialView(ObjGeneralSetting);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(GeneralSetting model)
        {
            AlertBack alert = new AlertBack(); 
            try
            {
                if (ModelState.IsValid)
                {
                    
                   _generalSettingService.Update(model);
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
                GeneralSetting ObjGeneralSetting = _generalSettingService.Get(id); 
               _generalSettingService.Delete(ObjGeneralSetting);

                sb.Append("Sumitted");
                return Content(sb.ToString());

            }
            catch (Exception ex)
            {
                sb.Append("Error :" + ex.Message);
            }

            return Content(sb.ToString());
        }

        public ActionResult Setup()
        {
            ViewBag.ListSettings = _generalSettingService.GetAll().ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Setup([FromForm] IFormCollection collection)
        {
            var settings = _generalSettingService.GetAll().ToList();
            foreach (var item in collection)
            {
                if (item.Key.ToString() != "__RequestVerificationToken")
                {
                    var setting = settings.FirstOrDefault(i => i.SettingKey == item.Key.ToString());
                    if(setting!=null)
                    {
                        setting.SettingValue = item.Value.ToString();
                        _generalSettingService.Update(setting);
                    } 
                }
            }

            ViewBag.ListSettings = _generalSettingService.GetAll().ToList();
            return View();
        }

    }
}

