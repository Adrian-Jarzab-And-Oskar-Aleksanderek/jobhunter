using Microsoft.AspNetCore.Identity;
using Backend.Models.Review;
namespace Backend.Models;

public class User : IdentityUser
{
    public ICollection<Review.Review> Reviews { get; set; } = new List<Review.Review>();
    
}