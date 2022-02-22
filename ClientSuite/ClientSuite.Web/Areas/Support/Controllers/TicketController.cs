using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ClientSuite.Service;
using ClientSuite.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using ClientSuite.Models.RequestModels;

namespace ClientSuite.Web.Areas.Support.Controllers
{
    [Area("Support")]
    public class TicketController : BaseController
    {
        private readonly ITicketService _ticketService;
        private readonly IProductService _productService;
        private readonly IPriorityService _priorityService;
        private const int clientRoleId = 2;
        private readonly IUserService _userService;
        private readonly IRoleUserService _roleUserService;
        private readonly IGeneralSettingService _generalSettingService;
        private readonly INotificationService _notificationService;

        public TicketController(ITicketService ticketService, IProductService productService, IPriorityService priorityService, IUserService userService
            , IRoleUserService roleUserService, IGeneralSettingService generalSettingService, INotificationService notificationService)
        {
            this._ticketService = ticketService;
            this._productService = productService;
            this._priorityService = priorityService;
            this._userService = userService;
            this._roleUserService = roleUserService;
            this._generalSettingService = generalSettingService;
            this._notificationService = notificationService;

        }
       
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetGrid([FromBody] DTParameters param)
        {
            return Json(_ticketService.GetGrid(param));
        }

        [HttpGet]
        public PartialViewResult Create()
        {
            Ticket model = new Ticket();
            ViewBag.Products = new SelectList(_productService.GetAll().ToArray(), "Id", "Name");
            ViewBag.Tickets = new SelectList(_ticketService.GetAll().ToArray(), "Id", "TicketSubject");
            ViewBag.Prioritys = new SelectList(_priorityService.GetAll().ToArray(), "Id", "Name");

            return PartialView(model);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([FromForm] Ticket model, IFormFile attachment, string attachment7)
        {
            AlertBack alert = new AlertBack();
            try
            {
                if (ModelState.IsValid)
                {
                    if (attachment != null)
                    {
                        ModelState.Clear();
                        model.Attachment = Env.GetUploadedFilePath(attachment).Result;
                    }
                    else
                    {
                        model.Attachment = attachment7;
                    }

                    _ticketService.Insert(model);
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
        [AllowAnonymous]
        public IActionResult CreateTicket()
        {
            TicketRequestModel model = new TicketRequestModel();
            ViewBag.Products = new SelectList(_productService.GetAll().ToArray(), "Id", "Name");
            ViewBag.Tickets = new SelectList(_ticketService.GetAll().ToArray(), "Id", "TicketSubject");
            ViewBag.Prioritys = new SelectList(_priorityService.GetAll().ToArray(), "Id", "Name");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public IActionResult CreateTicket([FromForm] TicketRequestModel model, IFormFile attachment, string attachment7)
        {
            AlertBack alert = new AlertBack();
            try
            {
                if (ModelState.IsValid)
                {
                    if (attachment != null)
                    {
                        ModelState.Clear();
                        model.Attachment = Env.GetUploadedFilePath(attachment).Result;
                    }
                    else
                    {
                        model.Attachment = attachment7;
                    }

                    _ticketService.Insert(model);
                    alert.Status = "success";
                    alert.Message = "Register Successfully";

                    string password = Guid.NewGuid().ToString().Substring(0, 5) + "@321";
                    //create client in user table
                    User user = new User()
                    {
                        UserName = model.Email,
                        Password = SecurePwdHasherHelper.Hash(password),
                        RoleId = clientRoleId,
                        Email = model.Email,
                        MobileNumber = "0101010101",
                        IsActive = true,
                        IsConfirm = true,
                        CompanyName = "soninfosys"
                    };

                    _userService.Insert(user);

                    var roleUser = new RoleUser()
                    {
                        RoleId = clientRoleId,
                        UserId = user.Id
                    };
                    _roleUserService.Insert(roleUser);

                    //send notification to admin
                    var nf = new Notification();
                    nf.AddedBy = user.Id;
                    nf.DateAdded = DateTime.Now;
                    nf.DateModified = null;
                    nf.Details = "You have new notification from " + model.Email + ", He created new ticket Check it.";
                    nf.IsRead = null;
                    nf.ModifiedBy = null;
                    nf.ProcessToUrl = "/Support/Ticket/Details/"+ model.Id;
                    string guid = Guid.NewGuid().ToString();
                    nf.UniqueId = guid;
                    nf.TableId = model.Id;
                    nf.TableName = "Ticket";
                    nf.ToUserId = 1;
                    _notificationService.Insert(nf);
                    //send notification end

                    //create client end

                    try
                    { 
                    //send email to user.
                    var GetSiteRoot = $"{this.Request.Scheme}://{this.Request.Host}";
                    string subject = "Your Ticket is created successfully";
                    string body = "Your username is="+model.Email+" <br> and password is "+password+" <br> <a href='" + GetSiteRoot + "/auth/login/'>Click Here for login and check your ticket revert.</a> ";
                    var gs = _generalSettingService.GetAll().Where(i => i.SettingGroup == "smtp").ToArray();

                    Env.SendEmail(user.Email, subject, body,
                        gs.FirstOrDefault(i => i.SettingKey == "SmtpUsername").SettingValue,
                        gs.FirstOrDefault(i => i.SettingKey == "SmtpPassword").SettingValue,
                        gs.FirstOrDefault(i => i.SettingKey == "SmtpPort").SettingValue,
                        gs.FirstOrDefault(i => i.SettingKey == "SmptServer").SettingValue);
                        //send email to user end.

                    }
                    catch (Exception)
                    {
                    }


                    ViewBag.Message = "Register Successfully, Admin will contact to you soon.";

                    return View(model);
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


        public IActionResult Details(int id)
        {
            Ticket model = _ticketService.Get(id);
            ViewBag.Products = new SelectList(_productService.GetAll().ToArray(), "Id", "Name");
            ViewBag.Tickets = new SelectList(_ticketService.GetAll().ToArray(), "Id", "TicketSubject");
            ViewBag.Prioritys = new SelectList(_priorityService.GetAll().ToArray(), "Id", "Name");

            return View(model);
        }


        public PartialViewResult Edit(int id)
        {
            Ticket ObjTicket = _ticketService.Get(id);
            ViewBag.Products = new SelectList(_productService.GetAll().ToArray(), "Id", "Name");
            ViewBag.Tickets = new SelectList(_ticketService.GetAll().ToArray(), "Id", "TicketSubject");
            ViewBag.Prioritys = new SelectList(_priorityService.GetAll().ToArray(), "Id", "Name");

            return PartialView(ObjTicket);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Ticket model, IFormFile attachment, string attachment7)
        {
            AlertBack alert = new AlertBack();
            try
            {
                if (ModelState.IsValid)
                {
                    if (attachment != null)
                    {
                        ModelState.Clear();
                        model.Attachment = Env.GetUploadedFilePath(attachment).Result;
                    }
                    else
                    {
                        model.Attachment = attachment7;
                    }

                    _ticketService.Update(model);
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
                Ticket ObjTicket = _ticketService.Get(id);
                _ticketService.Delete(ObjTicket);

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

