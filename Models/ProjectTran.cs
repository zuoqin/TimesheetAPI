using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TimeSheetWeb.Models
{
    [Table("pmprojecttran")]
    public class ProjectTran
    {
        public int empid { get; set; }
        public string seg1code { get; set; }
        public string seg2code { get; set; }
        public string seg3code { get; set; }
        public string seg4code { get; set; }
        public double unit { get; set; }
        public Nullable<System.DateTime> refdate { get; set; }
        public string seg5code { get; set; }
        [Key]
        public int autoid { get; set; }
        public int createuser { get; set; }
        public int moduser { get; set; }
        public Nullable<System.DateTime> moddate { get; set; }
        public DateTime createdate { get; set; }
        public int requestid { get; set; }
        public string status { get; set; }
        public string comments { get; set; }

        public ProjectTran()
        {
            refdate = DateTime.Now;
            createuser = 0;
            moduser = 0;
            moddate = DateTime.Now;
            createdate = DateTime.Now;
            requestid = 0;
            status = "SUBMIT";
            comments = "";
        }
    }
}