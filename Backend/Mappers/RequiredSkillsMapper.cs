using Backend.DTO.JobOffer;
using Backend.Models.JobOffer;

namespace Backend.Mappers;

public static class RequiredSkillsMapper
{
    public static RequiredSkillsDto MapRequiredSkillsDto(this RequierdSkills requierdSkill)
    {
        return new RequiredSkillsDto
        { 
            Id = requierdSkill.Id,
            Skills = SkillMapper.MapSkillDto(requierdSkill.Skills)
        };
    }
    
}