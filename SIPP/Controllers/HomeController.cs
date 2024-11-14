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
        private readonly SIPPDbContext _context;

        public HomeController(ILogger<HomeController> logger, SIPPDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
           
            var imoveis = await _context.Imoveis
                .Include(i => i.Imagens) 
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
