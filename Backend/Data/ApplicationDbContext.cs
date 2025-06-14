using Backend.Models;
using Backend.Models.Review;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Backend.Models.JobOffer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
    {
        public DbSet<JobOffer> JobOffers { get; set; }
        public DbSet<MultiLocation> MultiLocations { get; set; }
        public DbSet<EmploymentType> EmploymentTypes { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<SkillCategory> SkillCategories { get; set; }
        public DbSet<RequierdSkills> RequierdSkills { get; set; }
        public DbSet<NiceToHaveSkills> NiceToHaveSkills { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Company> Companies { get; set; }

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
            
            // Relation 1:N z Company -> Review
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Company)
                .WithMany(c => c.Reviews)
                .HasForeignKey(r => r.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);
            
            // Relation 1:N z Review -> User
            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            // Relation 1:N z JobOffer -> RequierdSkills
            modelBuilder.Entity<RequierdSkills>()
                .HasOne(rs => rs.JobOffer)
                .WithMany(j => j.RequierdSkills)
                .HasForeignKey(rs => rs.JobOfferId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relation 1:N z JobOffer -> NiceToHaveSkills
            modelBuilder.Entity<NiceToHaveSkills>()
                .HasOne(ns => ns.JobOffer)
                .WithMany(j => j.NiceToHaveSkills)
                .HasForeignKey(ns => ns.JobOfferId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relation 1:N Company -> JobOffer
            modelBuilder.Entity<JobOffer>()
                .HasOne(j => j.Company)
                .WithMany(c => c.JobOffers)
                .HasForeignKey(j => j.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relation 1:N SkillCategory -> Skill
            modelBuilder.Entity<Skill>()
                .HasOne(s => s.Category)
                .WithMany(c => c.Skills)
                .HasForeignKey(s => s.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);

            // Relation M:N RequierdSkills <-> Skills
            modelBuilder.Entity<RequierdSkills>()
                .HasMany(rs => rs.Skills)
                .WithMany()
                .UsingEntity(j => j.ToTable("RequierdSkills_Skills"));

            // Relation M:N NiceToHaveSkills <-> Skills
            modelBuilder.Entity<NiceToHaveSkills>()
                .HasMany(ns => ns.Skills)
                .WithMany()
                .UsingEntity(j => j.ToTable("NiceToHaveSkills_Skills"));
        }
    }
}