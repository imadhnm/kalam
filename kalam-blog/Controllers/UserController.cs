using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;

namespace kalam_blog.Controllers;

public class UserController : Controller
{
    private readonly IKalamUserService _userService;
    private readonly IOptions<PwdRecipe> _hashRecipe;

    public UserController(IKalamUserService userService, IOptions<PwdRecipe> hashRecipe)
    {
        _userService = userService;
        _hashRecipe = hashRecipe;
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
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
        var user = new UserDTO(model.Username, string.Empty, _password);

        var res = await _userService.Login(user);

        if (res)
        {
            return RedirectToAction("Index", "Home");
        }

        return View();
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
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

        var res = await _userService.Register(userInfo);

        if (res.IsSuccess)
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            return RedirectToAction("Index", "Home");
        }

        return View();
    }

    public async Task<IActionResult> VerifyEmail()
    {
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