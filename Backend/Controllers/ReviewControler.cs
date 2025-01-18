using Microsoft.AspNetCore.Authorization;
using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    // [ApiController]
    // [Route("api/[controller]")]
    public class ReviewControler : ControllerBase
    {
        // private readonly ApplicationDbContext _context;
        // private readonly UserManager<IdentityUser> _userManager;
        //
        // public ReviewControler(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        // {
        //     _context = context;
        //     _userManager = userManager;
        // }
        //
        // [HttpPost("create")]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Create([Bind("Comment, Rating")] Review review, int jobOfferId)
        // {
        //     // var userId = _userManager.GetUserId(User);
        //     // if (string.IsNullOrEmpty(userId))
        //     // {
        //     //     return RedirectToAction("Login", "Account");
        //     // }
        //
        //     var jobOffer = await _context.JobOffers.FindAsync(jobOfferId);
        //     if (jobOffer == null)
        //     {
        //         return NotFound("Oferta pracy nie została znaleziona.");
        //     }
        //
        //     review.UserId = "741d24fc-739f-4b1d-add1-c4ab006ee0c5";
        //     review.JobOfferId = jobOffer.Id;
        //     review.CreatedAt = DateTime.Now;
        //
        //     _context.Add(review);
        //     await _context.SaveChangesAsync();
        //
        //     return Ok(new { message = "Recenzja została dodana", review });
        // }
    }
}