using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;
using BCrypt.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

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

        public IActionResult Login ()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string email, string senha)
        {
            var usuario = await _context.UsuariosClientes.FirstOrDefaultAsync(u => u.Email == email);

            if (usuario != null && BCrypt.Net.BCrypt.Verify(senha, usuario.Senha))
            {
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, usuario.Name),
            new Claim(ClaimTypes.NameIdentifier, usuario.Email.ToString())
        };

                var usuarioIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal principal = new ClaimsPrincipal(usuarioIdentity);
                var props = new AuthenticationProperties
                {
                    AllowRefresh = true,
                    ExpiresUtc = DateTime.UtcNow.AddHours(8),
                    IsPersistent = true,
                };

                await HttpContext.SignInAsync(principal, props);
                return Redirect("/");
            }
            else
            {
                ViewBag.Message = "Email e/ou senha inválidos!";
                return View();
            }
        }




        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "UsuarioClientes");

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
                usuarioCliente.Senha = BCrypt.Net.BCrypt.HashPassword(usuarioCliente.Senha);
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
                    usuarioCliente.Senha = BCrypt.Net.BCrypt.HashPassword(usuarioCliente.Senha);
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
