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
            var sIPPDbContext = _context.Agendamento.Include(a => a.Cliente).Include(a => a.Corretor);
            return View(await sIPPDbContext.ToListAsync());
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
                new List<SelectListItem> { new SelectListItem { Value = userId, Text = "Você (Cliente)" } },
                "Value", "Text", userId);

            // Passa o ImovelId para a view
            ViewData["ImovelId"] = imovelId;

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AgendamentoId,DataAge,HoraAge,ClienteId,CorretorId")] Agendamento agendamento)
        {
            
            ModelState.Remove("Cliente");
            ModelState.Remove("ClienteId");
            ModelState.Remove("Corretor");
            
            if (ModelState.IsValid)
            {
                agendamento.AgendamentoId = Guid.NewGuid();

                // Pega o Id do AspNetUsers
                var userId = _userManager.GetUserId(User);

                // Buscar o cliente (logado) pelo Id recuperado acima
                var pessoa = await _context.Pessoa.FirstOrDefaultAsync(p => p.UserId == userId);

                agendamento.ClienteId = pessoa.PessoaId;

                _context.Add(agendamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

           


            return View(agendamento);
        }



        // GET: Agendamentoes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agendamento = await _context.Agendamento.FindAsync(id);
            if (agendamento == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Pessoa, "PessoaId", "PessoaId", agendamento.ClienteId);
            ViewData["CorretorId"] = new SelectList(_context.Pessoa, "PessoaId", "PessoaId", agendamento.CorretorId);
            return View(agendamento);
        }

        // POST: Agendamentoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AgendamentoId,DataAge,HoraAge,ClienteId,CorretorId")] Agendamento agendamento)
        {
            if (id != agendamento.AgendamentoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(agendamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AgendamentoExists(agendamento.AgendamentoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.Pessoa, "PessoaId", "PessoaId", agendamento.ClienteId);
            ViewData["CorretorId"] = new SelectList(_context.Pessoa, "PessoaId", "PessoaId", agendamento.CorretorId);
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
