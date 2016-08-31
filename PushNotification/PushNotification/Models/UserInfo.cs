using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PushNotification.Models
{
    public class UserInfo
    {
        #region "Attributes"

        public int UserID { get; set; }
        public int Id { get; set; }
        public string CloudId { get; set; }

        #endregion
        [ForeignKey("UserID")]
        public virtual User User { get; set; }
    }


    #region "Enum"
    //public enum Gender
    //{
    //    Male = 1,
    //    Female = 2
    //}
    #endregion
}