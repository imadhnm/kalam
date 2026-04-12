using Microsoft.AspNetCore.Identity;

public interface IAuthService
{
    Task<Result> Register(UserDTO user);
    Task<bool> Login(LoginDTO user);
    Task Logout();
    Task<bool> ResetPassword(Guid UserId, string newPassword);
    Task<bool> ForgotPassword(string email);
}