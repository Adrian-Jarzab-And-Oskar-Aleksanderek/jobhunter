using System.Text.Json.Serialization;

namespace Backend.DTO.JobOffer;

public class MultiLocationDto
{
    [JsonPropertyName("city")]
    public string City { get; set; }
    
    [JsonPropertyName("slug")]
    public string Slug { get; set; }
    
    [JsonPropertyName("street")]
    public string Street { get; set; }
}