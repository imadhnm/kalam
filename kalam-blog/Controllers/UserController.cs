using Microsoft.AspNetCore.Mvc;

namespace kalam_blog.Controllers;

public class UserController : Controller
{
    public UserController()
    {

    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(UserViewModel model)
    {
        return View();
    }

    public IActionResult Register()
    {
        return View();
    }

    // public async Task<IActionResult> Register()
    // {
    //     return View();
    // }
}