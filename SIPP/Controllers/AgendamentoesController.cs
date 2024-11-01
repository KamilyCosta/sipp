using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public AgendamentoesController(SIPPDbContext context)
        {
            _context = context;
        }

        // GET: Agendamentoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Agendamento.ToListAsync());
        }

        // GET: Agendamentoes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agendamento = await _context.Agendamento
                .FirstOrDefaultAsync(m => m.AgendamentoId == id);
            if (agendamento == null)
            {
                return NotFound();
            }

            return View(agendamento);
        }

        // GET: Agendamentoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Agendamentoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AgendamentoId,ClienteId,CorretorId,DataAge,HoraAge")] Agendamento agendamento)
        {
            if (ModelState.IsValid)
            {
                agendamento.AgendamentoId = Guid.NewGuid();
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
            return View(agendamento);
        }

        // POST: Agendamentoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AgendamentoId,ClienteId,CorretorId,DataAge,HoraAge")] Agendamento agendamento)
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
