using Backend.Migrations;
using Backend.Models;
using Backend.Models.Review;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Backend.Models.JobOffer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{


    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<JobOffer> JobOffers { get; set; }
        public DbSet<MultiLocation> MultiLocations { get; set; }
        public DbSet<EmploymentType> EmploymentTypes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                },
            };
            modelBuilder.Entity<IdentityRole>().HasData(roles);
            
           // Relation 1:N z JobOffer -> MultiLocation
           modelBuilder.Entity<MultiLocation>()
               .HasOne(m => m.JobOffer)
               .WithMany(j => j.MultiLocation)
               .HasForeignKey(m => m.JobOfferId)
               .OnDelete(DeleteBehavior.Cascade);

            // Relation 1:N z JobOffer -> EmploymentType
            modelBuilder.Entity<EmploymentType>()
                .HasOne(e => e.JobOffer)
                .WithMany(j => j.EmploymentTypes)
                .HasForeignKey(e => e.JobOfferId)
                .OnDelete(DeleteBehavior.Cascade);
            
            // Relation 1:N z JobOffer -> Review
            modelBuilder.Entity<Review>()
                .HasOne(r => r.JobOffer)
                .WithMany(j => j.Reviews)
                .HasForeignKey(r => r.JobOfferId)
                .OnDelete(DeleteBehavior.Cascade);
            
            // Relation 1:N z Review -> User
            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}