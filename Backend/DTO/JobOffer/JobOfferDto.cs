using System.Text.Json.Serialization;
using Backend.Models.JobOffer;

namespace Backend.DTO.JobOffer;

public class JobOfferDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("slug")]
    public string Slug { get; set; }
    
    [JsonPropertyName("title")]
    public string Title { get; set; }
    
    [JsonPropertyName("requiredSkills")]
    public List<RequiredSkillsDto> RequiredSkills { get; set; }
    
    [JsonPropertyName("niceToHaveSkills")]
    public List<NiceToHaveSkills> NiceToHaveSkills { get; set; }
    
    [JsonPropertyName("workplaceType")]
    public string WorkplaceType { get; set; }
    
    [JsonPropertyName("workingTime")]
    public string WorkingTime { get; set; }
    
    [JsonPropertyName("experienceLevel")]
    public string ExperienceLevel { get; set; }
    
    [JsonPropertyName("employmentTypes")]
    public ICollection<EmploymentTypeDto>? EmploymentTypes { get; set; } = new List<EmploymentTypeDto>();
    
    [JsonPropertyName("multiLocation")]
    public ICollection<MultiLocationDto>? MultiLocation { get; set; } = new List<MultiLocationDto>();
    
    [JsonPropertyName("city")]
    public string City { get; set; }
    
    [JsonPropertyName("street")]
    public string Street { get; set; }
    
    [JsonPropertyName("remoteInterview")]
    public bool RemoteInterview { get; set; }
    
    [JsonPropertyName("company")]
    public CompanyDto Company { get; set; }

}