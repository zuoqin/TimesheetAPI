using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TimeSheetWeb.Models
{
    [Table("pmprojectsegment1")]
    public partial class Client
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string seg1code { get; set; }
        public int clientleader { get; set; }
        public int createuser { get; set; }
        public Nullable<System.DateTime> createdate { get; set; }
        public int moduser { get; set; }
        public Nullable<System.DateTime> moddate { get; set; }
        public string english { get; set; }
        public string chinese { get; set; }
        public string big5 { get; set; }
        public string japanese { get; set; }
        public string bpsname_english { get; set; }
        public string bpsname_chinese { get; set; }
        public bool notshowflag { get; set; }

        //public ICollection<Duty> ClientDuties { get; set; }
        //public ICollection<Product> ClientProducts { get; set; }
    }
}