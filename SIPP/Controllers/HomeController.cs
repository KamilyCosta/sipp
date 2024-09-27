using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Vendas()
        {
            return View();
        }

        [Authorize(Roles = "Corretores, Admin")]
        public IActionResult Agendamento()
        {
            return View();
        }
        
        public IActionResult Locacao()
        {
            return View();
        }

        public IActionResult Corretores()
        {
            return View();
        }

        public IActionResult FaleConosco()
        {
            return View();
        }

        public IActionResult Casa1()
        {
            return View();
        }

        [Authorize(Roles = "Cliente, Admin")]
        public IActionResult formulariocasa1()
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
