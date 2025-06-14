using Backend.DTO.JobOffer;
using Backend.Models.JobOffer;
namespace Backend.Mappers;

public static class JobOfferMapper
{
    public static JobOfferDto MapToJobOfferDto(this JobOffer jobOffer)
    {
        return new JobOfferDto
        {
            Id = jobOffer.Id,
            Slug = jobOffer.Slug,
            Title = jobOffer.Title,
            RequiredSkills = jobOffer.RequierdSkills
                .Select(requierdSkills => RequiredSkillsMapper.MapRequiredSkillsDto(requierdSkills))
                .ToList(),
            NiceToHaveSkills = jobOffer.NiceToHaveSkills,
            WorkplaceType = jobOffer.WorkplaceType,
            WorkingTime = jobOffer.WorkingTime,
            ExperienceLevel = jobOffer.ExperienceLevel,
            
            EmploymentTypes = jobOffer.EmploymentTypes
                .Select(employmentType => EmploymentTypeMapper.ToEmploymentTypeDto(employmentType))
                .ToList(),
            
            MultiLocation = jobOffer.MultiLocation
                .Select(location => MultiLocationMapper.MapMultiLocationDto(location))
                .ToList(),
            
            City = jobOffer.City,
            Street = jobOffer.Street,
            RemoteInterview = jobOffer.RemoteInterview,
            Company = CompanyMapper.MapCompanyDto(jobOffer.Company),
        };
    }
}
