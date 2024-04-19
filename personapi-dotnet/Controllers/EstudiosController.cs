using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging; // Asegúrate de agregar este using para el logging
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Controllers
{
    public class EstudiosController : Controller
    {
        private readonly PersonaDbContext _context;
        private readonly ILogger<EstudiosController> _logger; // Agrega un logger

        public EstudiosController(PersonaDbContext context, ILogger<EstudiosController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Estudios
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Loading index page with all studies");
            var personaDbContext = _context.Estudios.Include(e => e.CcPerNavigation).Include(e => e.IdProfNavigation);
            return View(await personaDbContext.ToListAsync());
        }

        // GET: Estudios/Details/5
        public async Task<IActionResult> Details(int? idProf, int? ccPer)
        {
            if (idProf == null || ccPer == null)
            {
                _logger.LogWarning("Details view called without a proper idProf or ccPer");
                return NotFound();
            }

            _logger.LogInformation("Getting details for idProf {IdProf} and ccPer {CcPer}", idProf, ccPer);
            var estudio = await _context.Estudios
                .Include(e => e.CcPerNavigation)
                .Include(e => e.IdProfNavigation)
                .FirstOrDefaultAsync(m => m.IdProf == idProf && m.CcPer == ccPer);

            if (estudio == null)
            {
                _logger.LogWarning("Details for idProf {IdProf} and ccPer {CcPer} not found", idProf, ccPer);
                return NotFound();
            }

            return View(estudio);
        }

        // GET: Estudios/Create
        public IActionResult Create()
        {
            ViewData["CcPer"] = new SelectList(_context.Personas, "Cc", "Cc");
            ViewData["IdProf"] = new SelectList(_context.Profesions, "Id", "Id");
            return View();
        }

        // POST: Estudios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProf,CcPer,Fecha,Univer")] Estudio estudio)
        {
            ModelState.Remove("CcPerNavigation");
            ModelState.Remove("IdProfNavigation");
            if (ModelState.IsValid)
            {
                _logger.LogInformation("Creating a new study with idProf {IdProf} and ccPer {CcPer}", estudio.IdProf, estudio.CcPer);
                _context.Add(estudio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                _logger.LogWarning("Model state is invalid");
            }

            ViewData["CcPer"] = new SelectList(_context.Personas, "Cc", "Cc", estudio.CcPer);
            ViewData["IdProf"] = new SelectList(_context.Profesions, "Id", "Id", estudio.IdProf);
            return View(estudio);
        }

        // GET: Estudios/Edit/5
        public async Task<IActionResult> Edit(int? idProf, int? ccPer)
        {
            if (idProf == null || ccPer == null)
            {
                _logger.LogWarning("Edit called without a proper idProf or ccPer");
                return NotFound();
            }

            _logger.LogInformation("Editing study for idProf {IdProf} and ccPer {CcPer}", idProf, ccPer);
            var estudio = await _context.Estudios
                .Include(e => e.CcPerNavigation)
                .Include(e => e.IdProfNavigation)
                .FirstOrDefaultAsync(m => m.IdProf == idProf && m.CcPer == ccPer);
            if (estudio == null)
            {
                _logger.LogWarning("Study for idProf {IdProf} and ccPer {CcPer} not found", idProf, ccPer);
                return NotFound();
            }

            ViewData["CcPer"] = new SelectList(_context.Personas, "Cc", "Cc", estudio.CcPer);
            ViewData["IdProf"] = new SelectList(_context.Profesions, "Id", "Id", estudio.IdProf);
            return View(estudio);
        }

        // POST: Estudios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int idProf, int ccPer, [Bind("IdProf,CcPer,Fecha,Univer")] Estudio estudio)
        {
            ModelState.Remove("CcPerNavigation");
            ModelState.Remove("IdProfNavigation");
            if (idProf != estudio.IdProf || ccPer != estudio.CcPer)
            {
                _logger.LogError("Mismatch in idProf or ccPer on edit");
                return NotFound();
            }

            if (ModelState.IsValid)
            {
               

                try
                {
                    _logger.LogInformation("Updating study for idProf {IdProf} and ccPer {CcPer}", idProf, ccPer);
                    _context.Update(estudio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if (!EstudioExists(estudio.IdProf, estudio.CcPer))
                    {
                        _logger.LogError(ex, "Study for idProf {IdProf} and ccPer {CcPer} not found", idProf, ccPer);
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            else if (!ModelState.IsValid)
            {
                _logger.LogWarning("Model state is invalid on edit");
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    _logger.LogWarning("Validation error: {ErrorMessage}", error.ErrorMessage);
                }
                ViewData["CcPer"] = new SelectList(_context.Personas, "Cc", "Cc", estudio.CcPer);
                ViewData["IdProf"] = new SelectList(_context.Profesions, "Id", "Id", estudio.IdProf);
                return View(estudio);
            }
            
                
            

            ViewData["CcPer"] = new SelectList(_context.Personas, "Cc", "Cc", estudio.CcPer);
            ViewData["IdProf"] = new SelectList(_context.Profesions, "Id", "Id", estudio.IdProf);
            return View(estudio);
        }

        // GET: Estudios/Delete/5
        public async Task<IActionResult> Delete(int? idProf, int? ccPer)
        {
            if (idProf == null || ccPer == null)
            {
                _logger.LogWarning("Delete called without a proper idProf or ccPer");
                return NotFound();
            }

            _logger.LogInformation("Deleting study for idProf {IdProf} and ccPer {CcPer}", idProf, ccPer);
            var estudio = await _context.Estudios
                .Include(e => e.CcPerNavigation)
                .Include(e => e.IdProfNavigation)
                .FirstOrDefaultAsync(m => m.IdProf == idProf && m.CcPer == ccPer);
            if (estudio == null)
            {
                _logger.LogWarning("Study for idProf {IdProf} and ccPer {CcPer} not found", idProf, ccPer);
                return NotFound();
            }

            return View(estudio);
        }

        // POST: Estudios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int idProf, int ccPer)
        {
            if (_context.Estudios == null)
            {
                _logger.LogError("Entity set 'PersonaDbContext.Estudios' is null");
                return Problem("Entity set 'PersonaDbContext.Estudios' is null.");
            }
            var estudio = await _context.Estudios
                .FirstOrDefaultAsync(m => m.IdProf == idProf && m.CcPer == ccPer);

            if (estudio != null)
            {
                _logger.LogInformation("Confirmed deletion of study for idProf {IdProf} and ccPer {CcPer}", idProf, ccPer);
                _context.Estudios.Remove(estudio);
                await _context.SaveChangesAsync();
            }
            else
            {
                _logger.LogWarning("Attempt to delete non-existent study for idProf {IdProf} and ccPer {CcPer}", idProf, ccPer);
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool EstudioExists(int idProf, int ccPer)
        {
            bool exists = _context.Estudios.Any(e => e.IdProf == idProf && e.CcPer == ccPer);
            _logger.LogInformation("Checking existence of study for idProf {IdProf} and ccPer {CcPer}: {Exists}", idProf, ccPer, exists);
            return exists;
        }
    }
}
