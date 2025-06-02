using Microsoft.EntityFrameworkCore;
using PixCharge.API.Models;
using PixCharge.API.Services;

namespace PixCharge.API.Persistence
{
    public class AppDbContext : DbContext
    {
        private readonly IUserContextService _userContext;
        public AppDbContext(DbContextOptions<AppDbContext> options, IUserContextService userContext)
            : base(options) { _userContext = userContext; }

        public DbSet<User> Users => Set<User>();
        public DbSet<Client> Clients => Set<Client>();
        public DbSet<Pix> PixCharges => Set<Pix>();
        public DbSet<PixRecurringCharge> PixRecurringCharges => Set<PixRecurringCharge>();
        public DbSet<Notification> Notifications => Set<Notification>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var userId = _userContext.UserId;

            // User -> Clients
            modelBuilder.Entity<User>()
                .HasMany(u => u.Clients)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Client>().HasQueryFilter(c => c.UserId == userId);

            // User -> PixCharges
            modelBuilder.Entity<User>()
                .HasMany(u => u.Charges)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Pix>().HasQueryFilter(p => p.UserId == userId);

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

            modelBuilder.Entity<PixRecurringCharge>().HasQueryFilter(r => r.UserId == userId);

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

            modelBuilder.Entity<Notification>()
                .HasOne(n => n.User)
                .WithMany()
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Notification>()
                .HasOne(n => n.Client)
                .WithMany()
                .HasForeignKey(n => n.ClientId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Notification>()
                .HasOne(n => n.PixCharge)
                .WithMany()
                .HasForeignKey(n => n.PixChargeId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Notification>().HasQueryFilter(n => n.UserId == userId);
            
        }
    }
}
