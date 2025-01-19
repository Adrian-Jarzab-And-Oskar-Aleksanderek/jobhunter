namespace Backend.Models;

public class ReviewEditRequest
{
    public int ReviewId { get; set; }
    public string Comment { get; set; }
    public int Rating { get; set; }
}