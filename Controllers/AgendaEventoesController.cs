using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class AgendaEventoesController : Controller
    {
        private readonly AppDbContext _context;

        public AgendaEventoesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: AgendaEventoes
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.AgendaEventos.Include(a => a.UsuarioCliente).Include(a => a.UsuarioDentista);
            return View(await appDbContext.ToListAsync());
        }

        // GET: AgendaEventoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AgendaEventos == null)
            {
                return NotFound();
            }

            var agendaEvento = await _context.AgendaEventos
                .Include(a => a.UsuarioCliente)
                .Include(a => a.UsuarioDentista)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (agendaEvento == null)
            {
                return NotFound();
            }

            return View(agendaEvento);
        }

        // GET: AgendaEventoes/Create
        public IActionResult Create()
        {
            ViewData["UsuarioClienteId"] = new SelectList(_context.UsuariosClientes, "Id", "Email");
            ViewData["UsuarioDentistaId"] = new SelectList(_context.UsuariosDentistas, "Id", "Email");
            return View();
        }

        // POST: AgendaEventoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DataHora,Titulo,Descricao,UsuarioClienteId,UsuarioDentistaId")] AgendaEvento agendaEvento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(agendaEvento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            ViewData["UsuarioClienteId"] = new SelectList(_context.UsuariosClientes, "Id", "Email", agendaEvento.UsuarioClienteId);
            ViewData["UsuarioDentistaId"] = new SelectList(_context.UsuariosDentistas, "Id", "Email", agendaEvento.UsuarioDentistaId);
            return View(agendaEvento);
        }

        // GET: AgendaEventoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AgendaEventos == null)
            {
                return NotFound();
            }

            var agendaEvento = await _context.AgendaEventos.FindAsync(id);
            if (agendaEvento == null)
            {
                return NotFound();
            }
            ViewData["UsuarioClienteId"] = new SelectList(_context.UsuariosClientes, "Id", "Email", agendaEvento.UsuarioClienteId);
            ViewData["UsuarioDentistaId"] = new SelectList(_context.UsuariosDentistas, "Id", "Email", agendaEvento.UsuarioDentistaId);
            return View(agendaEvento);
        }

        // POST: AgendaEventoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DataHora,Titulo,Descricao,UsuarioClienteId,UsuarioDentistaId")] AgendaEvento agendaEvento)
        {
            if (id != agendaEvento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(agendaEvento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AgendaEventoExists(agendaEvento.Id))
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
            ViewData["UsuarioClienteId"] = new SelectList(_context.UsuariosClientes, "Id", "Email", agendaEvento.UsuarioClienteId);
            ViewData["UsuarioDentistaId"] = new SelectList(_context.UsuariosDentistas, "Id", "Email", agendaEvento.UsuarioDentistaId);
            return View(agendaEvento);
        }

        // GET: AgendaEventoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AgendaEventos == null)
            {
                return NotFound();
            }

            var agendaEvento = await _context.AgendaEventos
                .Include(a => a.UsuarioCliente)
                .Include(a => a.UsuarioDentista)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (agendaEvento == null)
            {
                return NotFound();
            }

            return View(agendaEvento);
        }

        // POST: AgendaEventoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AgendaEventos == null)
            {
                return Problem("Entity set 'AppDbContext.AgendaEventos'  is null.");
            }
            var agendaEvento = await _context.AgendaEventos.FindAsync(id);
            if (agendaEvento != null)
            {
                _context.AgendaEventos.Remove(agendaEvento);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AgendaEventoExists(int id)
        {
          return _context.AgendaEventos.Any(e => e.Id == id);
        }
    }
}
