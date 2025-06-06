using System.ComponentModel.DataAnnotations;

namespace Backend.Models.Review;

public class ReviewEditRequest
{
    [Required]
    public int ReviewId { get; set; }
    public string Comment { get; set; }
    public int Rating { get; set; }
}