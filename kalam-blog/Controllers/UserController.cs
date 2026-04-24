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
        return View();
    }

    public IActionResult Profile()
    {
        // if (User.Identity == null || !User.Identity.IsAuthenticated)
        // {
        //     return Redirect("/Home");
        // }

        return View();
    }

    public IActionResult Settings()
    {
        return View();
    }
}