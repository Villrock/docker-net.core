using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QFlow.Models.DataModels;
using QFlow.Models.DataModels.Alerts;
using QFlow.Models.DataModels.Emails;
using QFlow.Models.DataModels.Requests;
using QFlow.Models.DataModels.Users;

namespace QFlow.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Manager> Managers { get; set; }

        public DbSet<Request> Requests { get; set; }
        public DbSet<RequestDetail> RequestDetails { get; set; }

        public DbSet<RequestStatus> RequestStatuses { get; set; }

        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<ManufacturerModel> ManufacturerModels { get; set; }

        public DbSet<Message> Messages { get; set; }
        public DbSet<RequestStatusMessage> RequestStatusMessages { get; set; }

        public DbSet<EmailNotification> EmailNotifications { get; set; }
        public DbSet<AlertNotification> AlertNotifications { get; set; }

        public DbSet<AlertType> AlertTypes { get; set; }
        public DbSet<PeriodTime> PeriodTimes { get; set; }
        public DbSet<AlertTypePeriodTime> AlertTypePeriodTime { get; set; }

        public DbSet<Currency> Currencies { get; set; }

        public ApplicationDbContext( DbContextOptions<ApplicationDbContext> options )
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Request>()
                .HasIndex(r => r.Title);

            modelBuilder.Entity<Client>()
                .HasIndex(c => new { c.FirstName, c.LastName });
        }
    }
}
