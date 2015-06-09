using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeSheetWeb.Models
{
    public class Client_DTO
    {
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

        public List<Product> theProducts;
        public List<Duty> theTasks;

    }
}