using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace PushNotification.Models
{
    public class User
    {
        #region "Attributes"
        [Key]
        public int UserID { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string LoginType { get; set; }
        
        public string UserType { get; set; }

        public DateTime CreatedDate { get; set; }
        
        public DateTime? LastLogin { get; set; }
        
        public bool IsActive { get; set; }

        public string Email { get; set; }

        public bool IsVerify { get; set; }

        public string VerifyCode { get; set; }

        #endregion "End Attribute"

        #region "RelationShip"
        
        public virtual ICollection<UserInfo> UserInfo { get; set; }
        public virtual ICollection<Message> SentMessages { get; set; }
        #endregion
    }

    #region "Enum"

    //public enum LoginType
    //{
    //    Normal = 1,
    //    Facebook = 2,
    //    Both = 3
    //}

    //public enum UserType
    //{
    //    User = 1,
    //    Admin = 2
    //}
    #endregion "Enum"
}