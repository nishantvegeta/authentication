using System;
using auth.Entity;

namespace auth.Services;

public interface IUserService
{
    Task CreateUser(User user, string password);
}
