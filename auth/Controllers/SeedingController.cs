using Microsoft.AspNetCore.Mvc;
using auth.Data;
using auth.Provider.Interfaces;
using auth.ViewModels;
using Microsoft.AspNetCore.Authorization;
using auth.Entity;
using auth.Services;
using auth.Constants;

namespace auth.Controllers
{
    public class SeedingController : Controller
    {
        private readonly FirstRunDbContext dbContext;
        private readonly IUserService userService;

        public SeedingController(FirstRunDbContext dbContext, IUserService userService)
        {
            this.dbContext = dbContext;
            this.userService = userService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> SeedSuperAdmin()
        {
            try
            {
                var admin = new List<User>
                {
                    new User
                    {
                        Email = "super.admin",
                        Username = "Super admin",
                        Role = "Admin",
                        Age = 20,
                    },
                    new User
                    {
                        Email = "user1@example.com",
                        Username = "user 1",
                        Role = "Normal",
                        Age = 25,
                    },
                    new User
                    {
                        Email = "user2@example.com",
                        Username = "user 2",
                        Role = "Normal",
                        Age = 8,
                    },
                    new User
                    {
                        Email = "user3@example.com",
                        Username = "user3",
                        Role = "Normal",
                        Age = 30,
                    }
                };

                foreach (var user in admin)
                {
                    await userService.CreateUser(user, "admin");
                }

                return RedirectToAction("Login", "Auth");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException?.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
