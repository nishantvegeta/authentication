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
                var admin = new User()
                {
                    Email = "super.admin",
                    Username = "Super admin",
                    Role = "Admin",
                };

                await userService.CreateUser(admin , "admin");

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
