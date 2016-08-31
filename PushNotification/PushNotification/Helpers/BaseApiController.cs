using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using PushNotification.Models;
using PushNotification.Models.DTOs;
using PushNotification.Respository;

namespace PushNotification.Helpers
{
    public class BaseApiController : ApiController
    {
        protected User getCurrentUser()
        {
            User currentUser = null;
            if (User.Identity.IsAuthenticated)
            {
                currentUser = UserRespository.Instance.GetByUserNameOrEmail(User.Identity.Name);
            }

            return currentUser;
        }
    }
}