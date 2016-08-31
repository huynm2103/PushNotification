using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PushNotification.Models.DTOs
{
    public class HttpMessageDTO
    {

        public string Status { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}