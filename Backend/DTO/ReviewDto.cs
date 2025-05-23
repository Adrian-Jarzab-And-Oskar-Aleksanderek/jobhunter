using Microsoft.AspNetCore.Identity;

namespace Backend.DTO;

public class ReviewDto
{
    public int Id { get; set; }

    public string Comment { get; set; }

    public int Rating { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    

    public string? User { get; set; }
}