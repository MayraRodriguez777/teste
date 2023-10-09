using Eassydentalmvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class ClientesController : Controller
{
    private readonly AppDbContext _context;
    public async Task<IActionResult> Index()
    {
        var clientes = await _context.Clientes.ToListAsync();
        return View(clientes);
    }

    public ClientesController(AppDbContext context)
    {
        _context = context;
    }

    // ... outras ações

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Telefone,Email")] Cliente cliente)
    {
        if (ModelState.IsValid)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
            return RedirectToAction("Create");
        }
        return View(cliente);


    }
}

