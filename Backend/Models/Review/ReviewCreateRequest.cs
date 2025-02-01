using System.ComponentModel.DataAnnotations;

namespace Backend.Models.Review;

public class ReviewCreateRequest
{
    [Required]
    public int JobOfferId { get; set; }
    public string Comment { get; set; }
    [Required]
    public int Rating { get; set; }
}