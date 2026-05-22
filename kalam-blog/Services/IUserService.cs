public interface IUserService
{
    Task GetUserByNameAsync(string name);
    Task GetUserByEmailAsync(string email);
    Task GetUserByID(string id);
    Task<bool> ChangePassword();
    Task<bool> ChangeEmail();
    Task<bool> ChangeUsername();
}