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
    public class UsuarioDentistasController : Controller
    {
        private readonly AppDbContext _context;

        public UsuarioDentistasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: UsuarioDentistas
        public async Task<IActionResult> Index()
        {
              return View(await _context.UsuariosDentistas.ToListAsync());
        }

        // GET: UsuarioDentistas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UsuariosDentistas == null)
            {
                return NotFound();
            }

            var usuarioDentista = await _context.UsuariosDentistas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuarioDentista == null)
            {
                return NotFound();
            }

            return View(usuarioDentista);
        }

        // GET: UsuarioDentistas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UsuarioDentistas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Telefone,Email,CRO,Nome_da_clinica,Endereço,Senha")] UsuarioDentista usuarioDentista)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuarioDentista);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(usuarioDentista);
        }

        // GET: UsuarioDentistas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UsuariosDentistas == null)
            {
                return NotFound();
            }

            var usuarioDentista = await _context.UsuariosDentistas.FindAsync(id);
            if (usuarioDentista == null)
            {
                return NotFound();
            }
            return View(usuarioDentista);
        }

        // POST: UsuarioDentistas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Telefone,Email,CRO,Nome_da_clinica,Endereço,Senha")] UsuarioDentista usuarioDentista)
        {
            if (id != usuarioDentista.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuarioDentista);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioDentistaExists(usuarioDentista.Id))
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
            return View(usuarioDentista);
        }

        // GET: UsuarioDentistas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UsuariosDentistas == null)
            {
                return NotFound();
            }

            var usuarioDentista = await _context.UsuariosDentistas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuarioDentista == null)
            {
                return NotFound();
            }

            return View(usuarioDentista);
        }

        // POST: UsuarioDentistas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UsuariosDentistas == null)
            {
                return Problem("Entity set 'AppDbContext.UsuariosDentistas'  is null.");
            }
            var usuarioDentista = await _context.UsuariosDentistas.FindAsync(id);
            if (usuarioDentista != null)
            {
                _context.UsuariosDentistas.Remove(usuarioDentista);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioDentistaExists(int id)
        {
          return _context.UsuariosDentistas.Any(e => e.Id == id);
        }
    }
}
