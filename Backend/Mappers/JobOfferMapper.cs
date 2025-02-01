using Backend.DTO;
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
            RequiredSkills = jobOffer.RequiredSkills,
            NiceToHaveSkills = jobOffer.NiceToHaveSkills,
            WorkplaceType = jobOffer.WorkplaceType,
            WorkingTime = jobOffer.WorkingTime,
            ExperienceLevel = jobOffer.ExperienceLevel,
            
            Reviews = jobOffer.Reviews
                .Select(review => ReviewMapper.MapToReviewDto(review))
                .ToList(),
            
            EmploymentTypes = jobOffer.EmploymentTypes
                .Select(employmentType => EmploymentTypeMapper.ToEmploymentType(employmentType))
                .ToList(),
            
            MultiLocation = jobOffer.MultiLocation
                .Select(location => MultiLocationMapper.MapMultiLocationDto(location))
                .ToList(),
            
            City = jobOffer.City,
            Street = jobOffer.Street,
            RemoteInterview = jobOffer.RemoteInterview,
            CompanyName = jobOffer.CompanyName,
        };
    }
}
