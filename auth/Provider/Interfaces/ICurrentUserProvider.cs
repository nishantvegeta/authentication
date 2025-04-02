using System;
using auth.Entity;

namespace auth.Provider.Interfaces;

public interface ICurrentUserProvider
{
    Task<User?> GetCurrentUser();
    long? GetCurrentUserId();
    bool IsLoggedIn();
}
