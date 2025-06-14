using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Data;

namespace Backend.Controllers;

[Route("api/[Controller]")]
[ApiController]
public class CompanyController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CompanyController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCompany(int id)
    {
        var company = await _context.Companies
            .Include(c => c.JobOffers)
            .ThenInclude(j => j.EmploymentTypes)
            .Include(c => c.Reviews)
            .ThenInclude(r => r.User)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (company == null)
            return NotFound();

        return Ok(new
        {
            id = company.Id,
            name = company.Name,
            description = company.Description,
            logoUrl = company.LogoUrl,
            jobOffers = company.JobOffers.Select(j => new
            {
                id = j.Id,
                title = j.Title,
                city = j.City,
                workplaceType = j.WorkplaceType,
                experienceLevel = j.ExperienceLevel,
                employmentTypes = j.EmploymentTypes.Select(et => new
                {
                    type = et.Type,
                    from = et.From,
                    to = et.To,
                    currency = et.Currency,
                    gross = et.Gross
                })
            }),
            reviews = company.Reviews.Select(r => new
            {
                id = r.Id,
                comment = r.Comment,
                rating = r.Rating,
                createdAt = r.CreatedAt,
                user = r.User?.UserName
            })
        });
    }
} 