using System;
using auth.Entity;
using BCrypt.Net;
using auth.Data;

namespace auth.Services;

public class UserService : IUserService
{
    private readonly FirstRunDbContext dbcontext;

    public UserService(FirstRunDbContext dbcontext)
    {
        this.dbcontext = dbcontext;
    }

    public async Task CreateUser(User user, string password)
    {
        user.Password = BCrypt.Net.BCrypt.HashPassword(password);
        await dbcontext.Users.AddAsync(user);
        await dbcontext.SaveChangesAsync();
    }
}
