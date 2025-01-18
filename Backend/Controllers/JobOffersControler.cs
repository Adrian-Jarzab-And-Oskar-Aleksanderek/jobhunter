using Backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers;

[ApiController]
public class JobOffersControler :ControllerBase
{
    private readonly ApplicationDbContext _context;

    public JobOffersControler(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("/api/offers/page/{id}")]

    public IActionResult GetAllJobOffers([FromRoute] int id)
    {
        var jobOffers = _context.JobOffers
            .Include(j => j.Reviews)
            .Include(j => j.MultiLocation)
            .Include(j => j.EmploymentTypes)
            .Skip(30 * id)
            .Take(30)
            .ToList();



        return Ok(jobOffers);
    }

    [HttpGet("/api/offer/{id}")]
    public IActionResult GetJobOfferById([FromRoute] int id)
    {
        var jobOffer = _context.JobOffers.Find(id);
        if (jobOffer == null)
        {
            return NotFound();
        }

        return Ok(jobOffer);
    }
}