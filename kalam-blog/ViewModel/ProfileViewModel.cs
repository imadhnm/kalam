using System.ComponentModel.DataAnnotations;

public class ProfileViewModel
{
    public string ActiveTab { get; set; }
    public SettingsViewModel SettingsViewModel { get; set; }
    public List<ProfileBlog> ProfileBlogs { get; set; }
    public List<Bookmark> Bookmarks { get; set; }
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

public class ProfileBlog
{
    public Guid BlogID { get; set; }
    public string BlogName { get; set; }
}

public class Bookmark
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string URL { get; set; }
}