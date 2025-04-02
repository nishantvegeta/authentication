using System.Security.Claims;
using auth.Data;
using auth.Entity;
using auth.Manager.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace auth.Manager;

public class AuthManager : IAuthManager
{
    private readonly FirstRunDbContext dbContext;
    private readonly IHttpContextAccessor httpContextAccessor;

    public AuthManager(FirstRunDbContext dbContext, IHttpContextAccessor httpContextAccessor)
    {
        this.dbContext = dbContext;
        this.httpContextAccessor = httpContextAccessor;
    }

    public async Task Login(string username, string password)
    {
        var user = dbContext.Users.FirstOrDefault(u => u.Username == username);
        if (user == null)
        {
            throw new Exception("User not found");
        }

        if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
        {
            throw new Exception("Invalid password");
        }

        var httpContext = httpContextAccessor.HttpContext;
        if (httpContext == null)
        {
            throw new Exception("HttpContext is null. Ensure this method is called within an HTTP request context.");
        }
        var claims = new List<Claim>
        {
            new ("Id", user.Id.ToString()),
        };
        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, 
            new ClaimsPrincipal(claimsIdentity));
    }

    private async Task<User?> GetUserByUsername(string username)
    {
        return await dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task Logout()
    {
        var httpContext = httpContextAccessor.HttpContext;
        if (httpContext == null)
        {
            throw new Exception("HttpContext is null. Ensure this method is called within an HTTP request context.");
        }
        await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }
}
