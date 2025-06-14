using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models.JobOffer;

public class NiceToHaveSkills
{
    public int Id { get; set; }
    
    [ForeignKey("JobOffer")]
    public int JobOfferId { get; set; }
    public JobOffer JobOffer { get; set; }
    
    public ICollection<Skill> Skills { get; set; } = new List<Skill>();
}