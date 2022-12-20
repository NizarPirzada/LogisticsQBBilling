using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using FTData.Model.Entities;


#nullable disable

namespace FTData.Context
{
    public partial class SLCContext : DbContext
    {
        public SLCContext()
        {
        }
        private readonly string _connectionString;
        public SLCContext(string connectionString)
            //: base(options)
        {
            _connectionString = connectionString;
        }

        public SLCContext(DbContextOptions<SLCContext> options)
        : base(options)
        {
            //_connectionString = options.ToString();
        }

        public virtual DbSet<ApplicationError> ApplicationErrors { get; set; }
        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public virtual DbSet<CompanyInformation> CompanyInformations { get; set; }
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
        public virtual DbSet<VwAvailableTicketsForInvoice> VwAvailableTicketsForInvoices { get; set; }
        public virtual DbSet<VwEstimate> VwEstimates { get; set; }
        public virtual DbSet<VwInvoice> VwInvoices { get; set; }
        public virtual DbSet<VwInvoiceSummary> VwInvoiceSummaries { get; set; }
        public virtual DbSet<VwJob> VwJobs { get; set; }
        public virtual DbSet<VwPaymentDetail> VwPaymentDetails { get; set; }
        public virtual DbSet<VwReportEmployeeDriver> VwReportEmployeeDrivers { get; set; }
        public virtual DbSet<VwReportJobPrice> VwReportJobPrices { get; set; }
        public virtual DbSet<VwReportTruckDetail> VwReportTruckDetails { get; set; }
        public virtual DbSet<VwReportTruckHire> VwReportTruckHires { get; set; }
        public virtual DbSet<VwTentativeInvoice> VwTentativeInvoices { get; set; }
        public virtual DbSet<VwTicket> VwTickets { get; set; }
        public virtual DbSet<VwTicketLineItem> VwTicketLineItems { get; set; }
        public virtual DbSet<VwTicketsForPayment> VwTicketsForPayments { get; set; }
        public virtual DbSet<VwTruckTotal> VwTruckTotals { get; set; }
        public virtual DbSet<VwUnpaidInvoice> VwUnpaidInvoices { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<ApplicationError>(entity =>
            {
                entity.ToTable("Application_Error");

                entity.Property(e => e.ApplicationErrorId).HasColumnName("Application_Error_ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.EventDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Event_Date");

                entity.Property(e => e.Source)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy).HasColumnName("Created_By");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Creation_Date");

                entity.Property(e => e.UpdatedBy).HasColumnName("Updated_By");

                entity.Property(e => e.LastUpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Last_Updated_Date");
            });

            //modelBuilder.Entity<ApplicationUser>(entity =>
            //{
            //    entity.ToTable("Application_User");

            //    entity.Property(e => e.ApplicationUserId).HasColumnName("Application_User_ID");

            //    entity.Property(e => e.EmailAddress)
            //        .HasMaxLength(100)
            //        .IsUnicode(false)
            //        .HasColumnName("Email_Address");

            //    entity.Property(e => e.FullName)
            //        .IsRequired()
            //        .HasMaxLength(50)
            //        .IsUnicode(false)
            //        .HasColumnName("Full_Name");

            //    entity.Property(e => e.IsSystemAdministrator).HasColumnName("Is_System_Administrator");

            //    entity.Property(e => e.LogonId)
            //        .IsRequired()
            //        .HasMaxLength(25)
            //        .IsUnicode(false)
            //        .HasColumnName("Logon_ID");

            //    entity.Property(e => e.Password)
            //        .HasMaxLength(4096)
            //        .IsUnicode(false);


            //    entity.Property(e => e.CreatedBy).HasColumnName("Created_By");

            //    entity.Property(e => e.CreationDate)
            //        .HasColumnType("datetime")
            //        .HasColumnName("Creation_Date");

            //    entity.Property(e => e.UpdatedBy).HasColumnName("Updated_By");

            //    entity.Property(e => e.LastUpdatedDate)
            //        .HasColumnType("datetime")
            //        .HasColumnName("Last_Updated_Date");
            //});

            modelBuilder.Entity<CompanyInformation>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Company_Information");

                entity.Property(e => e.Address1)
                    .HasMaxLength(75)
                    .IsUnicode(false)
                    .HasColumnName("Address_1");

                entity.Property(e => e.Address2)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Address_2");

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyInformationId).HasColumnName("Company_Information_ID");

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Company_Name");

                entity.Property(e => e.Fax)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Url)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("URL");

                entity.Property(e => e.ZipCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Zip_Code");

                entity.Property(e => e.CreatedBy).HasColumnName("Created_By");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Creation_Date");

                entity.Property(e => e.UpdatedBy).HasColumnName("Updated_By");

                entity.Property(e => e.LastUpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Last_Updated_Date");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.HasIndex(e => e.Code, "IX_Customer")
                    .IsUnique();

                entity.Property(e => e.CustomerId).HasColumnName("Customer_ID");

                entity.Property(e => e.Address1)
                    .HasMaxLength(75)
                    .IsUnicode(false)
                    .HasColumnName("Address_1");

                entity.Property(e => e.Address2)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Address_2");

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fax)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.IsInActive)
                    .HasColumnName("Is_InActive")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.Phone)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.ZipCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Zip_Code");

                entity.Property(e => e.CreatedBy).HasColumnName("Created_By");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Creation_Date");

                entity.Property(e => e.UpdatedBy).HasColumnName("Updated_By");

                entity.Property(e => e.LastUpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Last_Updated_Date");
            });

            modelBuilder.Entity<Driver>(entity =>
            {
                entity.ToTable("Driver");

                entity.HasIndex(e => e.Code, "UIX_Driver")
                    .IsUnique();

                entity.Property(e => e.DriverId).HasColumnName("Driver_ID");

                entity.Property(e => e.Address1)
                    .HasMaxLength(75)
                    .IsUnicode(false)
                    .HasColumnName("Address_1");

                entity.Property(e => e.Address2)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Address_2");

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DriverTypeId).HasColumnName("Driver_Type_ID");

                entity.Property(e => e.Fax)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(75)
                    .IsUnicode(false)
                    .HasColumnName("Full_Name");

                entity.Property(e => e.HireDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Hire_Date");

                entity.Property(e => e.IsInactive)
                    .HasColumnName("Is_Inactive")
                    .HasDefaultValueSql("(0)");

                entity.Property(e => e.Phone)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.ZipCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Zip_Code");

                entity.HasOne(d => d.DriverType)
                    .WithMany(p => p.Drivers)
                    .HasForeignKey(d => d.DriverTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Driver_Type_Driver_FK1");

                entity.Property(e => e.CreatedBy).HasColumnName("Created_By");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Creation_Date");

                entity.Property(e => e.UpdatedBy).HasColumnName("Updated_By");

                entity.Property(e => e.LastUpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Last_Updated_Date");
            });

            modelBuilder.Entity<DriverType>(entity =>
            {
                entity.ToTable("Driver_Type");

                entity.Property(e => e.DriverTypeId)
                    .ValueGeneratedNever()
                    .HasColumnName("Driver_Type_ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy).HasColumnName("Created_By");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Creation_Date");

                entity.Property(e => e.UpdatedBy).HasColumnName("Updated_By");

                entity.Property(e => e.LastUpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Last_Updated_Date");
            });

            modelBuilder.Entity<Estimate>(entity =>
            {
                entity.ToTable("Estimate");

                entity.HasIndex(e => e.Code, "IX_Estimate")
                    .IsUnique();

                entity.Property(e => e.EstimateId).HasColumnName("Estimate_ID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedById).HasColumnName("Created_By_ID");

                entity.Property(e => e.CustomerId).HasColumnName("Customer_ID");

                entity.Property(e => e.Description)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.EnteredDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Entered_Date");

                entity.Property(e => e.ExpirationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Expiration_Date");

                entity.Property(e => e.Location)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Total).HasColumnType("money");

                entity.HasOne(d => d.CreatedBy);
                   // .WithMany(p => p.Estimates)
                    //.HasForeignKey(d => d.CreatedById)
                    //.OnDelete(DeleteBehavior.ClientSetNull)
                    //.HasConstraintName("FK_Estimate_Application_User");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Estimates)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Customer_Estimate_FK1");

               // entity.Property(e => e.CreatedBy).HasColumnName("Created_By");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Creation_Date");

                entity.Property(e => e.UpdatedBy).HasColumnName("Updated_By");

                entity.Property(e => e.LastUpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Last_Updated_Date");
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.ToTable("Invoice");

                entity.HasIndex(e => e.Number, "IX_Invoice")
                    .IsUnique();

                entity.Property(e => e.InvoiceId).HasColumnName("Invoice_ID");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Creation_Date");

                entity.Property(e => e.IsReadyForTruckHirePayment).HasColumnName("Is_Ready_For_Truck_Hire_Payment");

                entity.Property(e => e.PaidCheckNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Paid_Check_Number");

                entity.Property(e => e.PoNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PO_Number");

                entity.Property(e => e.QuickbooksInvoiceNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Quickbooks_Invoice_Number");

                entity.Property(e => e.TotalDue)
                    .HasColumnType("money")
                    .HasColumnName("Total_Due");

                entity.Property(e => e.CreatedBy).HasColumnName("Created_By");

                entity.Property(e => e.UpdatedBy).HasColumnName("Updated_By");

                entity.Property(e => e.LastUpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Last_Updated_Date");
            });

            modelBuilder.Entity<Job>(entity =>
            {
                entity.ToTable("Job");

                entity.HasIndex(e => new { e.Code, e.CustomerId }, "IX_Job")
                    .IsUnique();

                entity.Property(e => e.JobId).HasColumnName("Job_ID");

                entity.Property(e => e.AwardedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Awarded_Date");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerId).HasColumnName("Customer_ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IsComplete).HasColumnName("Is_Complete");

                entity.Property(e => e.Location)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.PoNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PO_Number");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Jobs)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Customer_Job_FK1");

                entity.Property(e => e.CreatedBy).HasColumnName("Created_By");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Creation_Date");

                entity.Property(e => e.UpdatedBy).HasColumnName("Updated_By");

                entity.Property(e => e.LastUpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Last_Updated_Date");
            });

            modelBuilder.Entity<JobProduct>(entity =>
            {
                entity.ToTable("Job_Product");

                entity.HasIndex(e => new { e.JobId, e.Code }, "IX_Job_Product_1")
                    .IsUnique();

                entity.Property(e => e.JobProductId).HasColumnName("Job_Product_ID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DriverPrice)
                    .HasColumnType("money")
                    .HasColumnName("Driver_Price");

                entity.Property(e => e.JobId).HasColumnName("Job_ID");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.ProductId).HasColumnName("Product_ID");

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.JobProducts)
                    .HasForeignKey(d => d.JobId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Job_Job_Product_FK1");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.JobProducts)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Product_Job_Product_FK1");

                entity.Property(e => e.CreatedBy).HasColumnName("Created_By");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Creation_Date");

                entity.Property(e => e.UpdatedBy).HasColumnName("Updated_By");

                entity.Property(e => e.LastUpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Last_Updated_Date");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("Payment");

                entity.Property(e => e.PaymentId).HasColumnName("Payment_ID");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Creation_Date");

                entity.Property(e => e.DateRangeEnd)
                    .HasColumnType("datetime")
                    .HasColumnName("Date_Range_End");

                entity.Property(e => e.DateRangeStart)
                    .HasColumnType("datetime")
                    .HasColumnName("Date_Range_Start");

                entity.Property(e => e.GrossAmount)
                    .HasColumnType("money")
                    .HasColumnName("Gross_Amount");

                entity.Property(e => e.PaymentAmount)
                    .HasColumnType("money")
                    .HasColumnName("Payment_Amount");

                entity.Property(e => e.CreatedBy).HasColumnName("Created_By");

                entity.Property(e => e.UpdatedBy).HasColumnName("Updated_By");

                entity.Property(e => e.LastUpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Last_Updated_Date");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.HasIndex(e => e.Code, "IX_Product")
                    .IsUnique();

                entity.Property(e => e.ProductId).HasColumnName("Product_ID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.CreatedBy).HasColumnName("Created_By");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Creation_Date");

                entity.Property(e => e.UpdatedBy).HasColumnName("Updated_By");

                entity.Property(e => e.LastUpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Last_Updated_Date");
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.ToTable("Ticket");

                entity.HasIndex(e => new { e.JobId, e.Code }, "IX_Ticket")
                    .IsUnique();

                entity.Property(e => e.TicketId).HasColumnName("Ticket_ID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Creation_Date");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.InvoiceId).HasColumnName("Invoice_ID");

                entity.Property(e => e.JobId).HasColumnName("Job_ID");

                entity.Property(e => e.JobProductId).HasColumnName("Job_Product_ID");

                entity.HasOne(d => d.Invoice)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.InvoiceId)
                    .HasConstraintName("Invoice_Ticket_FK1");

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.JobId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Job_Ticket_FK1");

                entity.HasOne(d => d.JobProduct)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.JobProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ticket_Job_Product");

                entity.Property(e => e.CreatedBy).HasColumnName("Created_By");

                entity.Property(e => e.UpdatedBy).HasColumnName("Updated_By");

                entity.Property(e => e.LastUpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Last_Updated_Date");
            });

            modelBuilder.Entity<TicketLineItem>(entity =>
            {
                entity.ToTable("Ticket_Line_Item");

                entity.Property(e => e.TicketLineItemId).HasColumnName("Ticket_Line_Item_ID");

                entity.Property(e => e.DriverId).HasColumnName("Driver_ID");

                entity.Property(e => e.DriverPricePerUnit)
                    .HasColumnType("money")
                    .HasColumnName("Driver_Price_Per_Unit");

                entity.Property(e => e.PaymentId).HasColumnName("Payment_ID");

                entity.Property(e => e.PricePerUnit)
                    .HasColumnType("money")
                    .HasColumnName("Price_Per_Unit");

                entity.Property(e => e.Quantity).HasColumnType("numeric(6, 3)");

                entity.Property(e => e.TicketId).HasColumnName("Ticket_ID");

                entity.Property(e => e.TruckId).HasColumnName("Truck_ID");

                entity.HasOne(d => d.Driver)
                    .WithMany(p => p.TicketLineItems)
                    .HasForeignKey(d => d.DriverId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Driver_Ticket_Line_Item_FK1");

                entity.HasOne(d => d.Payment)
                    .WithMany(p => p.TicketLineItems)
                    .HasForeignKey(d => d.PaymentId)
                    .HasConstraintName("Payment_Ticket_Line_Item_FK1");

                entity.HasOne(d => d.Ticket)
                    .WithMany(p => p.TicketLineItems)
                    .HasForeignKey(d => d.TicketId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Ticket_Ticket_Line_Item_FK1");

                entity.HasOne(d => d.Truck)
                    .WithMany(p => p.TicketLineItems)
                    .HasForeignKey(d => d.TruckId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Truck_Ticket_Line_Item_FK1");

                entity.Property(e => e.CreatedBy).HasColumnName("Created_By");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Creation_Date");

                entity.Property(e => e.UpdatedBy).HasColumnName("Updated_By");

                entity.Property(e => e.LastUpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Last_Updated_Date");
            });

            modelBuilder.Entity<Truck>(entity =>
            {
                entity.ToTable("Truck");

                entity.HasIndex(e => e.Code, "UIX_Truck")
                    .IsUnique();

                entity.Property(e => e.TruckId).HasColumnName("Truck_ID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DefaultDriverId).HasColumnName("Default_Driver_ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.IsInactive)
                    .HasColumnName("Is_Inactive")
                    .HasDefaultValueSql("(0)");

                entity.HasOne(d => d.DefaultDriver)
                    .WithMany(p => p.Trucks)
                    .HasForeignKey(d => d.DefaultDriverId)
                    .HasConstraintName("Driver_Truck_FK1");

                entity.Property(e => e.CreatedBy).HasColumnName("Created_By");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Creation_Date");

                entity.Property(e => e.UpdatedBy).HasColumnName("Updated_By");

                entity.Property(e => e.LastUpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Last_Updated_Date");
            });

            modelBuilder.Entity<VwAvailableTicketsForInvoice>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_Available_Tickets_For_Invoice");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Creation_Date");

                entity.Property(e => e.CustomerDescription)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Customer_Description");

                entity.Property(e => e.JobDescription)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Job_Description");

                entity.Property(e => e.JobId).HasColumnName("Job_ID");

                entity.Property(e => e.JobProductId).HasColumnName("Job_Product_ID");

                entity.Property(e => e.ProductDescription)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Product_Description");

                entity.Property(e => e.TicketDescription)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Ticket_Description");

                entity.Property(e => e.TicketId).HasColumnName("Ticket_ID");

                entity.Property(e => e.Total).HasColumnType("numeric(38, 3)");

                entity.Property(e => e.TotalDue)
                    .HasColumnType("numeric(38, 7)")
                    .HasColumnName("Total_Due");
            });

            modelBuilder.Entity<VwEstimate>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_Estimate");

                entity.Property(e => e.Address1)
                    .HasMaxLength(75)
                    .IsUnicode(false)
                    .HasColumnName("Address_1");

                entity.Property(e => e.Address2)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Address_2");

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedById).HasColumnName("Created_By_ID");

                entity.Property(e => e.CustomerDescription)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Customer_Description");

                entity.Property(e => e.CustomerId).HasColumnName("Customer_ID");

                entity.Property(e => e.Description)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.EnteredDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Entered_Date");

                entity.Property(e => e.EstimateId).HasColumnName("Estimate_ID");

                entity.Property(e => e.ExpirationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Expiration_Date");

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Full_Name");

                entity.Property(e => e.Location)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Total).HasColumnType("money");

                entity.Property(e => e.ZipCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Zip_Code");
            });

            modelBuilder.Entity<VwInvoice>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_Invoice");

                entity.Property(e => e.Address1)
                    .HasMaxLength(75)
                    .IsUnicode(false)
                    .HasColumnName("Address_1");

                entity.Property(e => e.Address2)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Address_2");

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Creation_Date");

                entity.Property(e => e.CustomerDescription)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Customer_Description");

                entity.Property(e => e.InvoiceId).HasColumnName("Invoice_ID");

                entity.Property(e => e.JobDescription)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Job_Description");

                entity.Property(e => e.JobId).HasColumnName("Job_ID");

                entity.Property(e => e.JobProductId).HasColumnName("Job_Product_ID");

                entity.Property(e => e.LineItemTotal)
                    .HasColumnType("numeric(26, 7)")
                    .HasColumnName("Line_Item_Total");

                entity.Property(e => e.Phone)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.PoNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PO_Number");

                entity.Property(e => e.PricePerUnit)
                    .HasColumnType("money")
                    .HasColumnName("Price_Per_Unit");

                entity.Property(e => e.ProductCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Product_Code");

                entity.Property(e => e.ProductDescription)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Product_Description");

                entity.Property(e => e.Quantity).HasColumnType("numeric(6, 3)");

                entity.Property(e => e.State)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.TicketCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Ticket_Code");

                entity.Property(e => e.TicketCreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Ticket_Creation_Date");

                entity.Property(e => e.TicketId).HasColumnName("Ticket_ID");

                entity.Property(e => e.TicketTruck)
                    .IsRequired()
                    .HasMaxLength(101)
                    .IsUnicode(false)
                    .HasColumnName("Ticket_Truck");

                entity.Property(e => e.TotalDue)
                    .HasColumnType("money")
                    .HasColumnName("Total_Due");

                entity.Property(e => e.TruckCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Truck_Code");

                entity.Property(e => e.TruckDescription)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("Truck_Description");

                entity.Property(e => e.ZipCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Zip_Code");
            });

            modelBuilder.Entity<VwInvoiceSummary>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_Invoice_Summary");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Creation_Date");

                entity.Property(e => e.CustomerDescription)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Customer_Description");

                entity.Property(e => e.InvoiceId).HasColumnName("Invoice_ID");

                entity.Property(e => e.IsReadyForTruckHirePayment).HasColumnName("Is_Ready_For_Truck_Hire_Payment");

                entity.Property(e => e.JobDescription)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Job_Description");

                entity.Property(e => e.TotalDue)
                    .HasColumnType("money")
                    .HasColumnName("Total_Due");
            });

            modelBuilder.Entity<VwJob>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_Job");

                entity.Property(e => e.AwardedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Awarded_Date");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CodeDescription)
                    .IsRequired()
                    .HasMaxLength(103)
                    .IsUnicode(false)
                    .HasColumnName("Code_Description");

                entity.Property(e => e.CustomerCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Customer_Code");

                entity.Property(e => e.CustomerDescription)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Customer_Description");

                entity.Property(e => e.CustomerId).HasColumnName("Customer_ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IsComplete).HasColumnName("Is_Complete");

                entity.Property(e => e.JobId).HasColumnName("Job_ID");

                entity.Property(e => e.Location)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.PoNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PO_Number");
            });

            modelBuilder.Entity<VwPaymentDetail>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_Payment_Detail");

                entity.Property(e => e.Address1)
                    .HasMaxLength(75)
                    .IsUnicode(false)
                    .HasColumnName("Address_1");

                entity.Property(e => e.Address2)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Address_2");

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Creation_Date");

                entity.Property(e => e.CustomerDescription)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Customer_Description");

                entity.Property(e => e.DateRangeEnd)
                    .HasColumnType("datetime")
                    .HasColumnName("Date_Range_End");

                entity.Property(e => e.DateRangeStart)
                    .HasColumnType("datetime")
                    .HasColumnName("Date_Range_Start");

                entity.Property(e => e.DriverId).HasColumnName("Driver_ID");

                entity.Property(e => e.DriverPricePerUnit)
                    .HasColumnType("money")
                    .HasColumnName("Driver_Price_Per_Unit");

                entity.Property(e => e.DriverTypeId).HasColumnName("Driver_Type_ID");

                entity.Property(e => e.Fax)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(75)
                    .IsUnicode(false)
                    .HasColumnName("Full_Name");

                entity.Property(e => e.GrossAmount)
                    .HasColumnType("money")
                    .HasColumnName("Gross_Amount");

                entity.Property(e => e.JobDescription)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Job_Description");

                entity.Property(e => e.LineItemTotal)
                    .HasColumnType("numeric(26, 7)")
                    .HasColumnName("Line_Item_Total");

                entity.Property(e => e.PaymentAmount)
                    .HasColumnType("money")
                    .HasColumnName("Payment_Amount");

                entity.Property(e => e.PaymentId).HasColumnName("Payment_ID");

                entity.Property(e => e.Phone)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.ProductDescription)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Product_Description");

                entity.Property(e => e.Quantity).HasColumnType("numeric(6, 3)");

                entity.Property(e => e.State)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.TicketCreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Ticket_Creation_Date");

                entity.Property(e => e.TicketLineItemId).HasColumnName("Ticket_Line_Item_ID");

                entity.Property(e => e.TruckDescription)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("Truck_Description");

                entity.Property(e => e.ZipCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Zip_Code");
            });

            modelBuilder.Entity<VwReportEmployeeDriver>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_Report_Employee_Driver");

                entity.Property(e => e.Address1)
                    .HasMaxLength(75)
                    .IsUnicode(false)
                    .HasColumnName("Address_1");

                entity.Property(e => e.Address2)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Address_2");

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DriverId).HasColumnName("Driver_ID");

                entity.Property(e => e.Fax)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(75)
                    .IsUnicode(false)
                    .HasColumnName("Full_Name");

                entity.Property(e => e.IsInactive).HasColumnName("Is_Inactive");

                entity.Property(e => e.Phone)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.TruckCode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Truck_Code");

                entity.Property(e => e.TruckDescription)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("Truck_Description");

                entity.Property(e => e.TruckId).HasColumnName("Truck_ID");

                entity.Property(e => e.ZipCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Zip_Code");
            });

            modelBuilder.Entity<VwReportJobPrice>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_Report_Job_Price");

                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Customer_Code");

                entity.Property(e => e.CustomerDescription)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Customer_Description");

                entity.Property(e => e.CustomerId).HasColumnName("Customer_ID");

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DriverPrice)
                    .HasColumnType("money")
                    .HasColumnName("Driver_Price");

                entity.Property(e => e.IsComplete).HasColumnName("Is_Complete");

                entity.Property(e => e.IsInActive).HasColumnName("Is_InActive");

                entity.Property(e => e.JobCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Job_Code");

                entity.Property(e => e.JobDescription)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Job_Description");

                entity.Property(e => e.JobId).HasColumnName("Job_ID");

                entity.Property(e => e.Price).HasColumnType("money");
            });

            modelBuilder.Entity<VwReportTruckDetail>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_Report_Truck_Detail");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.DriverCode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Driver_Code");

                entity.Property(e => e.DriverTypeId).HasColumnName("Driver_Type_ID");

                entity.Property(e => e.FullName)
                    .HasMaxLength(75)
                    .IsUnicode(false)
                    .HasColumnName("Full_Name");

                entity.Property(e => e.IsInactive).HasColumnName("Is_Inactive");

                entity.Property(e => e.TruckId).HasColumnName("Truck_ID");
            });

            modelBuilder.Entity<VwReportTruckHire>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_Report_Truck_Hire");

                entity.Property(e => e.Address1)
                    .HasMaxLength(75)
                    .IsUnicode(false)
                    .HasColumnName("Address_1");

                entity.Property(e => e.Address2)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Address_2");

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DriverId).HasColumnName("Driver_ID");

                entity.Property(e => e.Fax)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(75)
                    .IsUnicode(false)
                    .HasColumnName("Full_Name");

                entity.Property(e => e.IsInactive).HasColumnName("Is_Inactive");

                entity.Property(e => e.Phone)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.TruckCode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Truck_Code");

                entity.Property(e => e.TruckDescription)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("Truck_Description");

                entity.Property(e => e.TruckId).HasColumnName("Truck_ID");

                entity.Property(e => e.ZipCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Zip_Code");
            });

            modelBuilder.Entity<VwTentativeInvoice>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_Tentative_Invoice");

                entity.Property(e => e.Address1)
                    .HasMaxLength(75)
                    .IsUnicode(false)
                    .HasColumnName("Address_1");

                entity.Property(e => e.Address2)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Address_2");

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreationDate).HasColumnName("Creation_Date");

                entity.Property(e => e.CustomerDescription)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Customer_Description");

                entity.Property(e => e.InvoiceId).HasColumnName("Invoice_ID");

                entity.Property(e => e.JobDescription)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Job_Description");

                entity.Property(e => e.JobId).HasColumnName("Job_ID");

                entity.Property(e => e.JobProductId).HasColumnName("Job_Product_ID");

                entity.Property(e => e.LineItemTotal)
                    .HasColumnType("numeric(26, 7)")
                    .HasColumnName("Line_Item_Total");

                entity.Property(e => e.Number)
                    .IsRequired()
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.PoNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PO_Number");

                entity.Property(e => e.PricePerUnit)
                    .HasColumnType("money")
                    .HasColumnName("Price_Per_Unit");

                entity.Property(e => e.ProductCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Product_Code");

                entity.Property(e => e.ProductDescription)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Product_Description");

                entity.Property(e => e.ProductId).HasColumnName("Product_ID");

                entity.Property(e => e.Quantity).HasColumnType("numeric(6, 3)");

                entity.Property(e => e.State)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.TicketCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Ticket_Code");

                entity.Property(e => e.TicketCreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Ticket_Creation_Date");

                entity.Property(e => e.TicketId).HasColumnName("Ticket_ID");

                entity.Property(e => e.TicketTruck)
                    .IsRequired()
                    .HasMaxLength(101)
                    .IsUnicode(false)
                    .HasColumnName("Ticket_Truck");

                entity.Property(e => e.TotalDue).HasColumnName("Total_Due");

                entity.Property(e => e.TruckCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Truck_Code");

                entity.Property(e => e.TruckDescription)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("Truck_Description");

                entity.Property(e => e.ZipCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Zip_Code");
            });

            modelBuilder.Entity<VwTicket>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_Ticket");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Creation_Date");

                entity.Property(e => e.CustomerCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Customer_Code");

                entity.Property(e => e.CustomerDescription)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Customer_Description");

                entity.Property(e => e.CustomerId).HasColumnName("Customer_ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.InvoiceId).HasColumnName("Invoice_ID");

                entity.Property(e => e.JobCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Job_Code");

                entity.Property(e => e.JobDescription)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Job_Description");

                entity.Property(e => e.JobId).HasColumnName("Job_ID");

                entity.Property(e => e.JobProductId).HasColumnName("Job_Product_ID");

                entity.Property(e => e.TicketId).HasColumnName("Ticket_ID");
            });

            modelBuilder.Entity<VwTicketLineItem>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_Ticket_Line_Item");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Creation_Date");

                entity.Property(e => e.DriverCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Driver_Code");

                entity.Property(e => e.DriverId).HasColumnName("Driver_ID");

                entity.Property(e => e.DriverPricePerUnit)
                    .HasColumnType("money")
                    .HasColumnName("Driver_Price_Per_Unit");

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(75)
                    .IsUnicode(false)
                    .HasColumnName("Full_Name");

                entity.Property(e => e.PaymentId).HasColumnName("Payment_ID");

                entity.Property(e => e.PricePerUnit)
                    .HasColumnType("money")
                    .HasColumnName("Price_Per_Unit");

                entity.Property(e => e.Quantity).HasColumnType("numeric(6, 3)");

                entity.Property(e => e.TicketId).HasColumnName("Ticket_ID");

                entity.Property(e => e.TicketLineItemId).HasColumnName("Ticket_Line_Item_ID");

                entity.Property(e => e.TruckCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Truck_Code");

                entity.Property(e => e.TruckDescription)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("Truck_Description");

                entity.Property(e => e.TruckId).HasColumnName("Truck_ID");
            });

            modelBuilder.Entity<VwTicketsForPayment>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_Tickets_For_Payment");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Creation_Date");

                entity.Property(e => e.DriverId).HasColumnName("Driver_ID");

                entity.Property(e => e.DriverPricePerUnit)
                    .HasColumnType("money")
                    .HasColumnName("Driver_Price_Per_Unit");

                entity.Property(e => e.DriverTypeId).HasColumnName("Driver_Type_ID");

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(75)
                    .IsUnicode(false)
                    .HasColumnName("Full_Name");

                entity.Property(e => e.InvoiceCreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Invoice_Creation_Date");

                entity.Property(e => e.IsReadyForTruckHirePayment).HasColumnName("Is_Ready_For_Truck_Hire_Payment");

                entity.Property(e => e.JobDescription)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Job_Description");

                entity.Property(e => e.PaymentId).HasColumnName("Payment_ID");

                entity.Property(e => e.ProductDescription)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Product_Description");

                entity.Property(e => e.Quantity).HasColumnType("numeric(6, 3)");

                entity.Property(e => e.TicketGross)
                    .HasColumnType("numeric(26, 7)")
                    .HasColumnName("Ticket_Gross");

                entity.Property(e => e.TicketLineItemId).HasColumnName("Ticket_Line_Item_ID");

                entity.Property(e => e.TicketTotalPayment)
                    .HasColumnType("numeric(38, 7)")
                    .HasColumnName("Ticket_Total_Payment");

                entity.Property(e => e.TruckNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Truck_Number");
            });

            modelBuilder.Entity<VwTruckTotal>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_Truck_Totals");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Creation_Date");

                entity.Property(e => e.QuantityTotal)
                    .HasColumnType("numeric(38, 3)")
                    .HasColumnName("Quantity_Total");

                entity.Property(e => e.TicketTotal)
                    .HasColumnType("numeric(38, 7)")
                    .HasColumnName("Ticket_Total");

                entity.Property(e => e.TruckCode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Truck_Code");

                entity.Property(e => e.TruckDescription)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("Truck_Description");
            });

            modelBuilder.Entity<VwUnpaidInvoice>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_Unpaid_Invoices");

                entity.Property(e => e.CustomerId).HasColumnName("Customer_ID");

                entity.Property(e => e.InvoiceId).HasColumnName("Invoice_ID");

                entity.Property(e => e.TotalDue)
                    .HasColumnType("money")
                    .HasColumnName("Total_Due");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
