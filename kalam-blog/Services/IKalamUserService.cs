using Microsoft.AspNetCore.Identity;

public interface IKalamUserService
{
    Task<Result> Register(UserDTO user);
    Task<bool> Login(UserDTO user);
}