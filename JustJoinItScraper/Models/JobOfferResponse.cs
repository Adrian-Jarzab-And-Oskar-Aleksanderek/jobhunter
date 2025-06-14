using System.Text.Json.Serialization;
using Backend.Models.JobOffer;
namespace Scraper.Models;

public class JobOfferResponse
{
    public List<JobOfferData> Data { get; set; }
    public Meta Meta { get; set; }
}

public class JobOfferData
{
    [JsonPropertyName("slug")]
    public string Slug { get; set; }
    
    [JsonPropertyName("title")]
    public string Title { get; set; }
    
    [JsonPropertyName("requiredSkills")]
    public List<string>? RequiredSkills { get; set; }
    
    [JsonPropertyName("niceToHaveSkills")]
    public List<string>? NiceToHaveSkills { get; set; }
    
    [JsonPropertyName("workplaceType")]
    public string? WorkplaceType { get; set; }
    
    [JsonPropertyName("workingTime")]
    public string? WorkingTime { get; set; }
    
    [JsonPropertyName("experienceLevel")]
    public string? ExperienceLevel { get; set; }
    
    [JsonPropertyName("categoryId")]
    public int CategoryId { get; set; }
    
    [JsonPropertyName("city")]
    public string? City { get; set; }
    
    [JsonPropertyName("street")]
    public string? Street { get; set; }
    
    [JsonPropertyName("latitude")]
    public double Latitude { get; set; }
    
    [JsonPropertyName("longitude")]
    public double Longitude { get; set; }
    
    [JsonPropertyName("remoteInterview")]
    public bool RemoteInterview { get; set; }
    
    [JsonPropertyName("companyName")]
    public string CompanyName { get; set; }
    
    [JsonPropertyName("companyLogoThumbUrl")]
    public string? CompanyLogoThumbUrl { get; set; }
    
    [JsonPropertyName("publishedAt")]
    public string PublishedAt { get; set; }
    
    [JsonPropertyName("openToHireUkrainians")]
    public bool OpenToHireUkrainians { get; set; }
    
    [JsonPropertyName("multiLocation")]
    public List<MultiLocationData>? MultiLocation { get; set; }
    
    [JsonPropertyName("employmentTypes")]
    public List<EmploymentTypeData>? EmploymentTypes { get; set; }

}

public class MultiLocationData
{
    [JsonPropertyName("city")]
    public string City { get; set; }
    
    [JsonPropertyName("slug")]
    public string Slug { get; set; }
    
    [JsonPropertyName("street")]
    public string Street { get; set; }
    
    [JsonPropertyName("latitude")]
    public double Latitude { get; set; }
    
    [JsonPropertyName("longitude")]
    public double Longitude { get; set; }
}

public class EmploymentTypeData
{
    [JsonPropertyName("to")]
    public double? To { get; set; }
    
    [JsonPropertyName("from")]
    public double? From { get; set; }
    
    [JsonPropertyName("type")]
    public string Type { get; set; }
    
    [JsonPropertyName("gross")]
    public bool Gross { get; set; }
    
    [JsonPropertyName("to_Chf")]
    public double? To_Chf { get; set; }
    
    [JsonPropertyName("to_Eur")]
    public double? To_Eur { get; set; }
    
    [JsonPropertyName("to_Gbp")]
    public double? To_Gbp { get; set; }
    
    [JsonPropertyName("to_Pln")]
    public double? To_Pln { get; set; }
    
    [JsonPropertyName("to_usd")]
    public double? To_usd { get; set; }
    
    [JsonPropertyName("currency")]
    public string Currency { get; set; }
    
    [JsonPropertyName("from_Chf")]
    public double? From_Chf { get; set; }
    
    [JsonPropertyName("from_Eur")]
    public double? From_Eur { get; set; }
    
    [JsonPropertyName("from_Gbp")]
    public double? From_Gbp { get; set; }
    
    [JsonPropertyName("from_Pln")]
    public double? From_Pln { get; set; }
    
    [JsonPropertyName("from_Usd")]
    public double? From_Usd { get; set; }
}