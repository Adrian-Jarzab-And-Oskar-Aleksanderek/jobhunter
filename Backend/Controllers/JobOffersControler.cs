using Backend.Data;
using Microsoft.AspNetCore.Authorization;
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
    // [Authorize]
    [HttpGet("/api/offers")]
    public IActionResult GetAllJobOffers([FromQuery] int page)
    {
        int resultsPerPage = 30;
        int totalResults = _context.JobOffers.Count();
        int totalPages = (int)Math.Ceiling((double)totalResults / resultsPerPage) - 1;
    
        var jobOffers = _context.JobOffers
            .Skip(resultsPerPage * page)    
            .Take(resultsPerPage)
            .Include(j => j.MultiLocation)
            .Include(j => j.EmploymentTypes)
            .ToList();
        if (page > totalPages)
        {
            return Redirect("" + totalPages);
    
        }
    
        return Ok(new { jobOffers, totalPages, page });
    }


    [HttpGet("/api/offer")]
    public IActionResult GetJobOfferById([FromQuery] int id)
    {
        var jobOffer = _context.JobOffers
            .Include(j => j.Reviews)
            .Include(j => j.MultiLocation)
            .Include(j => j.EmploymentTypes)
            .FirstOrDefault(j => j.Id == id);

        if (jobOffer == null)
        {
            return NotFound();
        }

        return Ok(jobOffer);
    }

}