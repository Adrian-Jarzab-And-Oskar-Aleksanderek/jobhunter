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

    [HttpGet("/api/offers")]

    public IActionResult GetAllJobOfferts()
    {
        var JobOfferts=_context.JobOffers.ToList();
        return Ok(JobOfferts);
    }

    [HttpGet("/api/offers/{id}")]
    public IActionResult GetJobOffertById([FromRoute] int id)
    {
        var jobOffert = _context.JobOffers.Find(id);
        if (jobOffert == null)
        {
            return NotFound();
        }

        return Ok(jobOffert);
    }
}