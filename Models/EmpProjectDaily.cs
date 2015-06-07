using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TimeSheetWeb.Models
{
    [Table("pmprojecttrandaily")]
    public class EmpProjectDaily
    {
        public int empid { get; set; }
        public Nullable<System.DateTime> refdate { get; set; }
        public string datetype { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int autoid { get; set; }
        public string status { get; set; }
        public double workhours { get; set; }
        public double leavehours { get; set; }
        public string comments { get; set; }
        public int returnuser { get; set; }
    }
}