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
    public class UsuarioClientesController : Controller
    {
        private readonly AppDbContext _context;

        public UsuarioClientesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: UsuarioClientes
        public async Task<IActionResult> Index()
        {
              return View(await _context.UsuariosClientes.ToListAsync());
        }

        // GET: UsuarioClientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UsuariosClientes == null)
            {
                return NotFound();
            }

            var usuarioCliente = await _context.UsuariosClientes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuarioCliente == null)
            {
                return NotFound();
            }

            return View(usuarioCliente);
        }

        // GET: UsuarioClientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UsuarioClientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Telefone,Email,Senha")] UsuarioCliente usuarioCliente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuarioCliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(usuarioCliente);
        }

        // GET: UsuarioClientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UsuariosClientes == null)
            {
                return NotFound();
            }

            var usuarioCliente = await _context.UsuariosClientes.FindAsync(id);
            if (usuarioCliente == null)
            {
                return NotFound();
            }
            return View(usuarioCliente);
        }

        // POST: UsuarioClientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Telefone,Email,Senha")] UsuarioCliente usuarioCliente)
        {
            if (id != usuarioCliente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuarioCliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioClienteExists(usuarioCliente.Id))
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
            return View(usuarioCliente);
        }

        // GET: UsuarioClientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UsuariosClientes == null)
            {
                return NotFound();
            }

            var usuarioCliente = await _context.UsuariosClientes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuarioCliente == null)
            {
                return NotFound();
            }

            return View(usuarioCliente);
        }

        // POST: UsuarioClientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UsuariosClientes == null)
            {
                return Problem("Entity set 'AppDbContext.UsuariosClientes'  is null.");
            }
            var usuarioCliente = await _context.UsuariosClientes.FindAsync(id);
            if (usuarioCliente != null)
            {
                _context.UsuariosClientes.Remove(usuarioCliente);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioClienteExists(int id)
        {
          return _context.UsuariosClientes.Any(e => e.Id == id);
        }
    }
}
