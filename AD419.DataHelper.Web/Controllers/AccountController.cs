﻿using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AD419.DataHelper.Web.Helpers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Serilog;

namespace AD419.DataHelper.Web.Controllers
{
    public class AccountController : SuperController
    {
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            var res = CasHelper.Login();
            if (res != null)
            {
                return Redirect(res);
            }
            
            // fetch cas ticket
            var auth = HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (auth == null)
            {
                Log.Warning("Auth cookie missing during login");
                return Content(string.Empty);
            }

            // decrypt data
            var cookie = FormsAuthentication.Decrypt(auth.Value);
            if (cookie == null)
            {
                Log.Warning("Auth cookie failed to decrypt");
                return Content(string.Empty);
            }
            var username = cookie.Name;

            // find current user
            var user = UserManager.FindByName(username);
            if (user == null)
            {
                Log.Warning("User {username} not found", username);
                //TODO: should return custom unauthenticated page, not redirect
                return Content("You do not have permission to view this page");
            }

            // kill old cookies
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);

            // sign in!
            Log.Information("User {username} logging in", username);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = true }, UserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie));

            return RedirectToLocal(returnUrl);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            Log.Information("User logging out");
            AuthenticationManager.SignOut();
            return Redirect("https://cas.ucdavis.edu/cas/logout");
        }

        #region Helpers
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
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
        #endregion
    }
}