using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ClientSuite.Models;
using ClientSuite.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ClientSuite.Web.Controllers
{
    [Route("auth")]
    public class AuthController : Controller
    {
        private readonly IAuthLoginService _AuthLoginSer;
        private readonly IConfiguration _configuration;
        private readonly IRoleUserService _RoleUserSer;
        private readonly IRoleService _RoleSer;
        private readonly IUserService _UserSer;
        private readonly IAppSettingService _appSetting;
         
       
        public AuthController(IAuthLoginService AuthLoginSer, IConfiguration configuration, IRoleUserService RoleUserSer, IRoleService RoleSer, IUserService UserSer, IAppSettingService appSetting)
        {
            this._AuthLoginSer = AuthLoginSer;
            this._configuration = configuration;
            this._RoleUserSer = RoleUserSer;
            this._RoleSer = RoleSer;
            this._UserSer = UserSer;
            this._appSetting = appSetting;
        }

        [Route("login")]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel
            {
                ReturnUrl = returnUrl
            });
        }

        [Route("login")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var (isValid, user) = await _AuthLoginSer.ValidateUserCredentialsAsync(model.UserName);
                string dbPassword = user.Password.ToString();

                bool CheckPassword = false;
                if (SecurePwdHasherHelper.Verify(model.Password, dbPassword))
                {
                    CheckPassword = true;
                }

                if (isValid && CheckPassword)
                {
                    var roleuser = _RoleUserSer.GetRoleUser().Where(i => i.UserId == user.Id).Select(i => i.RoleId).FirstOrDefault();
                    var role = _RoleSer.GetRole().Where(i => i.Id == roleuser).Select(i => i.RoleName).FirstOrDefault();
                    var setting = _appSetting.GetAppSetting().FirstOrDefault();
                    if (roleuser > 0 && role != null)
                    {
                        await LoginAsync(user, roleuser, role, setting);
                        if (IsUrlValid(model.ReturnUrl))
                        {
                            return Redirect(model.ReturnUrl);
                        }

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("RoleNotAssigned", "Role is Not Assigned.");
                    }
                }
                else
                {
                    ModelState.AddModelError("InvalidCredentials", "Invalid credentials.");
                }
            }

            return View(model);
        }

        private static bool IsUrlValid(string returnUrl)
        {
            return !string.IsNullOrWhiteSpace(returnUrl)
                   && Uri.IsWellFormedUriString(returnUrl, UriKind.Relative);
        }

        private async Task LoginAsync(User user, int roleid, string rolename, AppSetting s)
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
                    new Claim(ClaimTypes.Webpage, "AppName="+s.AppName+"#AppShortName="+s.AppShortName +"#AppShortName="+s.AppShortName+"#AppVersion="+s.AppVersion+"#FooterText="+s.FooterText+"#Skin="+s.Skin+"#IsToggleSidebar="+s.IsToggleSidebar+"#IsToggleRightSidebar="+s.IsToggleRightSidebar+"#IsFixedLayout="+s.IsFixedLayout+"#IsBoxedLayout="+s.IsBoxedLayout) 

                    //new Claim(ClaimTypes.DateOfBirth, user.DateOfBirth.ToString("o"), ClaimValueTypes.DateTime)
                };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(principal, properties);
        }

        [Route("logout")]
        public async Task<IActionResult> Logout(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            if (!_configuration.GetValue<bool>("Account:ShowLogoutPrompt"))
            {
                return await Logout();
            }

            return View();
        }


        public IActionResult Cancel(string returnUrl)
        {
            if (IsUrlValid(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("login", "Auth");
        }

        [HttpPost]
        [Route("logout")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }

            return RedirectToAction("login", "Auth");
        }

        [Route("unauthorized")]
        public IActionResult unauthorized()
        {
            return View();
        }

        [Route("forget")]
        [HttpPost]
        public async Task<JsonResult> Forget(string Username)
        {
            try
            {
                var (isValid, user) = await _AuthLoginSer.ValidateUserCredentialsAsync(Username);

                if (isValid)
                {
                    //update user data
                    User model = new User();
                    model = user;

                    model.ChangePasswordCode = Env.Encrypt(Username);
                    model.IsActive = false;
                    _UserSer.UpdateUser(model);
                    //end update user data

                    var GetSiteRoot = $"{this.Request.Scheme}://{this.Request.Host}";
                    string subject = "My Password";
                    string body = "Click here to reset your password<br> <a href='" + GetSiteRoot + "/auth/forgot/?id=" + user.ChangePasswordCode + "'>Click Here to reset</a> ";
                    Env.SendEmail(user.Email, subject, body);
                    return Json(new { msgstatus = true, msg = "Please check your email." });
                }
                else
                {
                    return Json(new { msgstatus = false, msg = "Entered Username does not exists." });
                }

            }
            catch (Exception ex)
            {
                return Json(new { msgstatus = false, msg = ex.Message });
            }
        }



        [Route("forgot")]
        public IActionResult Forgot(string id)
        {
            try
            {
                string dusername = Env.Decrypt(id);
                var user = _UserSer.GetUser().FirstOrDefault(i => i.IsActive == false && i.UserName == dusername);

                if (user != null)
                {
                    ViewBag.Valid = true;
                    ViewBag.id = id;
                }
                else
                    ViewBag.Valid = false;
            }
            catch (Exception)
            {
                ViewBag.Valid = false;
            } 
            return View();
        }

        [Route("forgot")]
        [HttpPost]
        public JsonResult Forgot(string id, string Password, string ConfirmPassword)
        {
            string message = string.Empty;
            bool status = false;
            if (Password == ConfirmPassword)
            {
                string dusername = Env.Decrypt(id);
                var user = _UserSer.GetUser().FirstOrDefault(i => i.IsActive == false && i.UserName == dusername);

                if (user != null)
                {
                    user.Password = SecurePwdHasherHelper.Hash(Password);
                    user.ChangePasswordCode ="";
                    user.IsActive = true;
                    _UserSer.UpdateUser(user);
                    message = "Successfully Reset now you may login";
                    status = true;
                }
                else
                {
                    message = "Please re-send request for forget password.";
                    status = false;
                }
            }
            else
            {
                message = "Password are not matached";
                status = false;
            }

            return Json(new { msgstatus = status, msg = message });
        }

    }
}

