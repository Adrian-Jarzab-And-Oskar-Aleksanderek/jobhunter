
namespace Backend.Models;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

public class Review
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Comment is required.")]
    public string Comment { get; set; }

    [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
    public int Rating { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [ForeignKey("JobOffer")]
    public int JobOfferId { get; set; }

    public JobOffer? JobOffer { get; set; }
    
    [ForeignKey("User")]
    public string? UserId { get; set; }

    public IdentityUser? User { get; set; }
}