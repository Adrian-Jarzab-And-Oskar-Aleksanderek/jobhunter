using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace Backend.Data
{
    using Models;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<JobOffer> JobOffers { get; set; }
        public DbSet<MultiLocation> MultiLocations { get; set; }
        public DbSet<EmploymentType> EmploymentTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relacja 1:Wielu z JobOffer -> MultiLocation
            modelBuilder.Entity<MultiLocation>()
                .HasOne(m => m.JobOffer)
                .WithMany(j => j.MultiLocation)
                .HasForeignKey(m => m.JobOfferId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relacja 1:Wielu z JobOffer -> EmploymentType
            modelBuilder.Entity<EmploymentType>()
                .HasOne(e => e.JobOffer)
                .WithMany(j => j.EmploymentTypes)
                .HasForeignKey(e => e.JobOfferId)
                .OnDelete(DeleteBehavior.Cascade);
            
            // Relacja 1:Wielu z JobOffer -> Review
            modelBuilder.Entity<Review>()
                .HasOne(r => r.JobOffer)
                .WithMany(j => j.Reviews)
                .HasForeignKey(r => r.JobOfferId)
                .OnDelete(DeleteBehavior.Cascade);
            
            // Relacja 1:Wielu z Review -> User
            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}