using Microsoft.AspNetCore.Mvc;
using auth.Data;
using auth.Manager.Interfaces;
using auth.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace auth.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {

        private readonly FirstRunDbContext dbContext;
        private readonly IAuthManager authManager;

        public AuthController(FirstRunDbContext dbContext, IAuthManager authManager)
        {
            this.dbContext = dbContext;
            this.authManager = authManager;
        }

        // GET: AuthController
        public ActionResult Login()
        {
            var vm = new LoginVm();
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVm Vm)
        {
            if(!ModelState.IsValid)
            {
                return View(Vm);
            }

            try
            {
                await authManager.Login(Vm.Username, Vm.Password);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                Vm.ErrorMessage = ex.Message;
                return View(Vm);
            }
        }

        public async Task<IActionResult> Logout()
        {
            await authManager.Logout();
            return RedirectToAction("Index", "Home");
        }
    }
}
