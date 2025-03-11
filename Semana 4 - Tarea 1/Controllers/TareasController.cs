// Controllers/TareasController.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using GestorProyectos.Data;
using GestorProyectos.Models;

namespace GestorProyectos.Controllers
{
    /// <summary>
    /// Controlador generado por scaffolding para gestionar tareas.
    /// Permite crear, leer, actualizar y eliminar tareas.
    /// </summary>
    [Authorize] // Requiere autenticación para acceder a cualquier acción
    public class TareasController : Controller
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Constructor que recibe el contexto de base de datos mediante inyección de dependencias.
        /// </summary>
        /// <param name="context">Contexto de la base de datos</param>
        public TareasController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Acción que muestra la lista de todas las tareas.
        /// </summary>
        /// <returns>Vista con la lista de tareas</returns>
        // GET: Tareas
        public async Task<IActionResult> Index()
        {
            // Obtener todas las tareas incluyendo información del proyecto relacionado
            var tareas = _context.Tareas.Include(t => t.Proyecto);
            return View(await tareas.ToListAsync());
        }

        /// <summary>
        /// Acción que muestra los detalles de una tarea específica.
        /// </summary>
        /// <param name="id">ID de la tarea</param>
        /// <returns>Vista con los detalles de la tarea</returns>
        // GET: Tareas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Obtener la tarea con sus relaciones
            var tarea = await _context.Tareas
                .Include(t => t.Proyecto)
                .Include(t => t.Comentarios)
                    .ThenInclude(c => c.Usuario)
                .Include(t => t.Archivos)
                .FirstOrDefaultAsync(m => m.TareaID == id);

            if (tarea == null)
            {
                return NotFound();
            }

            return View(tarea);
        }

        /// <summary>
        /// Acción que muestra el formulario para crear una nueva tarea.
        /// </summary>
        /// <returns>Vista con el formulario de creación</returns>
        // GET: Tareas/Create
        public IActionResult Create()
        {
            // Pasar la lista de proyectos para el select
            ViewData["ProyectoID"] = new SelectList(_context.Proyectos, "ProyectoID", "Nombre");
            return View();
        }

        /// <summary>
        /// Acción que procesa el formulario de creación de una tarea.
        /// </summary>
        /// <param name="tarea">Datos de la nueva tarea</param>
        /// <returns>Redirección a la lista de tareas o formulario con errores</returns>
        // POST: Tareas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TareaID,ProyectoID,Titulo,Descripcion,Estado")] Tarea tarea)
        {
            if (ModelState.IsValid)
            {
                // Agregar la tarea a la base de datos
                _context.Add(tarea);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProyectoID"] = new SelectList(_context.Proyectos, "ProyectoID", "Nombre", tarea.ProyectoID);
            return View(tarea);
        }

        /// <summary>
        /// Acción que muestra el formulario para editar una tarea existente.
        /// </summary>
        /// <param name="id">ID de la tarea a editar</param>
        /// <returns>Vista con el formulario de edición</returns>
        // GET: Tareas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarea = await _context.Tareas.FindAsync(id);
            if (tarea == null)
            {
                return NotFound();
            }
            ViewData["ProyectoID"] = new SelectList(_context.Proyectos, "ProyectoID", "Nombre", tarea.ProyectoID);
            return View(tarea);
        }

        /// <summary>
        /// Acción que procesa el formulario de edición de una tarea.
        /// </summary>
        /// <param name="id">ID de la tarea</param>
        /// <param name="tarea">Datos actualizados de la tarea</param>
        /// <returns>Redirección a la lista de tareas o formulario con errores</returns>
        // POST: Tareas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TareaID,ProyectoID,Titulo,Descripcion,Estado")] Tarea tarea)
        {
            if (id != tarea.TareaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Actualizar la tarea en la base de datos
                    _context.Update(tarea);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TareaExists(tarea.TareaID))
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
            ViewData["ProyectoID"] = new SelectList(_context.Proyectos, "ProyectoID", "Nombre", tarea.ProyectoID);
            return View(tarea);
        }

        /// <summary>
        /// Acción que muestra la confirmación para eliminar una tarea.
        /// </summary>
        /// <param name="id">ID de la tarea a eliminar</param>
        /// <returns>Vista de confirmación de eliminación</returns>
        // GET: Tareas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarea = await _context.Tareas
                .Include(t => t.Proyecto)
                .FirstOrDefaultAsync(m => m.TareaID == id);
            if (tarea == null)
            {
                return NotFound();
            }

            return View(tarea);
        }

        /// <summary>
        /// Acción que procesa la eliminación de una tarea.
        /// </summary>
        /// <param name="id">ID de la tarea a eliminar</param>
        /// <returns>Redirección a la lista de tareas</returns>
        // POST: Tareas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tarea = await _context.Tareas.FindAsync(id);
            if (tarea != null)
            {
                _context.Tareas.Remove(tarea);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Método auxiliar para verificar si una tarea existe en la base de datos.
        /// </summary>
        /// <param name="id">ID de la tarea</param>
        /// <returns>True si la tarea existe, False en caso contrario</returns>
        private bool TareaExists(int id)
        {
            return _context.Tareas.Any(e => e.TareaID == id);
        }
    }
}