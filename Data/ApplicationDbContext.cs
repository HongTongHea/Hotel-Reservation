using Hotel_Reservation.Models;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Reservation.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<AppUser> AppUser { get; set; }

        public DbSet<AppUserPermission> AppUserPermissions { get; set; }

        public DbSet<Customer> Customer { get; set; }

        public DbSet<Room> Room { get; set; }

        public DbSet<Reservation> Reservation { get; set; }

        public DbSet<Employee> Employee { get; set; }

        public DbSet<Services> Services { get; set; }

        public DbSet<Billing> Billing { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUser>()
                .HasMany(u => u.Permissions)
                .WithOne(p => p.AppUser)
                .HasForeignKey(p => p.AppUserID);

            base.OnModelCreating(modelBuilder);
        }
    }
}
