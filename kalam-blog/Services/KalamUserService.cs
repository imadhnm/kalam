
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class KalamUserService : IKalamUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
    private readonly KalamDbContext _kalamdb;

    public KalamUserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
                                IPasswordHasher<ApplicationUser> passwordHasher, KalamDbContext kalamDb)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _passwordHasher = passwordHasher;
        _kalamdb = kalamDb;
    }

    public async Task<Result> Register(UserDTO userInfo)
    {
        if (!await CheckUsernameAvailability(userInfo.Username))
        {
            return Result.Failed(UserError.UsernameAlreadyExists);
        }

        if (!await CheckEmailAlreadyExist(userInfo.Email))
        {
            return Result.Failed(UserError.EmailAlreadyExists);
        }

        try
        {
            // var hashedPassword = _passwordHasher.HashPassword(null, userInfo.Password);

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

            return Result.Succeeded();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<bool> Login(UserDTO userInfo)
    {
        try
        {
            var _username = userInfo.Username.ToUpper();

            var _user = await _userManager.Users.Where(x => x.NormalizedUserName == _username).FirstOrDefaultAsync();

            if (_user == null)
            {
                return false;
            }

            //this failsif the user's email is not confirmed thanks to "config.SignIn.RequireConfirmedEmail = true;" 
            //in "Program.cs". Have commented it for now. Uncomment it after implementing "Verify Email" feature. 
            //My head hurts trying to figure out why this kept failing
            var _signIn = await _signInManager.PasswordSignInAsync(_user, userInfo.Password, false, false);

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

    private async Task<bool> CheckUsernameAvailability(string username)
    {
        try
        {
            var _user = username.ToUpper();

            var res = _kalamdb.Users.Where(x => x.NormalizedUserName == _user).Select(o => o.UserName);

            if (res != null)
            {
                return true;
            }

            return false;
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
            var _user = email.ToUpper();

            var res = _kalamdb.Users.Where(x => x.NormalizedEmail == _user).Select(o => o.Email);

            if (res != null)
            {
                return true;
            }

            return false;
        }
        catch (System.Exception)
        {

            throw;
        }
    }
}