using Backend.DTO.JobOffer;
using Backend.Models.JobOffer;

namespace Backend.Mappers;

public static class CompanyMapper
{
    public static CompanyDto MapCompanyDto(this Company company)
    {
        return new CompanyDto
        {
        Id = company.Id,
        Name = company.Name,
        Description = company.Description,
        Reviews = company.Reviews,
        LogoUrl = company.LogoUrl
        };
    }
}