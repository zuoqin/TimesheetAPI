using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace TimeSheetWeb.Models
{
    public class TimeSheetWebContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Configure Code First to ignore PluralizingTableName convention 
            // If you keep this convention then the generated tables will have pluralized names. 
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            // Map one-to-zero or one relationship 
            //modelBuilder.Entity<Duty>()
            //    .HasRequired<Client>(t => t.theClient)
            //    .WithMany(s => s.ClientDuties)
            //    .HasForeignKey(s => s.ClientCode);
            //.WithRequiredPrincipal();



        } 
        public TimeSheetWebContext() : base("name=TimeSheetWebContext")
        {
            this.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }

        public System.Data.Entity.DbSet<TimeSheetWeb.Models.Client> Clients { get; set; }
        public System.Data.Entity.DbSet<TimeSheetWeb.Models.Product> Products { get; set; }
        public System.Data.Entity.DbSet<TimeSheetWeb.Models.Duty> Duties { get; set; }
        public System.Data.Entity.DbSet<TimeSheetWeb.Models.User> Users { get; set; }
        public System.Data.Entity.DbSet<TimeSheetWeb.Models.EmpHR> EmpHRs { get; set; }
        public System.Data.Entity.DbSet<TimeSheetWeb.Models.EmpCalendar> EmpCalendars { get; set; }
        public System.Data.Entity.DbSet<TimeSheetWeb.Models.Calendar> CalendarData { get; set; }
        public System.Data.Entity.DbSet<TimeSheetWeb.Models.UserClient> UserClients { get; set; }
        public System.Data.Entity.DbSet<TimeSheetWeb.Models.ClientProduct> ClientProducts { get; set; }
        public System.Data.Entity.DbSet<TimeSheetWeb.Models.ClientTask> ClientTasks { get; set; }
        public System.Data.Entity.DbSet<TimeSheetWeb.Models.EmpProjectDaily> EmpDayProjects { get; set; }

        public System.Data.Entity.DbSet<TimeSheetWeb.Models.BusinessFocus> BusinessFocuces { get; set; }
        public System.Data.Entity.DbSet<TimeSheetWeb.Models.BusinessLine> BusinessLines { get; set; }
        public System.Data.Entity.DbSet<TimeSheetWeb.Models.ProjectTran> ProjectTrans { get; set; }
        
    }
}
