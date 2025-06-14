using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Backend.Models.JobOffer;

namespace Backend.Models.Review;

public class Review
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Comment is required.")]
    public string Comment { get; set; }

    [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
    public int Rating { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Required]
    [ForeignKey("Company")]
    public int CompanyId { get; set; }
    public Company Company { get; set; }
    
    [ForeignKey("User")]
    public string? UserId { get; set; }
    public User? User { get; set; }
}