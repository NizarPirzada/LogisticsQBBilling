using FTData.Model.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FTData.DbContext.LicenseDbContext
{
    public class LicenseDbContext: IdentityDbContext<LicenseUser, LicenseUserRole, long>
    {
        public LicenseDbContext(
           DbContextOptions<LicenseDbContext> options
           ) : base(options)
        {
        }


        #region [ENTITIES]

        public virtual DbSet<CompanyInformation> CompanyInformation { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Driver> Drivers { get; set; }
        public virtual DbSet<DriverType> DriverTypes { get; set; }
        public virtual DbSet<Estimate> Estimates { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<Job> Jobs { get; set; }
        public virtual DbSet<JobProduct> JobProducts { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<TicketLineItem> TicketLineItems { get; set; }
        public virtual DbSet<Truck> Trucks { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Ticket>()
            //    .HasOne(s => s.Job)
            //    .WithMany(g => g.Tickets)
            //    .HasForeignKey(s => s.Job_Id)
            //    .OnDelete(DeleteBehavior.Cascade);


        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
