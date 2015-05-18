using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TimeSheetWeb.Models
{
    [Table("pmprojectseg1toseg3")]
    public class ClientTask
    {
        public Nullable<System.DateTime> createdate { get; set; }
        public int createuser { get; set; }
        public Nullable<System.DateTime> moddate { get; set; }
        public int moduser { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(Order = 10)]
        public string seg1code { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(Order = 20)]
        public string seg3code { get; set; }
        public int autoid { get; set; }
    }
}