using System.Text.Json.Serialization;
using Backend.Models.Review;

namespace Backend.DTO.JobOffer;

public class CompanyDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("description")]
    public string? Description { get; set; }
    
    [JsonPropertyName("logoUrl")]
    public string? LogoUrl { get; set; }
    
    [JsonPropertyName("reviews")]

    public ICollection<Review>? Reviews { get; set; } = new List<Review>();

} 