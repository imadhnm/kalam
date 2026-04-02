public static class UserError
{
    public static readonly Error UsernameAlreadyExists = new Error("UsernameAlreadyExists", "Username already exists!");
    public static readonly Error EmailAlreadyExists = new Error("EmailAlreadyExists", "An Account with this Email already exists!");
    public static readonly Error UserCreationFailed = new Error("UserCreationFailed", "An Error occured! Please try again.");
}