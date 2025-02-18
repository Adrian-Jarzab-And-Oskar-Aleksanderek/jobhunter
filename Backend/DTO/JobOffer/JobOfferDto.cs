using System.Text.Json.Serialization;

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
    public List<string> RequiredSkills { get; set; }
    
    [JsonPropertyName("niceToHaveSkills")]
    public List<string>? NiceToHaveSkills { get; set; }
    
    [JsonPropertyName("workplaceType")]
    public string WorkplaceType { get; set; }
    
    [JsonPropertyName("workingTime")]
    public string WorkingTime { get; set; }
    
    [JsonPropertyName("experienceLevel")]
    public string ExperienceLevel { get; set; }
    
    [JsonPropertyName("reviews")]
    public ICollection<ReviewDto>? Reviews { get; set; } = new List<ReviewDto>();
    
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
    
    [JsonPropertyName("companyName")]
    public string CompanyName { get; set; }
}