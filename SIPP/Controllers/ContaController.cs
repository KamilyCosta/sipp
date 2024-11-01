using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SIPP.Models;
using System.Net;

namespace SIPP.Controllers
{
    public class ContaController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public ContaController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Senha);

                if (result.Succeeded)
                {
                    // Usuário criado com sucesso
                    // return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Ok(result);
            }
            
            return Ok("Erro");
        }
    }
}
