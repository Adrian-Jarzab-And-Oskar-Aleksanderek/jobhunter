using System.Security.Claims;
using System.Text.Json;
using Backend.Data;
using Backend.Mappers;
using Backend.Models;
using Backend.Models.Review;
using Backend.Service.Caching;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Backend.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly JsonSerializerOptions _jsonOptions;

        public ReviewController(ApplicationDbContext context, UserManager<User> userManager, RedisService redisService, IOptions<JsonOptions> jsonOptions)
        {
            _userManager = userManager;
            _context = context;
            _jsonOptions = jsonOptions.Value.JsonSerializerOptions;
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] ReviewCreateRequest request)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var jobOffer = await _context.JobOffers.FindAsync(request.JobOfferId);
            if (jobOffer == null)
                return NotFound("Job offer not found.");
            

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized("User not authenticated");

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound("User not found");
            
            var review = new Review
            {
                Comment = request.Comment, 
                Rating = request.Rating, 
                JobOfferId = request.JobOfferId,
                UserId = user.Id,
                CreatedAt = DateTime.UtcNow
            };
            
            _context.Add(review);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Review added", review });

        }
        
        [HttpPost("edit")]
        [Authorize]
        public async Task<IActionResult> Edit([FromBody] ReviewEditRequest request )
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var review = await _context.Reviews.FindAsync(request.ReviewId);
            if (review == null)
                return NotFound("Review not found.");
            
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized("User not authenticated");
            
            if (review.UserId != userId)
            {
                return Unauthorized("You can't edit this review.");
            }

            if (string.IsNullOrEmpty(request.Comment))
                return BadRequest("Review need to have a comment.");
            
            if (request.Rating is < 1 or > 5)
                return BadRequest("Review need to have a rating between 1 and 5.");

            review.Comment = request.Comment;
            review.Rating = request.Rating;
            _context.Reviews.Update(review);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Review edited." });
        }

        
        [HttpPost("delete")]
        [Authorize]
        public async Task<IActionResult> Delete([FromBody] ReviewDeleteRequest request)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var review = await _context.Reviews.FindAsync(request.ReviewId);
            if (review == null)
                return NotFound("Review not found.");
            
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized("User not authenticated");
            
            if (review.UserId != userId)
            {
                return Unauthorized("You can't delete this review.");
            }
            
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Review deleted." });
        }
        
        [HttpGet("get")]
        public async Task<IActionResult> GetAllReviewsForOffer([FromQuery] int offer)
        {
            var reviews = _context.Reviews
                .Include(r => r.User)
                .Where(r => r.JobOfferId == offer)
                .ToList()
                .Select(review => review.MapToReviewDto())
                .ToList();

            var response = new { reviews };
            
            return Ok(response);
        }
    }
}