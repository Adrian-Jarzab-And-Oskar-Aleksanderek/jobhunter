using System.Text.Json.Serialization;
using Backend.Models.JobOffer;

namespace Backend.DTO.JobOffer;

public class SkillDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("category")]
    public SkillCategory Category { get; set; }

}