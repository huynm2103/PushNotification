using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PushNotification.Models;
using PushNotification.Models.DTOs;
using PushNotification.Respository;

namespace PushNotification.Helpers
{
    public class BaseController : Controller
    {
        protected User getCurrentUser()
        {
            User currentUser = null;
            if (User.Identity.IsAuthenticated)
            {
                currentUser = UserRespository.Instance.GetByUserNameOrEmail(User.Identity.Name);
                ViewBag.CurrentUser = new UserBasicInfoDTO
                {
                    //FullName = currentUser.UserInfo.FullName,
                    //ProfileImage = currentUser.UserInfo.ProfileImage,
                    LoginType = currentUser.LoginType,
                    Role = currentUser.UserType,
                    IsActive = currentUser.IsActive,
                    UserName = currentUser.Username
                };
            }

            return currentUser;
        }

        public string GetBaseUrl()
        {
            var baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
            Request.ApplicationPath.TrimEnd('/') + "/";
            return baseUrl;
        }
    }
}