using Microsoft.AspNetCore.Identity;

public interface IRegisterUser
{
    public record Request(string Email, string Password);
    Task Register(Request request, UserManager<IdentityUser> user);
}