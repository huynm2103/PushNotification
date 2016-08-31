using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PushNotification.Helpers;

namespace PushNotification.Controllers.WebController
{
    public class ClientController : BaseController
    {
        public ActionResult Home()
        {
            return PartialView("~/Views/Home/_Home.cshtml");
        }

        public ActionResult Register()
        {
            return PartialView("~/Views/Login/_Register.cshtml");
        }
    }
}