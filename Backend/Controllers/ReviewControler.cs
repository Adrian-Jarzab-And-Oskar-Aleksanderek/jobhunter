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
        public async Task<IActionResult> Create([FromBody] ReviewCreateRequest request)
        {
            var review = new Review { Comment = request.Comment, Rating = request.Rating, JobOfferId = request.JobOfferId };
            // var userId = _userManager.GetUserId(User);
            // if (string.IsNullOrEmpty(userId))
            // {
            //     return Unauthorized("Nie jesteś zalogowany.");
            // }

                var jobOffer = await _context.JobOffers.FindAsync(request.JobOfferId);
                if (jobOffer == null)
                {
                    return NotFound("Job offer not found.");
                }

                review.UserId = "741d24fc-739f-4b1d-add1-c4ab006ee0c5";
                review.JobOfferId = jobOffer.Id;
                review.CreatedAt = DateTime.UtcNow;

                _context.Add(review);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Review added", review });

        }
        
        [HttpPost("edit")]
        public async Task<IActionResult> Edit([FromBody] ReviewEditRequest request )
        {
            if (request  == null)
            {
                return NotFound("Review not found.");
            }
           
            var review = await _context.Reviews.FindAsync(request.ReviewId);
            // var userId = _userManager.GetUserId(User);
            // if (string.IsNullOrEmpty(userId) || review.UserId != userId)
            // {
            //     return Unauthorized("Nie masz uprawnień do edytowania tej recenzji.");
            // }

            if (!string.IsNullOrEmpty(request.Comment))
            {
                return NotFound("Review need to have a comment.");
            }
            if (request.Rating is < 1 or > 5)
            {
                return NotFound("Review need to have a rating between 1 and 5.");
            
            }
            review.Comment = request.Comment;
            review.Rating = request.Rating;
            
            _context.Reviews.Update(review);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Review edited." });
        }

        
        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromBody] ReviewDeleteRequest request)
        {
            var review = await _context.Reviews.FindAsync(request.ReviewId);
            if (review == null)
            {
                return NotFound("Review not found.");
            }

          
            // var userId = _userManager.GetUserId(User);
            // if (string.IsNullOrEmpty(userId) || review.UserId != userId)  // Zakładając, że masz pole UserId w modelu Recenzja
            // {
            //     return Unauthorized("Nie masz uprawnień do usunięcia tej recenzji.");
            // }

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Review deleted." });
        }

    }
}