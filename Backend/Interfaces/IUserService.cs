using Backend.DTO.Account;
using Backend.Models;
using Microsoft.AspNetCore.Identity;

namespace Backend.Interfaces;

public interface IUserService
{
    Task<bool> IsEmailTaken(string email);
    Task<bool> IsUserNameTaken(string userName);
    Task<bool> CheckPasswordAsync(User user, string password);
    Task<IdentityResult> ChangePasswordAsync(User user, string currentPassword, string newPassword);
    Task<IdentityResult> ChangeEmailAsync(User user, string newEmail);
}