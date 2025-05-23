using System.ComponentModel.DataAnnotations;

namespace Backend.DTO.Account;

public class ChangeUsernameDto
{
    [Required]
    public string newUserName { get; set; }
}