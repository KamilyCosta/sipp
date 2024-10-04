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
    public class CorretorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CorretorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Corretors
        public async Task<IActionResult> Index()
        {
            return View(await _context.Corretores.ToListAsync());
        }

        // GET: Corretors/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var corretor = await _context.Corretores
                .FirstOrDefaultAsync(m => m.CorretorId == id);
            if (corretor == null)
            {
                return NotFound();
            }

            return View(corretor);
        }

        // GET: Corretors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Corretors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CorretorId,Nome,TemDeTrabalho,CRECI,Telefone")] Corretor corretor)
        {
            if (ModelState.IsValid)
            {
                corretor.CorretorId = Guid.NewGuid();
                _context.Add(corretor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(corretor);
        }

        // GET: Corretors/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var corretor = await _context.Corretores.FindAsync(id);
            if (corretor == null)
            {
                return NotFound();
            }
            return View(corretor);
        }

        // POST: Corretors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CorretorId,Nome,TemDeTrabalho,CRECI,Telefone")] Corretor corretor)
        {
            if (id != corretor.CorretorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(corretor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CorretorExists(corretor.CorretorId))
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
            return View(corretor);
        }

        // GET: Corretors/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var corretor = await _context.Corretores
                .FirstOrDefaultAsync(m => m.CorretorId == id);
            if (corretor == null)
            {
                return NotFound();
            }

            return View(corretor);
        }

        // POST: Corretors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var corretor = await _context.Corretores.FindAsync(id);
            if (corretor != null)
            {
                _context.Corretores.Remove(corretor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CorretorExists(Guid id)
        {
            return _context.Corretores.Any(e => e.CorretorId == id);
        }
    }
}
