namespace Scraper;
using System.Collections.Generic;

public class JobOffer
{
    public string Slug { get; set; }
    public string Title { get; set; }
    public List<string> RequiredSkills { get; set; }
    public object NiceToHaveSkills { get; set; }
    public string WorkplaceType { get; set; }
    public string WorkingTime { get; set; }
    public string ExperienceLevel { get; set; }
    public List<EmploymentType> EmploymentTypes { get; set; }
    public int CategoryId { get; set; }
    public List<MultiLocation> MultiLocation { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public string Latitude { get; set; }
    public string Longitude { get; set; }
    public bool RemoteInterview { get; set; }
    public string CompanyName { get; set; }
    public string CompanyLogoThumbUrl { get; set; }
    public string PublishedAt { get; set; }
    public bool OpenToHireUkrainians { get; set; }
}
