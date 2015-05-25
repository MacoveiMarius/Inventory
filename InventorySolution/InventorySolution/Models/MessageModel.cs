using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InventorySolution.Classes.Utility;

namespace InventorySolution.Models
{
    public class MessageModel
    {
        public HtmlString Message { get; set; }
        public MessageType Type { get; set; }
        public MessageIcon Icon { get; set; }
    }

    public enum MessageType
    {
        [StringValue("alert-success")] Success = 1,
        [StringValue("alert-danger")] Error = 2,
        [StringValue("alert-warning")] Warning = 3
    }

    public enum MessageIcon
    {
        [StringValue("fa fa-check")] SuccessIcon = 1,
        [StringValue("fa fa-ban")] ErrorIcon = 2,
        [StringValue("fa fa-warning")] WarningIcon = 3,
    }
}