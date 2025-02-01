using Backend.DTO;
using Backend.DTO.JobOffer;
using Backend.Models.JobOffer;

namespace Backend.Mappers;

public static class EmploymentTypeMapper
{
    public static EmploymentTypeDto ToEmploymentType(this EmploymentType employmentType)
    {
        return new EmploymentTypeDto
        {

            Id = employmentType.Id,
            To = employmentType.To,
            From = employmentType.From,
            Type = employmentType.Type,
            Gross = employmentType.Gross,
            To_Chf = employmentType.To_Chf,
            To_Eur = employmentType.To_Eur,
            To_Gbp = employmentType.To_Gbp,
            To_Pln = employmentType.To_Pln,
            To_usd = employmentType.To_usd,
            Currency = employmentType.Currency,
            From_Chf = employmentType.From_Chf,
            From_Eur = employmentType.From_Eur,
            From_Gbp = employmentType.From_Gbp,
            From_Pln = employmentType.From_Pln,
            From_Usd = employmentType.From_Usd,
            JobOfferId = employmentType.JobOfferId,
        };
    }
}