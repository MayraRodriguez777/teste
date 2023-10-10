using Eassydentalmvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class DentistasController : Controller
{
    private readonly AppDbContext _context;

    public DentistasController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var dentistas = await _context.Dentistas.ToListAsync();
        return View(dentistas);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Telefone,Email,CRO,Nome_da_clinica,Endereço")] Dentista dentista)
    {
        if (ModelState.IsValid)
        {
            _context.Add(dentista);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(dentista);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var dentista = await _context.Dentistas.FindAsync(id);

        if (dentista == null)
        {
            return NotFound();
        }

        return View(dentista);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Telefone,Email,CRO,Nome_da_clinica,Endereço")] Dentista dentista)
    {
        if (id != dentista.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(dentista);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DentistaExists(dentista.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        return View(dentista);
    }

    private bool DentistaExists(int id)
    {
        return _context.Dentistas.Any(e => e.Id == id);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var dentista = await _context.Dentistas.FirstOrDefaultAsync(m => m.Id == id);

        if (dentista == null)
        {
            return NotFound();
        }

        return View(dentista);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var dentista = await _context.Dentistas.FirstOrDefaultAsync(m => m.Id == id);

        if (dentista == null)
        {
            return NotFound();
        }

        return View(dentista);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var dentista = await _context.Dentistas.FindAsync(id);
        _context.Dentistas.Remove(dentista);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
