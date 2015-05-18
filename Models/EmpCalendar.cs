using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TimeSheetWeb.Models
{
    [Table("calendar_h")]
    public class EmpCalendar
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string calendarcode { get; set; }
        public string english { get; set; }
        public string chinese { get; set; }
        public string big5 { get; set; }
        public string restday { get; set; }
        public byte firstweekday { get; set; }
        public string japanese { get; set; }
        public double mwstdworkhour { get; set; }
        public double mwlunchhour { get; set; }
        public bool mwnolunchhourhalf { get; set; }
        public double mwhalfdayhour { get; set; }
        public string countrycode { get; set; }
        public string dw_mon_days_type { get; set; }
        public decimal dw_fixed_mon_days { get; set; }
    }
}