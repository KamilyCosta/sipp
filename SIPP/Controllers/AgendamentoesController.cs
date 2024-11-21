using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SIPP.Data;
using SIPP.Models;

namespace SIPP.Controllers
{
    public class AgendamentoesController : Controller
    {
        private readonly SIPPDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public AgendamentoesController(SIPPDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }



        // GET: Agendamentoes
        public async Task<IActionResult> Index()
        {
            // Obtém o ID do usuário logado
            var userId = _userManager.GetUserId(User);

            // Verifica se o usuário está logado
            if (userId == null)
            {
                // Se não estiver logado, redireciona para a página de login
                return Redirect("https://localhost:7061/Identity/Account/Login");
            }

            // Obtém a pessoa associada ao usuário logado (se existir)
            var pessoa = await _context.Pessoa.FirstOrDefaultAsync(p => p.UserId == userId);

            // Verifique o TipoPessoaId para determinar se é Admin ou Corretor
            Guid tipoAdminId = new Guid("799237FF-605B-4614-BBA8-6DA107B3FFE4");
            Guid tipoCorretorId = new Guid("A83D62DD-7112-4B7A-9CB0-134AD4ACF74C");

            bool isAdmin = false;
            bool isCorretor = false;

            if (pessoa != null)
            {
                isAdmin = pessoa.TipoPessoaId == tipoAdminId;
                isCorretor = pessoa.TipoPessoaId == tipoCorretorId;
            }
            else
            {
                // Verifique se o usuário logado é admin ou corretor diretamente com o tipo do usuário
                var user = await _userManager.GetUserAsync(User);
                isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
                isCorretor = await _userManager.IsInRoleAsync(user, "Corretor");
            }

            // Consulta todos os agendamentos
            IQueryable<Agendamento> agendamentosQuery = _context.Agendamento
                .Include(a => a.Cliente)
                .Include(a => a.Corretor)
                .Include(a => a.Imovel);

            // Se o usuário for admin ou corretor, exibe todos os agendamentos
            if (isAdmin || isCorretor)
            {
                return View(await agendamentosQuery.ToListAsync());
            }
            else
            {
                if (pessoa == null)
                {
                    ViewBag.Message = "Você não tem um agendamento registrado. Realize um agendamento em nossos imóveis!";
                    return View(new List<Agendamento>());
                }

                agendamentosQuery = agendamentosQuery.Where(a => a.ClienteId == pessoa.PessoaId);
                var agendamentos = await agendamentosQuery.ToListAsync();

                if (!agendamentos.Any())
                {
                    ViewBag.Message = "Você ainda não tem agendamentos. Realize um agendamento em nossos imóveis!";
                }

                return View(agendamentos);
            }
        }









        // GET: Agendamentoes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agendamento = await _context.Agendamento
                .Include(a => a.Cliente)
                .Include(a => a.Corretor)
                .FirstOrDefaultAsync(m => m.AgendamentoId == id);
            if (agendamento == null)
            {
                return NotFound();
            }

            return View(agendamento);
        }


        public IActionResult Create(Guid imovelId)
        {
            
            Guid tipoCorretorId = new Guid("A83D62DD-7112-4B7A-9CB0-134AD4ACF74C");

           
            var corretores = _context.Pessoa
                .Where(p => p.TipoPessoaId == tipoCorretorId)  
                .Select(p => new SelectListItem
                {
                    Value = p.PessoaId.ToString(),
                    Text = p.Nome
                })
                .ToList();  

            
            ViewData["CorretorId"] = new SelectList(corretores, "Value", "Text");

            var userId = _userManager.GetUserId(User);

            
            ViewData["ClienteId"] = new SelectList(
                new List<SelectListItem> {
            new SelectListItem { Value = userId, Text = "Você (Cliente)" }
                },
                "Value", "Text", userId);

            var imovel = _context.Imoveis.FirstOrDefault(i => i.ImovelId == imovelId);
            ViewData["ImovelId"] = imovelId;

            return View();
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AgendamentoId,DataAge,HoraAge,ClienteId,CorretorId,ImovelId")] Agendamento agendamento)
        {
            
            ModelState.Remove("Cliente");
            ModelState.Remove("ClienteId");
            ModelState.Remove("Corretor");
            

            if (ModelState.IsValid)
            {
                
                agendamento.AgendamentoId = Guid.NewGuid();

                
                var userId = _userManager.GetUserId(User);

               
                var pessoa = await _context.Pessoa.FirstOrDefaultAsync(p => p.UserId == userId);


                
                agendamento.ClienteId = pessoa.PessoaId;

                
                var corretor = await _context.Pessoa.FirstOrDefaultAsync(p => p.PessoaId == agendamento.CorretorId);

                
                agendamento.Corretor = corretor;

                
                var imovel = await _context.Imoveis.FirstOrDefaultAsync(i => i.ImovelId == agendamento.ImovelId);

                agendamento.Imovel = imovel;

                _context.Add(agendamento);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(agendamento);
        }


        
        


        // GET: Agendamentoes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agendamento = await _context.Agendamento
                .Include(a => a.Cliente)
                .Include(a => a.Corretor)
                .FirstOrDefaultAsync(m => m.AgendamentoId == id);
            if (agendamento == null)
            {
                return NotFound();
            }

            return View(agendamento);
        }

        // POST: Agendamentoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var agendamento = await _context.Agendamento.FindAsync(id);
            if (agendamento != null)
            {
                _context.Agendamento.Remove(agendamento);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AgendamentoExists(Guid id)
        {
            return _context.Agendamento.Any(e => e.AgendamentoId == id);
        }
    }
}
