using System;

namespace auth.ViewModels;

public class LoginVm
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string ErrorMessage;
}
