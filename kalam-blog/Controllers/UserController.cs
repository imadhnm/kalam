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

    public IActionResult Register()
    {
        return View();
    }
}