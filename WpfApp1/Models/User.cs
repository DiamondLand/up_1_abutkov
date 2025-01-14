public enum UserRole
{
    Student,
    Teacher
}

public class User
{
    public string Username { get; set; }
    public string Password { get; set; }
    public UserRole Role { get; set; }
}
