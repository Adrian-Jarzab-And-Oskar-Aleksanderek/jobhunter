using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class JobOffer
    {
        public int Id { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
        public List<string> RequiredSkills { get; set; }
        public List<string>? NiceToHaveSkills { get; set; }
        public string WorkplaceType { get; set; }
        public string WorkingTime { get; set; }
        public string ExperienceLevel { get; set; }
        public ICollection<Review>? Reviews { get; set; } = new List<Review>();
        public List<EmploymentType> EmploymentTypes { get; set; }
        public int CategoryId { get; set; }
        public List<MultiLocation> MultiLocation { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool RemoteInterview { get; set; }
        public string CompanyName { get; set; }
        public string CompanyLogoThumbUrl { get; set; }
        public string PublishedAt { get; set; }
        public bool OpenToHireUkrainians { get; set; }
    }

}