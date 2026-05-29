using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace kalam_blog.Controllers;

public class UserController : Controller
{
    private readonly IAuthService _authService;
    private readonly IUserService _userService;
    private readonly IOptions<PwdRecipe> _hashRecipe;

    public UserController(IUserService userService, IAuthService authService, IOptions<PwdRecipe> hashRecipe)
    {
        _userService = userService;
        _authService = authService;
        _hashRecipe = hashRecipe;
    }

    // move to a different controller
    public IActionResult Dashboard()
    {
        ViewData["Dashboard_Active"] = "active";
        return View();
    }

    public IActionResult Profile()
    {
        // if (User.Identity == null || !User.Identity.IsAuthenticated)
        // {
        //     return Redirect("/Home");
        // }

        var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

        return View(new ProfileViewModel());
    }

    public IActionResult Settings()
    {
        ViewData["Settings_Active"] = "active";
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ChangeEmail(SettingsViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View("Settings", model);
        }

        var email = model.Email;

        var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

        await _userService.ChangeEmail(userId, email);
        var em = User.FindFirstValue(ClaimTypes.Email);

        return Redirect("/Settings");
    }

    [HttpPost]
    public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
    {

        if (!ModelState.IsValid)
        {
            return View("Settings", model);
        }

        var _currentPassword = PepperdPassword(model.Password);
        var _newPassword = PepperdPassword(model.NewPassword);

        var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

        var res = await _authService.ResetPassword(userId, _currentPassword, _newPassword);

        return View("Settings", model);
    }

    private string PepperdPassword(string password)
    {
        byte[] pepperBytes = Encoding.UTF8.GetBytes(_hashRecipe.Value.Secret);
        byte[] pwdBytes = Encoding.UTF8.GetBytes(password);

        using var hmac = new HMACSHA256(pepperBytes);
        return Convert.ToBase64String(hmac.ComputeHash(pwdBytes));
    }
}