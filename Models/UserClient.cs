using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TimeSheetWeb.Models
{
    [Table("pmprojectmember")]
    public class UserClient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(Order = 10)]
        public int empid { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(Order = 20)]
        public string seg1code { get; set; }
        public int moduser { get; set; }
        public Nullable<System.DateTime> moddate { get; set; }
        public Nullable<System.DateTime> createdate { get; set; }
        public int createuser { get; set; }
        public int autoid { get; set; }
        public int clientleader { get; set; }
        public int teamleader { get; set; }
        public int teamleader2 { get; set; }
    }
}