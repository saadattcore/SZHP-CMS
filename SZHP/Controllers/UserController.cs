using SZHPCMS.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SZHPCMS.Controllers
{
    public class UserController : BaseController
    {
        //
        // GET: /User/
        public ApplicationUserManager UserManager { get; private set; }
        public UserController()
        {
            UserManager = new ApplicationUserManager(new ApplicationUserStore(new ApplicationDbContext()));
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            LoginViewModel objmodel = new LoginViewModel();

            if (Request.Cookies["SZHPLogin"] != null)
            {
                objmodel.UserName = Request.Cookies["SZHPLogin"].Values["UserName"];
                objmodel.Password = Request.Cookies["SZHPLogin"].Values["Password"];
            }
            else
            {
                objmodel.UserName = string.Empty;
                objmodel.Password = string.Empty;
            }
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindAsync(model.UserName, model.Password);

                if (user != null)
                {
                    Session.Add(SZHPCMS.Common.Constants.LOGGED_IN_USER_ID, user.Id);

                    await SignInAsync(user, model.RememberMe);

                    if (model.RememberMe)
                    {
                        HttpCookie cookie = new HttpCookie("SZHPLogin");
                        cookie.Values.Add("UserName", model.UserName);
                        cookie.Values.Add("Password", model.Password);
                        cookie.Expires = DateTime.Now.AddDays(365);
                        Response.Cookies.Add(cookie);

                    }

                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ViewBag.ReturnUrl = returnUrl;
                    // ModelState.AddModelError("", "Invalid username or password.");
                    ViewBag.Error = "Invalid username or password";
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("DashBoard", "Home");
            }
        }

        #endregion

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            return RedirectToAction("Login");
        }

        public ActionResult Logout()
        {
            var AutheticationManager = HttpContext.GetOwinContext().Authentication;
            AutheticationManager.SignOut();
            FormsAuthentication.SignOut();

            Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetAllowResponseInBrowserHistory(false);

            Session.Abandon();

            return RedirectToAction("Login");
        }


        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }


        [HttpPost]
        public ActionResult ChangePassword(ManageUserViewModel viewModel)
        {

            try
            {
                if (ModelState.IsValid)
                {

                    long userID = UserID;

                    var user = UserManager.FindById(userID);

                    IdentityResult result = null;

                    if (user != null)
                    {
                        result = UserManager.ChangePassword<ApplicationUser, long>(userID, viewModel.OldPassword, viewModel.NewPassword);

                        IEnumerable<string> errors = result.Errors;

                        if (errors.Count() > 0)
                        {
                            TempData[SZHPCMS.Common.Constants.MESSAGE] = errors.FirstOrDefault();

                            TempData["IsSucess"] = false;
                        }
                        else
                        {
                            TempData[SZHPCMS.Common.Constants.MESSAGE] = "Password changed sucessfully";

                            TempData["IsSucess"] = true;

                        }

                        viewModel.OldPassword = string.Empty;
                        viewModel.NewPassword = string.Empty;
                        viewModel.ConfirmPassword = string.Empty;

                        return RedirectToAction("ChangePassword");
                    }
                }
                else
                {
                    return View(viewModel);
                }
            }
            catch (Exception)
            {
                throw;
            }


            return View();

        }

        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            
                ViewData["error"] = "";
                return View(new LoginViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult ForgotPassword(LoginViewModel model)
        {
            var user = UserManager.FindByEmail(model.UserName);
            if (user != null)
            {
                //var mUserPassword = _UsersBH.GetPassword(model.Email);
                //if (mUserPassword.Trim().Length > 0)
                //{
                //    mUserPassword = Utility.Decrypt(_UsersBH.GetPassword(model.Email));
                //    string strActivaionUrl = Request.Url.ToString();
                //    strActivaionUrl = strActivaionUrl.Remove(strActivaionUrl.LastIndexOf('/'));
                //    strActivaionUrl = strActivaionUrl + "/Login";
                //    string strPath = Server.MapPath("/assets/Email/password-reset.html");
                //    Utility.SendEmail("Password Retrieval", strActivaionUrl, model.Email, mUserPassword, model.firstName, strPath);

                //    return RedirectToAction("ThankYou", new { id = 1 });
                //}
            }
            else
            {
                ViewData["error"] = "Invalid user email, please try again";
            }

            return View(model);

        }

    }
}