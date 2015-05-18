using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TimeSheetWeb.Models
{
    [Table("users")]
    public partial class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int userid { get; set; }
        public string usercode { get; set; }
        public string english { get; set; }
        public string chinese { get; set; }
        public string big5 { get; set; }
        public string usergroupcode { get; set; }
        public string userpassword { get; set; }
        public int empid { get; set; }
        public string datemask { get; set; }
        public string timemask { get; set; }
        public string employeefilter { get; set; }
        public byte theme { get; set; }
        public int language { get; set; }
        public int empnamemode { get; set; }
        public int recordperpage { get; set; }
        public bool showcode { get; set; }
        public bool locked { get; set; }
        public int createuser { get; set; }
        public string createusergroup { get; set; }
        public Nullable<System.DateTime> createtime { get; set; }
        public int mod_user { get; set; }
        public Nullable<System.DateTime> mod_time { get; set; }
        public bool hideprivatefile { get; set; }
        public bool delegateapprove { get; set; }
        public int delegateapprover { get; set; }
        public string starturl { get; set; }
        public byte fontsize { get; set; }
        public string exporttype { get; set; }
        public bool popupnote { get; set; }
        public string emplistcolumns { get; set; }
        public string resetpwdsn { get; set; }
        public bool delegateapply { get; set; }
        public int delegateapplicant { get; set; }
        public string japanese { get; set; }
        public string usertheme { get; set; }
        public bool companyuser { get; set; }
        public int noterows { get; set; }
        public bool wraptext { get; set; }
        public bool floatmenu { get; set; }
    }
}