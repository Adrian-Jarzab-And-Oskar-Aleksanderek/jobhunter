namespace Backend.Models.JobOffer;

using System.Collections.Generic;

public class Skill
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public int? CategoryId { get; set; }
    public SkillCategory Category { get; set; }

    public List<JobOffer> JobOffers { get; set; } = new();
}
