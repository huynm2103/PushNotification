using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace PushNotification.Models
{
    public class Message
    {
        #region "Attributes"
        /// <summary>
        /// Identify number of message.
        /// </summary>
        public int MessageId { get; set; }

        /// <summary>
        /// Identify number of user who create this conversation.
        /// </summary>
        public int CreatorID { get; set; }

        /// <summary>
        /// Content of message.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Message sent date.
        /// </summary>
        public DateTime SentTime { get; set; }

        /// <summary>
        /// Message created date.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Message loop.
        /// </summary>
        public bool IsLoop { get; set; }

        #endregion "End Attribute"

        #region "RelationShip"

        [ForeignKey("CreatorID")]
        [InverseProperty("SentMessages")]
        public virtual User Creator { get; set; }

        #endregion
    }
}