using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PushNotification.Helpers
{
    public static class VarConstants
    {
        // This class includes constants string of DDL system.
        public static readonly string ConnectionString = "DDLDataContext";
        public static string DatetimeFormat = "yyyy-MM-dd HH:mm:ss";
        public static string DateFormat = "yyyy-MM-dd"; 
        public static string TimeZoneId = "SE Asia Standard Time"; // GMT+7

        /// <summary>
        /// Login type constants
        /// </summary>
        public static class LoginType
        {
            public static readonly string NORMAL = "normal";
            public static readonly string FACEBOOK = "facebook";
            public static readonly string BOTH = "both";
        }

        public static class HttpMessageType
        {
            public static readonly string NOT_AUTHEN = "not-authen";
            public static readonly string NOT_FOUND = "not-found";
            public static readonly string BAD_REQUEST = "bad-request";
            public static readonly string SUCCESS = "success";
            public static readonly string ERROR = "error";
        }
    }
}