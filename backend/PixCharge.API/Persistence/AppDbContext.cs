using Microsoft.EntityFrameworkCore;
using PixCharge.API.Models;

namespace PixCharge.API.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Client> Clients => Set<Client>();
        public DbSet<Pix> PixCharges => Set<Pix>();
        public DbSet<PixRecurringCharge> PixRecurringCharges => Set<PixRecurringCharge>();
        public DbSet<Notification> Notifications => Set<Notification>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User -> Clients
            modelBuilder.Entity<User>()
                .HasMany(u => u.Clients)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // User -> PixCharges
            modelBuilder.Entity<User>()
                .HasMany(u => u.Charges)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Client -> PixCharges
            modelBuilder.Entity<Client>()
                .HasMany(c => c.Charges)
                .WithOne(p => p.Client)
                .HasForeignKey(p => p.ClientId)
                .OnDelete(DeleteBehavior.Cascade);

            // Pix.Amount com precisão
            modelBuilder.Entity<Pix>()
                .Property(p => p.Amount)
                .HasColumnType("decimal(18,2)");

            // PixRecurringCharge relationships
            modelBuilder.Entity<PixRecurringCharge>()
                .HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PixRecurringCharge>()
                .HasOne(r => r.Client)
                .WithMany()
                .HasForeignKey(r => r.ClientId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PixRecurringCharge>()
                .Property(r => r.Amount)
                .HasColumnType("decimal(18,2)");

            // Notification constraints
            modelBuilder.Entity<Notification>()
                .Property(n => n.Message)
                .HasMaxLength(2000);
        }
    }
}
