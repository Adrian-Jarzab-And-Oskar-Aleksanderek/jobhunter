using System.IdentityModel.Tokens.Jwt;
using Backend.Data;
using Backend.Models;
using Backend.Models.Review;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ReviewControler : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;    
        public ReviewControler(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
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
            
            var authorizationHeader = Request.Headers["Authorization"].ToString();
            var jwtHandler = new JwtSecurityTokenHandler();
            var token = authorizationHeader.Substring("Bearer ".Length).Trim();
            var jwtToken = jwtHandler.ReadJwtToken(token);
            var userName = jwtToken.Payload["given_name"]?.ToString();
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
            
            if (user == null)
            {
                return Unauthorized("Please login to create review.");
            }
            
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
            
            var authorizationHeader = Request.Headers["Authorization"].ToString();
            var jwtHandler = new JwtSecurityTokenHandler();
            var token = authorizationHeader.Substring("Bearer ".Length).Trim();
            var jwtToken = jwtHandler.ReadJwtToken(token);
            var userName = jwtToken.Payload["given_name"]?.ToString();
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
            
            if (user == null)
            {
                return Unauthorized("Please login to edit review.");
            }
            
            if (review.UserId != user.Id)
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
            
            var authorizationHeader = Request.Headers["Authorization"].ToString();
            var jwtHandler = new JwtSecurityTokenHandler();
            var token = authorizationHeader.Substring("Bearer ".Length).Trim();
            var jwtToken = jwtHandler.ReadJwtToken(token);
            var userName = jwtToken.Payload["given_name"]?.ToString();
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
            
            if (user == null)
            {
                return Unauthorized("Please login to delete review.");
            }
            
            
            if (review.UserId != user.Id)
            {
                return Unauthorized("You can't delete this review.");
            }
            
            
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Review deleted." });
        }

    }
}