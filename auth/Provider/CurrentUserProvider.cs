using System;
using auth.Data;
using auth.Provider.Interfaces;
using auth.Entity;
using System.Security.Claims;

namespace auth.Provider;

public class CurrentUserProvider : ICurrentUserProvider
{
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly FirstRunDbContext dbContext;

    public CurrentUserProvider(IHttpContextAccessor contextAccessor, FirstRunDbContext dbContext)
    {
        _contextAccessor = contextAccessor;
        this.dbContext = dbContext;
    }

    public bool IsLoggedIn()
        => GetCurrentUserId() != null;    

    public async Task<User?> GetCurrentUser()
    {
        var currentuserId = GetCurrentUserId();
        if (currentuserId == null)
        {
            return null;
        }

        return await dbContext.Users.FindAsync(currentuserId.Value);
    }

    public async Task<User?> GetUserById(long id)
    {
        return await dbContext.Users.FindAsync(id);
    }

    public long? GetCurrentUserId()
    {
        var userId = _contextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null)
        {
            return null;
        }

        return long.Parse(userId);
    }
}
