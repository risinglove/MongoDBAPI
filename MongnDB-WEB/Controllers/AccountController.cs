using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using MongnDB_WEB.Models;
using BLL;
using Utility;
using MongoDB.Bson;
using MongnDB_WEB.Function;
using System.Web.Security;

namespace MongnDB_WEB.Controllers
{
    [Authorize]
    public class AccountController : ControllerBaseAbs
    {
        private UserBLL userBll
        {
            get
            {
                return new UserBLL();
            }
        }
        //private ApplicationSignInManager _signInManager;
        //private ApplicationUserManager _userManager;

        //public AccountController()
        //{
        //}

        //public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        //{
        //    UserManager = userManager;
        //    SignInManager = signInManager;
        //}

        //public ApplicationSignInManager SignInManager
        //{
        //    get
        //    {
        //        return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
        //    }
        //    private set
        //    {
        //        _signInManager = value;
        //    }
        //}

        //public ApplicationUserManager UserManager
        //{
        //    get
        //    {
        //        return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        //    }
        //    private set
        //    {
        //        _userManager = value;
        //    }
        //}

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="model"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            AccountsPrincipal userPrincipal = AccountsPrincipal.ValidateLogin(model.Email,model.Password);
            if (userPrincipal == null)
            {
                ModelState.AddModelError("Message", "用户名或密码不正确, 请重新输入!");
                return View(model);
            }

            var identity = new ClaimsIdentity(userPrincipal.Identity);
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userPrincipal.user.Id));
            identity.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "ASP.NET Identity")); // 需要用于AntiForgeryToken
            
            HttpContext.GetOwinContext().Authentication.SignIn(identity);
            FormsAuthentication.SetAuthCookie(model.Email, model.RememberMe);
            Session[Globals.SESSIONKEY_USER] = userPrincipal.user;
            return RedirectToLocal(returnUrl);
            
            #region MyRegion
            //var user = userBll.Login(model.Email, EncyryptionUtil.getMD5(model.Password));
            //if (user == null)
            //{
            //    ModelState.AddModelError("", "用户名或密码错误。");
            //}
            // 这不会计入到为执行帐户锁定而统计的登录失败次数中
            // 若要在多次输入错误密码的情况下触发帐户锁定，请更改为 shouldLockout: true
            //var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            //switch (result)
            //{
            //    case SignInStatus.Success:
            //        //HttpContext.User = user;
            //        return RedirectToLocal(returnUrl);
            //    case SignInStatus.LockedOut:
            //    case SignInStatus.RequiresVerification:
            //    case SignInStatus.Failure:
            //    default:
            //        ModelState.AddModelError("", "无效的登录尝试。");
            //        return View(model);
            //} 
            #endregion
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (userBll.Check(model.Email))
                {
                    ModelState.AddModelError("", "该邮箱已被注册！");
                    return View(model);
                }
                Model.User userModel = new Model.User()
                {
                    NickName = model.NickName,
                    UserName = model.Email,
                    Password = EncyryptionUtil.getMD5(model.Password),
                    appID = EncyryptionUtil.getMD5(ObjectId.GenerateNewId(DateTime.Now).ToString()).ToLowerInvariant(),
                    appsecret = EncyryptionUtil.getMD5(Guid.NewGuid().ToString().Replace("-", "")).ToLowerInvariant()
                };
                if (userBll.Add(userModel))
                {
                    var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                    //var result = await UserManager.CreateAsync(user, model.Password);
                    //if (result.Succeeded)
                    //{
                    //    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    //    return RedirectToAction("Index", "Home");
                    //}
                   // AddErrors(result);
                }
                else
                {
                    ModelState.AddModelError("", "注册失败，请稍后重试！");
                }
            }

            // 如果我们进行到这一步时某个地方出错，则重新显示表单
            return View(model);
        }


        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            //var user = await UserManager.FindByNameAsync(model.Email);
            //if (user == null)
            //{
            //    // 请不要显示该用户不存在
            //    return RedirectToAction("ResetPasswordConfirmation", "Account");
            //}
            //var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            //if (result.Succeeded)
            //{
            //    return RedirectToAction("ResetPasswordConfirmation", "Account");
            //}
            //AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // 请求重定向到外部登录提供程序
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }
        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //if (_userManager != null)
                //{
                //    _userManager.Dispose();
                //    _userManager = null;
                //}

                //if (_signInManager != null)
                //{
                //    _signInManager.Dispose();
                //    _signInManager = null;
                //}
            }

            base.Dispose(disposing);
        }

        #region 帮助程序
        // 用于在添加外部登录名时提供 XSRF 保护
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}