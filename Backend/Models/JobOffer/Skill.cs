namespace Backend.Models.JobOffer;

using System.Collections.Generic;

public class Skill
{
    public int Id { get; set; }
    public string Name { get; set; }

    public List<JobOfferRequiredSkills> JobOfferRequiredSkills { get; set; } = new List<JobOfferRequiredSkills>();
    }
