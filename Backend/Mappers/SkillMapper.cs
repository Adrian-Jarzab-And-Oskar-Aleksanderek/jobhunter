using Backend.DTO.JobOffer;
using Backend.Models.JobOffer;

namespace Backend.Mappers;

public static class SkillMapper
{
    public static SkillDto MapSkillDto(this Skill skill)
    {
        return new SkillDto
        { 
            Id = skill.Id,
            Name = skill.Name,
            Category = skill.Category
        };
    }
    public static List<SkillDto> MapSkillDto(this ICollection<Skill> skills)
    {
        return skills.Select(s => s.MapSkillDto()).ToList();
    }
}