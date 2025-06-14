using System.ComponentModel.DataAnnotations;

namespace Backend.Models.JobOffer;

public class Company
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }
    
    [MaxLength(500)]
    public string Description { get; set; }
    
    public string LogoUrl { get; set; }
    
    public ICollection<JobOffer> JobOffers { get; set; }
    
    public ICollection<Review.Review>? Reviews { get; set; } = new List<Review.Review>();

}