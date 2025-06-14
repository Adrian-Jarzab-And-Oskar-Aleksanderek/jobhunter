using Backend.Interfaces;
using Backend.Models;
using Microsoft.AspNetCore.Identity;

namespace Backend.Service;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    public UserService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<bool> IsEmailTaken(string email)
    {
        return await _userManager.FindByEmailAsync(email) != null;
    }

    public async Task<bool> IsUserNameTaken(string userName)
    {
        return await _userManager.FindByNameAsync(userName) != null;
    }

    public async Task<bool> CheckPasswordAsync(User user, string password)
    {
        return await _userManager.CheckPasswordAsync(user, password);
    }

    public async Task<IdentityResult> ChangePasswordAsync(User user, string currentPassword, string newPassword)
    {
        return await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
    }

    public async Task<IdentityResult> ChangeEmailAsync(User user, string newEmail)
    {
        var token = await _userManager.GenerateChangeEmailTokenAsync(user, newEmail);
        return await _userManager.ChangeEmailAsync(user, newEmail, token);
    }
}
