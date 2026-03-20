
using Microsoft.AspNetCore.Identity;

public class RegisterUser : IRegisterUser
{
    public record Request(string Email, string Password);

    public async Task Register(IRegisterUser.Request request, UserManager<IdentityUser> userManager)
    {
        var user = new IdentityUser
        {
            Email = request.Email,
            UserName = request.Email
        };

        var result = await userManager.CreateAsync(user, request.Password);

        // if (!result.Succeeded)
        // {
        //     return Results.BadRequest(result.Errors);
        // }
    }
}