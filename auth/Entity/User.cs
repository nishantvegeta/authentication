using System;
using auth.Constants;

namespace auth.Entity;

public class User
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string UserType { get; set; } = UserTypeConstant.Admin;
    public int Age { get; set; }
}
