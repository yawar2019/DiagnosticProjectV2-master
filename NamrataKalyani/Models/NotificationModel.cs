using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NamrataKalyani.Models
{
    public class NotificationModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }

    }
}