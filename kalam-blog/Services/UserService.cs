
using Microsoft.AspNetCore.Identity;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly KalamDbContext _kalamdb;
    private readonly ILogger<AuthService> _logger;

    public UserService()
    {

    }

    public Task<bool> ChangeEmail()
    {
        throw new NotImplementedException();
    }

    public Task<bool> ChangePassword()
    {
        throw new NotImplementedException();
    }

    public Task<bool> ChangeUsername()
    {
        throw new NotImplementedException();
    }

    public async Task GetUserByEmailAsync(string name)
    {
        var user = _userManager.FindByNameAsync(name);
        throw new NotImplementedException();
    }

    public async Task GetUserByNameAsync(string email)
    {
        var user = _userManager.FindByNameAsync(email);

        throw new NotImplementedException();
    }

    public async Task GetUserByID(string id)
    {
        var user = await _userManager.FindByIdAsync(id);

        if (user == null)
        {
            // return Result.Failed(UserError.UserNotFound);
        }
    }
}