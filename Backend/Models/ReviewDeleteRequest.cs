using System.ComponentModel.DataAnnotations;

namespace Backend.Models;

public class ReviewDeleteRequest
{
    [Required]
    public int ReviewId { get; set; }
}