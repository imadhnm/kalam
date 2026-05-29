
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
    private readonly KalamDbContext _kalamdb;
    private readonly ILogger<AuthService> _logger;

    public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
                                IPasswordHasher<ApplicationUser> passwordHasher, KalamDbContext kalamDb, ILogger<AuthService> logger)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _passwordHasher = passwordHasher;
        _kalamdb = kalamDb;
        _logger = logger;
    }

    public async Task<bool> ChangeEmail(string userId, string email)
    {
        try
        {
            var normalizedEmail = email.ToUpper();

            var emailExists = await _userManager.Users.AnyAsync(x => x.NormalizedEmail == normalizedEmail);

            if (emailExists)
            {
                return false;
            }

            var user = await _userManager.FindByIdAsync(userId);
            var setEmailRes = await _userManager.SetEmailAsync(user, email);

            if (setEmailRes.Succeeded)
            {
                var res = await _userManager.UpdateAsync(user);
                if (res.Succeeded)
                {
                    await _signInManager.RefreshSignInAsync(user);
                    return true;
                }
            }


            return false;
        }
        catch (System.Exception)
        {

            throw;
        }
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