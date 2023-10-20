using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;
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

        // Verificação de autorização para detalhes
        var clienteEmail = User.Identity.Name;
        var dentistaEmail = User.Identity.Name;

        if (User.IsInRole("Dentista") && agendaEvento.UsuarioDentista.Email != dentistaEmail)
        {
            return Forbid();
        }

        if (User.IsInRole("Cliente") && agendaEvento.UsuarioCliente.Email != clienteEmail)
        {
            return Forbid();
        }

        return View(agendaEvento);
    }

    // GET: AgendaEventoes/Create
    public IActionResult Create()
    {
        ViewData["UsuarioClienteId"] = new SelectList(_context.UsuariosClientes, "Id", "Name");
        ViewData["UsuarioDentistaId"] = new SelectList(_context.UsuariosDentistas, "Id", "Name");
        return View();
    }

    // POST: AgendaEventoes/Create
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
        ViewData["UsuarioClienteId"] = new SelectList(_context.UsuariosClientes, "Id", "Name", agendaEvento.UsuarioClienteId);
        ViewData["UsuarioDentistaId"] = new SelectList(_context.UsuariosDentistas, "Id", "Name", agendaEvento.UsuarioDentistaId);
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

        // Verificação de autorização para edição
        var clienteEmail = User.Identity.Name;
        var dentistaEmail = User.Identity.Name;

        if (User.IsInRole("Dentista") && agendaEvento.UsuarioDentista.Email != dentistaEmail)
        {
            return Forbid();
        }

        if (User.IsInRole("Cliente") && agendaEvento.UsuarioCliente.Email != clienteEmail)
        {
            return Forbid();
        }

        ViewData["UsuarioClienteId"] = new SelectList(_context.UsuariosClientes, "Id", "Name", agendaEvento.UsuarioClienteId);
        ViewData["UsuarioDentistaId"] = new SelectList(_context.UsuariosDentistas, "Id", "Name", agendaEvento.UsuarioDentistaId);
        return View(agendaEvento);
    }

    // ... outros métodos ...

    private bool AgendaEventoExists(int id)
    {
        return _context.AgendaEventos.Any(e => e.Id == id);
    }
}
