using Microsoft.AspNetCore.Identity;
namespace Backend.Models;

public class User : IdentityUser
{
    public ICollection<Review.Review> Reviews { get; set; } = new List<Review.Review>();

}
