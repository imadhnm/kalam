using System.ComponentModel.DataAnnotations;

public class UserViewModel
{
    public string Name { get; set; } = string.Empty;
    [Required(ErrorMessage = "*Email is required")]
    public string Email { get; set; } = string.Empty;
    [Required(ErrorMessage = "Username is required")]
    public string Username { get; set; } = string.Empty;
    [Required(ErrorMessage = "*Password is required")]
    public string Password { get; set; } = string.Empty;
    [Required(ErrorMessage = "*Field is required")]
    [Compare("Password", ErrorMessage = "*Password doesn't match")]
    public string ConfirmPassword { get; set; } = string.Empty;
    public string DefaultDomain { get; set; } = string.Empty;
    public string CustomDomain { get; set; } = string.Empty;
}