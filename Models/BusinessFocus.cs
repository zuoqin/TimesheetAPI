using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TimeSheetWeb.Models
{
    [Table("pmprojectsegment4")]
    public class BusinessFocus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string seg4code { get; set; }
        public int createuser { get; set; }
        public Nullable<System.DateTime> createdate { get; set; }
        public Nullable<System.DateTime> moddate { get; set; }
        public int moduser { get; set; }
        public string english { get; set; }
        public string chinese { get; set; }
        public string big5 { get; set; }
        public string japanese { get; set; }
    }
}