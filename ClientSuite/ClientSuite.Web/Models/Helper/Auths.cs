using ClientSuite.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ClientSuite.Web
{
    public static class Auths
    {
        public static async Task LoginAsync(User user, int roleid, string rolename, AppSetting s, HttpContext httpContext)
        {
            var properties = new AuthenticationProperties
            {
                //AllowRefresh = false,
                //IsPersistent = true,
                //ExpiresUtc = DateTimeOffset.UtcNow.AddSeconds(10)
            };

            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, rolename),
                    new Claim(ClaimTypes.Rsa, roleid.ToString()),
                    new Claim(ClaimTypes.Webpage, "AppName="+s.AppName+"#AppShortName="+s.AppShortName +"#AppShortName="+s.AppShortName+"#AppVersion="+s.AppVersion+"#FooterText="+s.FooterText+"#Skin="+s.Skin+"#IsToggleSidebar="+s.IsToggleSidebar+"#IsToggleRightSidebar="+s.IsToggleRightSidebar+"#IsFixedLayout="+s.IsFixedLayout+"#IsBoxedLayout="+s.IsBoxedLayout+"#Logo="+s.Logo+"#LoginPageBackground="+s.LoginPageBackground+"#TimeZone="+s.TimeZone+"#Currency="+s.Currency)

                    //new Claim(ClaimTypes.DateOfBirth, user.DateOfBirth.ToString("o"), ClaimValueTypes.DateTime)
                };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            
            await httpContext.SignInAsync(principal, properties);
        }


        //copy this method and use in controller where need
        //public async Task Impers(int roleId, int userId)
        //{
        //    AlertBack alert = new AlertBack();
        //    RoleUser ObjRoleUser = _roleUserService.GetAllInclude(i => i.Role_RoleId, i => i.User_UserId).FirstOrDefault(i => i.RoleId == roleId && i.UserId == userId);

        //    if (ObjRoleUser != null)
        //    {
        //        if (User.Identity.IsAuthenticated)
        //        {
        //            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        //        }

        //        var setting = _appSettingService.GetAll().FirstOrDefault();
        //        await Auths.LoginAsync(ObjRoleUser.User_UserId, ObjRoleUser.RoleId, ObjRoleUser.Role_RoleId.RoleName, setting, HttpContext);
        //        alert.Status = "success";
        //    }
        //    else
        //    {
        //        alert.Status = "error";
        //    }
        //}


    }
}

