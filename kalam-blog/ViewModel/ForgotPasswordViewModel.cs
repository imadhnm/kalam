using System.ComponentModel.DataAnnotations;

public class ForgotPasswordViewModel
{
    [Required(ErrorMessage = "*Email is required")]
    public string Email { get; set; } = string.Empty;
}

public class ResetPasswordViewModel
{
    [Required(ErrorMessage = "*Password is required")]
    public string Password { get; set; } = string.Empty;
    [Required(ErrorMessage = "*Field is required")]
    [Compare("Password", ErrorMessage = "*Password doesn't match")]
    public string ConfirmPassword { get; set; } = string.Empty;
}