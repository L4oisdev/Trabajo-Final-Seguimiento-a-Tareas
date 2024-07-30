using GestionTareas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace GestionTareas.Controllers
{
    public class ProyectoController : Controller
    {
        private readonly GestionTareasContext _context;

        public ProyectoController(GestionTareasContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Lista()
        {
            List<Proyecto> proyectos = await _context.Proyectos.ToListAsync();
            return View(proyectos);
        }

        [HttpGet]
        public async Task<IActionResult> Agregar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Agregar(Proyecto proyecto)
        {
            _context.Proyectos.Add(proyecto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Lista));
        }

        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarConfirmado(int id)
        {
            var proyecto = await _context.Proyectos.FindAsync(id);
            if (proyecto == null)
            {
                return NotFound();
            }

            _context.Proyectos.Remove(proyecto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Lista));
        }

    }
}
