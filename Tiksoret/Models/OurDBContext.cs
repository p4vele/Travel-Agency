using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Tiksoret.Models
{
    public class OurDBContext :DbContext
    {
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    modelBuilder.Entity<User>().ToTable("tblUser");
        //}
        public OurDBContext():base("OurDBContext")
        {

        }
        public DbSet<User> user { get; set; }
        public DbSet<Admin> admin { get; set; }
        public DbSet<APlane> PlaneInfo { get; set; }
        public DbSet<FlightBooking> FlightBookings { get; set; }
        public DbSet<Payment> payment { get; set; }
        public System.Data.Entity.DbSet<Tiksoret.Models.TicketReserve_tbl> TicketReserve_tbl { get; set; }
    }
}