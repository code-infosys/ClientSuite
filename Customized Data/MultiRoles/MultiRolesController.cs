using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ClientSuite.Service;
using System.Linq.Dynamic.Core;
using ClientSuite.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace ClientSuite.Web.Controllers
{
    public class MultiRolesController : BaseController
    {
        private readonly IRoleUserService _RoleUserSer;
        private readonly IAppSettingService _appSetting;
        public MultiRolesController(IRoleUserService RoleUserSer, IAppSettingService appSetting)
        {
            this._RoleUserSer = RoleUserSer;
            this._appSetting = appSetting;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult GetGrid([FromBody]DTParameters param)
        {
            try
            {
                int user = Convert.ToInt32(Env.GetUserInfo("Id", User.Claims));
                var dtsource = _RoleUserSer.GetRoleUser().Where(i => i.UserId == user).Select(m => new RoleUserViewModel { Id = m.Id, RoleId = m.Role_RoleId.RoleName, UserId = m.User_UserId.UserName }).AsQueryable();

                List<String> columnSearch = new List<string>();

                foreach (var col in param.Columns)
                {
                    columnSearch.Add(col.Search.Value);
                }

                var dataa = FilterResult(param.Search.Value, dtsource, columnSearch, param.SearchFromLength);
                List<RoleUserViewModel> data = dataa.OrderBy(param.SortOrder).Skip(param.Start).Take(param.Length).ToList();
                int count = dataa.Count();

                DTResult<RoleUserViewModel> result = new DTResult<RoleUserViewModel>
                {
                    draw = param.Draw,
                    data = data,
                    recordsFiltered = count,
                    recordsTotal = count
                };

                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }

        private IQueryable<RoleUserViewModel> FilterResult(string search, IQueryable<RoleUserViewModel> dtResult, List<string> columnFilters, int SearchTake = 500)
        {
            IQueryable<RoleUserViewModel> results = dtResult;
            if (SearchTake == 0)
                results = results.OrderByDescending(i => i.Id).AsQueryable();
            else
                results = results.OrderByDescending(i => i.Id).Take(SearchTake).AsQueryable();

            if (!columnFilters.All(x => string.IsNullOrWhiteSpace(x)))
            {
                if (!string.IsNullOrEmpty(columnFilters[1]))
                    results = results.Where(p => p.Id.ToString().ToLower().Contains(columnFilters[1].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[2]))
                    results = results.Where(p => p.RoleId.ToString().ToLower().Contains(columnFilters[2].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[3]))
                    results = results.Where(p => p.UserId.ToString().ToLower().Contains(columnFilters[3].ToLower()));

            }
            return results.AsQueryable();
        }



        public async Task<IActionResult> Impers(int id)
        {
            RoleUser ObjRoleUser = _RoleUserSer.GetAllInclude(i=>i.Role_RoleId,i=>i.User_UserId ).FirstOrDefault(i=>i.Id==id);
            
            if (ObjRoleUser != null)
            {
                if (User.Identity.IsAuthenticated)
                {
                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                }

                var setting = _appSetting.GetAppSetting().FirstOrDefault();

                await LoginAsync(ObjRoleUser.UserId, ObjRoleUser.User_UserId.UserName, ObjRoleUser.RoleId, ObjRoleUser.Role_RoleId.RoleName, setting);

                return RedirectToAction("Index", "Home");

            }
            else
            {
                return RedirectToAction("unauthorized", "Auth");
            }
        }

        private async Task LoginAsync(int userid, string UserName, int roleid, string rolename, AppSetting s)
        {
            var properties = new AuthenticationProperties
            {
                //AllowRefresh = false,
                //IsPersistent = true,
                //ExpiresUtc = DateTimeOffset.UtcNow.AddSeconds(10)
            };

            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, userid.ToString()),
                    new Claim(ClaimTypes.Name, UserName),
                    new Claim(ClaimTypes.Role, rolename),
                    new Claim(ClaimTypes.Rsa, roleid.ToString()),
                    new Claim(ClaimTypes.Webpage, "AppName="+s.AppName+"#AppShortName="+s.AppShortName +"#AppShortName="+s.AppShortName+"#AppVersion="+s.AppVersion+"#FooterText="+s.FooterText+"#Skin="+s.Skin+"#IsToggleSidebar="+s.IsToggleSidebar+"#IsToggleRightSidebar="+s.IsToggleRightSidebar+"#IsFixedLayout="+s.IsFixedLayout+"#IsBoxedLayout="+s.IsBoxedLayout) 

                    //new Claim(ClaimTypes.DateOfBirth, user.DateOfBirth.ToString("o"), ClaimValueTypes.DateTime)
                };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(principal, properties);
        }

    }
}


