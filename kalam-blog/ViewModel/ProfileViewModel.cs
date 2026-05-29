using System.ComponentModel.DataAnnotations;

public class ProfileViewModel
{
    public string ActiveTab { get; set; }
    public SettingsViewModel SettingsViewModel { get; set; }
}

public class SettingsViewModel
{
    public string Email { get; set; }
    public ChangePasswordViewModel? PasswordViewModel { get; set; }
}

public class ChangePasswordViewModel
{
    [Required(ErrorMessage = "*Password is required")]
    public string Password { get; set; } = string.Empty;
    [Required(ErrorMessage = "*Password is required")]
    public string NewPassword { get; set; } = string.Empty;
    [Required(ErrorMessage = "*Field is required")]
    [Compare("NewPassword", ErrorMessage = "*Password doesn't match")]
    public string ConfirmPassword { get; set; } = string.Empty;
}