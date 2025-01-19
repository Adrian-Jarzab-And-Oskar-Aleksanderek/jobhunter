using Microsoft.AspNetCore.Authorization;
using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewControler : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ReviewControler(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(string comment, int rating, int jobOfferId)
        {
            var review = new Review { Comment = comment, Rating = rating, JobOfferId = jobOfferId };
            // var userId = _userManager.GetUserId(User);
            // if (string.IsNullOrEmpty(userId))
            // {
            //     return Unauthorized("Nie jesteś zalogowany.");
            // }

            var jobOffer = await _context.JobOffers.FindAsync(jobOfferId);
            if (jobOffer == null)
            {
                return NotFound("Oferta pracy nie została znaleziona.");
            }

            review.UserId = "741d24fc-739f-4b1d-add1-c4ab006ee0c5";
            review.JobOfferId = jobOffer.Id;
            review.CreatedAt = DateTime.UtcNow;

            _context.Add(review);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Recenzja została dodana", review });
        }
        
        [HttpPost("edit")]
        public async Task<IActionResult> Edit(int reviewId, string comment, int rating)
        {
            var review = await _context.Reviews.FindAsync(reviewId);
            if (review == null)
            {
                return NotFound("Recenzja nie została znaleziona.");
            }
           
            // var userId = _userManager.GetUserId(User);
            // if (string.IsNullOrEmpty(userId) || review.UserId != userId)
            // {
            //     return Unauthorized("Nie masz uprawnień do edytowania tej recenzji.");
            // }

            if (!string.IsNullOrEmpty(comment))
            {
                review.Comment = comment;
            }

            if (rating >= 1 && rating <= 5)
            {
                review.Rating = rating;
            }

            _context.Reviews.Update(review);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Recenzja została zaktualizowana." });
        }

        
        [HttpPost("delete")]
        public async Task<IActionResult> Delete(int reviewId)
        {
            var review = await _context.Reviews.FindAsync(reviewId);
            if (review == null)
            {
                return NotFound("Recenzja nie została znaleziona.");
            }

          
            // var userId = _userManager.GetUserId(User);
            // if (string.IsNullOrEmpty(userId) || review.UserId != userId)  // Zakładając, że masz pole UserId w modelu Recenzja
            // {
            //     return Unauthorized("Nie masz uprawnień do usunięcia tej recenzji.");
            // }

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Recenzja została usunięta." });
        }

    }
}