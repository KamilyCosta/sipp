using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIPP.Data;
using Microsoft.EntityFrameworkCore;
using SIPP.Models;
using System.Diagnostics;

namespace SIPP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index([FromServices] SIPPDbContext context) 
        {
            var imoveis = await context.Imoveis
                .OrderBy(i => i.ImovelId)
                .Take(3)
                .ToListAsync();

            return View(imoveis);
        }

        public IActionResult FaleConosco()
        {
            return View();
        }

        public IActionResult Casa1()
        {
            return View();
        }


       




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
