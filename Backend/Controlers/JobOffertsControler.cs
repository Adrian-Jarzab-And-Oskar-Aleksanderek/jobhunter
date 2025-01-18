using Backend.Data;
using Microsoft.AspNetCore.Mvc;
namespace Backend.Controlers;

[ApiController]
public class JobOffertsControler :ControllerBase
{
    private readonly ApplicationDbContext _context;

    public JobOffertsControler(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("/api/offers/page/{id}")]

    public IActionResult GetAllJobOffers([FromRoute] int id)
    {
        var jobOfferts = _context.JobOffers.Skip(30 * id).Take(30).ToList();

        return Ok(jobOfferts);
    }

    [HttpGet("/api/offer/{id}")]
    public IActionResult GetJobOfferById([FromRoute] int id)
    {
        var jobOffert = _context.JobOffers.Find(id);
        if (jobOffert == null)
        {
            return NotFound();
        }

        return Ok(jobOffert);
    }
}