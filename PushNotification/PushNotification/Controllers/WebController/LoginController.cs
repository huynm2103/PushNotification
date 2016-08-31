using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PushNotification.Helpers;
using PushNotification.Models.DTOs;
using PushNotification.Respository;

namespace PushNotification.Controllers.WebController
{
    public class LoginController : BaseController
    {
        [Route("Login")]
        public ActionResult Login(string returnUrl = "")
        {
            try
            {
                // Check is logged.
                if (User.Identity != null && User.Identity.IsAuthenticated) return Redirect("/");

                // Logout authen.
                FormsAuthentication.SignOut();
                // Remove all cookies.
                var limit = Request.Cookies.Count;
                for (var i = 0; i < limit; i++)
                {
                    var cookieName = Request.Cookies[i].Name;
                    var cookie = new HttpCookie(cookieName) { Expires = DateTime.UtcNow.AddDays(-1) };
                    Response.Cookies.Add(cookie);
                }
                ViewBag.ReturnUrl = returnUrl;
                return View("Login", new UserLoginDTO());
            }
            catch (Exception)
            {
                return Redirect("/#/error");
            }
        }

        [Route("Login")]
        [HttpPost]
        public ActionResult Login(UserLoginDTO model, string returnUrl)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return View("~/Views/Login/Login.cshtml", model);
                }

                var user = UserRespository.Instance.GetByUserNameOrEmail(model.Username, model.Password);
                if (user == null)
                {
                    ModelState.AddModelError("", "Wrong Password or Account didn't exist!");

                }
                else
                {
                    FormsAuthentication.SetAuthCookie(user.Username, model.RememberMe);
                    user.LastLogin = DateTime.UtcNow;
                    UserRespository.Instance.UpdateUser(user);
                    if (string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect("/");
                    }
                    else
                    {
                        return Redirect("/#" + returnUrl);
                    }
                }

                ViewBag.ReturlUrl = returnUrl;
                return View("~/Views/Login/Login.cshtml", model);
            }
            catch (Exception)
            {
                return Redirect("/#/error");
            }
        }

        [Route("Logout")]
        [Authorize]
        public ActionResult Logout()
        {
            try
            {
                FormsAuthentication.SignOut();
                //for (int index = 0; index < Session.Keys.Count; index++)
                //{
                //    var sessionName = Session.Keys[index];
                //    Session[sessionName] = null;
                //}
                // Remove all cookies.
                var limit = Request.Cookies.Count;
                for (int i = 0; i < limit; i++)
                {
                    var cookieName = Request.Cookies[i].Name;
                    var cookie = new HttpCookie(cookieName) { Expires = DateTime.Now.AddDays(-1) };
                    Response.Cookies.Add(cookie);
                }
                return Redirect("/");
            }
            catch (Exception)
            {
                return Redirect("/#/error");
            }
        }
    }
}