namespace PostgresCrud.Domain.User;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Role { get; set; } = UserRoles.User;
}

public static class UserRoles
{
    public const string Admin = "Admin";
    public const string User = "User";
}