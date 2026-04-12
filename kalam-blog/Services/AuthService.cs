
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
    private readonly KalamDbContext _kalamdb;

    public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
                                IPasswordHasher<ApplicationUser> passwordHasher, KalamDbContext kalamDb)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _passwordHasher = passwordHasher;
        _kalamdb = kalamDb;
    }

    public async Task<Result> Register(UserDTO userInfo)
    {
        if (await CheckUsernameAvailability(userInfo.Username))
        {
            return Result.Failed(UserError.UsernameAlreadyExists);
        }

        if (await CheckEmailAlreadyExist(userInfo.Email))
        {
            return Result.Failed(UserError.EmailAlreadyExists);
        }

        try
        {
            var user = new ApplicationUser
            {
                Email = userInfo.Email,
                UserName = userInfo.Username
            };

            var createUser = await _userManager.CreateAsync(user, userInfo.Password);

            if (!createUser.Succeeded)
            {
                return Result.Failed(UserError.UserCreationFailed);
            }

            await _signInManager.SignInAsync(user, isPersistent: true);

            return Result.Succeeded();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<bool> Login(LoginDTO userInfo)
    {
        try
        {
            var _user = await _userManager.FindByNameAsync(userInfo.Username);

            if (_user == null)
            {
                return false;
            }

            //this failsif the user's email is not confirmed thanks to "config.SignIn.RequireConfirmedEmail = true;" 
            //in "Program.cs". Have commented it for now. Uncomment it after implementing "Verify Email" feature. 
            //My head hurts trying to figure out why this kept failing
            var _signIn = await _signInManager.PasswordSignInAsync(_user, userInfo.Password, userInfo.IsPersist, false);

            if (!_signIn.Succeeded)
            {
                return false;
            }

            return true;
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    public async Task Logout()
    {
        await _signInManager.SignOutAsync();
    }

    private async Task<bool> CheckUsernameAvailability(string username)
    {
        try
        {
            var normalizedUsername = username.ToUpper();

            return await _userManager.Users.AnyAsync(x => x.NormalizedUserName == normalizedUsername);
        }
        catch (Exception)
        {

            throw;
        }
    }

    private async Task<bool> CheckEmailAlreadyExist(string email)
    {
        try
        {
            var normalizedEmail = email.ToUpper();

            return await _userManager.Users.AnyAsync(x => x.NormalizedEmail == normalizedEmail);
        }
        catch (System.Exception)
        {

            throw;
        }
    }

    public async Task<bool> ResetPassword(Guid UserId, string newPassword)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ForgotPassword(string email)
    {
        var emailExists = await CheckEmailAlreadyExist(email);

        if (!emailExists)
        {

        }

        throw new NotImplementedException();
    }
}