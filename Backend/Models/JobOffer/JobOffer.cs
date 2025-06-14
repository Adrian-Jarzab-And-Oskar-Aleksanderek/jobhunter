using System.ComponentModel.DataAnnotations;

namespace Backend.Models.JobOffer
{
    public class JobOffer
    {
        public int Id { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
        public string WorkplaceType { get; set; }
        public string WorkingTime { get; set; }
        public string ExperienceLevel { get; set; }
        public int CategoryId { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool RemoteInterview { get; set; }
        public string CompanyLogoThumbUrl { get; set; }
        public string PublishedAt { get; set; }
        public bool OpenToHireUkrainians { get; set; }
        public ICollection<EmploymentType>? EmploymentTypes { get; set; } = new List<EmploymentType>();
        public ICollection<MultiLocation>? MultiLocation { get; set; } = new List<MultiLocation>();
        public List<RequierdSkills> RequierdSkills { get; set; } = new List<RequierdSkills>();
        public List<NiceToHaveSkills> NiceToHaveSkills { get; set; } = new List<NiceToHaveSkills>();

        [Required]
        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}