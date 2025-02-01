using Backend.DTO;
using Backend.Models;
using Backend.Models.Review;

namespace Backend.Mappers;

public static class ReviewMapper
{
    public static ReviewDto MapToReviewDto(this Review review)
    {
        return new ReviewDto
        {
            Id = review.Id,
            
            Comment = review.Comment,
            
            Rating = review.Rating,
            
            CreatedAt = review.CreatedAt,
            
            User = review.User
        };
    }
}