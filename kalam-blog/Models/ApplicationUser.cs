using Microsoft.AspNetCore.Identity;

public class ApplicationUser : IdentityUser
{
    public string Name { get; set; } = string.Empty;
    public string DefaultDomain { get; set; } = string.Empty;
    public string CustomDomain { get; set; } = string.Empty;
    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }
    public DateTime DeletedOn { get; set; }
    public bool IsActive { get; set; }
    public bool SquatterFlag { get; set; }
    public bool MemberStatus { get; set; }
}

