using System.ComponentModel.DataAnnotations;

public class ForgotPasswordViewModel
{
    [Required(ErrorMessage = "*Email is required")]
    public string Email { get; set; } = string.Empty;
}