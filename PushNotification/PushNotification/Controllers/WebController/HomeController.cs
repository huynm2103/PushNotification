using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PushNotification.Helpers;

namespace PushNotification.Controllers
{
    public class HomeController : BaseController
    {
        [Route("")]
        public ActionResult Index()
        {
            getCurrentUser();
            ViewBag.BaseUrl = GetBaseUrl();
            return View();
        }


    }
}
