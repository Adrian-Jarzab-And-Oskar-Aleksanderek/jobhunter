using System.ComponentModel.DataAnnotations;

namespace Backend.Models.Review;

public class ReviewDeleteRequest
{
    [Required]
    public int ReviewId { get; set; }
}