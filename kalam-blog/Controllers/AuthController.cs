using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Options;

namespace kalam_blog.Controllers;

public class AuthController : Controller
{
    private readonly IAuthService _authService;
    private readonly IOptions<PwdRecipe> _hashRecipe;

    public AuthController(IAuthService authService, IOptions<PwdRecipe> hashRecipe)
    {
        _authService = authService;
        _hashRecipe = hashRecipe;
    }

    public IActionResult Login()
    {
        if (User.Identity != null && User.Identity.IsAuthenticated == true)
        {
            return Redirect("/Home");
        }

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [EnableRateLimiting("user")]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState
                            .Where(x => x.Value != null && x.Value.Errors.Count > 0)
                            .Select(x => new
                            {
                                Property = x.Key,
                                Errors = x.Value?.Errors.Select(e => e.ErrorMessage).ToArray()
                            });

            foreach (var error in errors)
            {
                ModelState.AddModelError(error.Property, $"{error.Errors}");
            }

            return View(model);
        }
        var _password = PepperdPassword(model.Password);
        var user = new LoginDTO(model.Username, _password, model.RememberMe);

        var res = await _authService.Login(user);

        if (res)
        {
            return Redirect("/Home");
        }

        return View();
    }

    public async Task<IActionResult> Logout()
    {
        await _authService.Logout();

        return Redirect("/Home");
    }

    public IActionResult Register()
    {
        if (User.Identity != null && User.Identity.IsAuthenticated == true)
        {
            return Redirect("/Home");
        }

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [EnableRateLimiting("user")]
    public async Task<IActionResult> Register(UserViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState
                            .Where(x => x.Value != null && x.Value.Errors.Count > 0)
                            .Select(x => new
                            {
                                Property = x.Key,
                                Errors = x.Value?.Errors.Select(e => e.ErrorMessage).ToArray()
                            });

            foreach (var error in errors)
            {
                ModelState.AddModelError(error.Property, $"{error.Errors}");
            }

            return View(model);
        }

        var _password = PepperdPassword(model.Password);
        var userInfo = new UserDTO(model.Username, model.Email, _password);

        var res = await _authService.Register(userInfo);

        if (res.IsSuccess)
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            return Redirect("/Home");
        }

        return View();
    }

    public async Task<IActionResult> VerifyEmail()
    {
        return View();
    }

    public async Task<IActionResult> ForgotPassword()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var res = _authService.ForgotPassword(model.Email);

        return View();
    }

    public async Task<IActionResult> ResetPassword()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var _password = PepperdPassword(model.Password);

        var res = _authService.ResetPassword(new Guid(), _password);

        return View();
    }

    private string PepperdPassword(string password)
    {
        byte[] pepperBytes = Encoding.UTF8.GetBytes(_hashRecipe.Value.Secret);
        byte[] pwdBytes = Encoding.UTF8.GetBytes(password);

        using var hmac = new HMACSHA256(pepperBytes);
        return Convert.ToBase64String(hmac.ComputeHash(pwdBytes));
    }
}