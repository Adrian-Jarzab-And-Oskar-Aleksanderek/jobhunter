using System.Text.Json.Serialization;
using Backend.Models.JobOffer;

namespace Backend.DTO.JobOffer;

public class RequiredSkillsDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("skills")]
    public ICollection<SkillDto> Skills { get; set; } = new List<SkillDto>();
}
