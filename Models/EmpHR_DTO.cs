using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeSheetWeb.Models
{
    public class EmpHR_DTO
    {
        public int empid { get; set; }
        public string empcode { get; set; }
        public string english { get; set; }
        public string chinese { get; set; }
        public Nullable<System.DateTime> contractstartdate { get; set; }
        public Nullable<System.DateTime> contractenddate { get; set; }
        public string calendarcode { get; set; }
    }
}