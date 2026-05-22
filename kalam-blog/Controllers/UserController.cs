using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace kalam_blog.Controllers;

public class UserController : Controller
{
    public UserController()
    {

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
    public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
    {
        SettingsViewModel viewModel = new SettingsViewModel() { PasswordViewModel = model };

        if (!ModelState.IsValid)
        {
            return PartialView("_Settings", viewModel);
        }

        // // var _password = PepperdPassword(model.Password);

        // // var res = _authService.ResetPassword(new Guid(), _password);

        // return View(viewModel);
        return PartialView("_Settings", viewModel);
    }
}