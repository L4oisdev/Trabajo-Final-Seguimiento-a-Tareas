using GestionTareas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GestionTareas.Controllers
{
    public class TareaController : Controller
    {
        private readonly GestionTareasContext _context;

        public TareaController(GestionTareasContext context)
        {
            _context = context; 
        }

        public async Task<IActionResult > Lista()
        {
            List<Tarea> tareas = await _context.Tareas
                .Include(t => t.Proyecto)
                .Include(t => t.Responsable)
                .ToListAsync();

            return View(tareas);
        }

        [HttpGet]
        public async Task<IActionResult> Agregar()
        {
            // Cargar proyectos y responsables para los select lists
            ViewBag.Proyectos = new SelectList(await _context.Proyectos.ToListAsync(), "ProyectoId", "Titulo");
            ViewBag.Responsables = new SelectList(await _context.Estudiantes.ToListAsync(), "EstudianteId", "Nombre");
            ViewBag.Estados = new SelectList(new List<string> { "pendiente", "en proceso", "finalizada", "desestimada" });
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Agregar(Tarea tarea)
        {
            if (ModelState.IsValid)
            {
                // Verificar que el proyecto y el responsable existen
                var proyectoExiste = await _context.Proyectos.AnyAsync(p => p.ProyectoId == tarea.ProyectoId);
                var responsableExiste = await _context.Estudiantes.AnyAsync(e => e.EstudianteId == tarea.ResponsableId);

                if (!proyectoExiste || !responsableExiste)
                {
                    ModelState.AddModelError("", "El proyecto o responsable seleccionado no existe.");
                    ViewBag.Proyectos = new SelectList(await _context.Proyectos.ToListAsync(), "ProyectoId", "Titulo", tarea.ProyectoId);
                    ViewBag.Responsables = new SelectList(await _context.Estudiantes.ToListAsync(), "EstudianteId", "Nombre", tarea.ResponsableId);
                    return View(tarea);
                }

                // Agregar la tarea a la base de datos
                _context.Tareas.Add(tarea);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Lista));
            }

            // En caso de error, volver a cargar los datos y mostrar el formulario con errores
            ViewBag.Proyectos = new SelectList(await _context.Proyectos.ToListAsync(), "ProyectoId", "Titulo", tarea.ProyectoId);
            ViewBag.Responsables = new SelectList(await _context.Estudiantes.ToListAsync(), "EstudianteId", "Nombre", tarea.ResponsableId);
            return View(tarea);
        }


        // Método para editar una tarea
        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            Tarea tarea = await _context.Tareas.FirstAsync(e => e.TareaId == id);
            // Cargar proyectos y responsables para los select lists
            ViewBag.Proyectos = new SelectList(await _context.Proyectos.ToListAsync(), "ProyectoId", "Titulo");
            ViewBag.Responsables = new SelectList(await _context.Estudiantes.ToListAsync(), "EstudianteId", "Nombre");
            ViewBag.Estados = new SelectList(new List<string> { "pendiente", "en proceso", "finalizada", "desestimada" });
            return View(tarea);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Tarea tarea)
        {
            _context.Tareas.Update(tarea);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Lista));
        }

        // Método para eliminar una tarea
        [HttpGet]
        public async Task<IActionResult> Eliminar(int id)
        {
            Tarea tarea = await _context.Tareas.FirstAsync(e => e.TareaId == id);
            _context.Tareas.Remove(tarea);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Lista));
        }
    }
}
