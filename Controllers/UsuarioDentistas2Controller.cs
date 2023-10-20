using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class UsuarioDentistas2Controller : Controller
    {
        private readonly AppDbContext _context;

        public UsuarioDentistas2Controller(AppDbContext context)
        {
            _context = context;
        }

        // GET: UsuarioDentistas2

        public async Task<IActionResult> Index()
        {
            return View(await _context.UsuariosDentistas.ToListAsync());


        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string senha)
        {
            var usuario = await _context.UsuariosDentistas.FirstOrDefaultAsync(u => u.Email == email);

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
                return Redirect(Url.Action("Index", "AgendaEventoes"));
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
            return RedirectToAction("Login", "UsuariosDentistas2");

        }
        // GET: UsuarioDentistas2/Details/5
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

        // GET: UsuarioDentistas2/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UsuarioDentistas2/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Telefone,Email,CRO,Especialidade,Nome_da_clinica,Endereço,Senha,Foto")] UsuarioDentista usuarioDentista)
        {
            if (ModelState.IsValid)
            {
                usuarioDentista.Senha = BCrypt.Net.BCrypt.HashPassword(usuarioDentista.Senha);
                _context.Add(usuarioDentista);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(usuarioDentista);
        }

        // GET: UsuarioDentistas2/Edit/5
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

        // POST: UsuarioDentistas2/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Telefone,Email,CRO,Especialidade,Nome_da_clinica,Endereço,Senha,Foto")] UsuarioDentista usuarioDentista)
        {
            if (id != usuarioDentista.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    usuarioDentista.Senha = BCrypt.Net.BCrypt.HashPassword(usuarioDentista.Senha);
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

        // GET: UsuarioDentistas2/Delete/5
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

        // POST: UsuarioDentistas2/Delete/5
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

