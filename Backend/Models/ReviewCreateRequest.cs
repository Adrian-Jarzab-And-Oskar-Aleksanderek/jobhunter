namespace Backend.Models;

public class ReviewCreateRequest
{
    public int JobOfferId { get; set; }
    public string Comment { get; set; }
    public int Rating { get; set; }
}