namespace Backend.Models.JobOffer;

public class JobOfferRequiredSkills
{
    public int JobOfferId { get; set; }
    public JobOffer JobOffer { get; set; }

    public int SkillId { get; set; }
    public Skill Skill { get; set; }
}