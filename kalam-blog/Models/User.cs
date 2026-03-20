using Microsoft.AspNetCore.Identity;

public class ApplicationUser : IdentityUser
{
    public string Name { get; set; } = string.Empty;
    public string blog_default { get; set; }
    public string blog_custom { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }
    public DateTime DeletedOn { get; set; }
    public bool IsActive { get; set; }
    public bool squatter_flag { get; set; }
    public bool member_status { get; set; }
}