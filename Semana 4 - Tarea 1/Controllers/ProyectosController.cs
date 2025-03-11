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
    /// Controlador generado por scaffolding para gestionar proyectos.
    /// Permite crear, leer, actualizar y eliminar proyectos.
    /// </summary>
    [Authorize] // Requiere autenticación para acceder a cualquier acción
    public class ProyectosController : Controller
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Constructor que recibe el contexto de base de datos mediante inyección de dependencias.
        /// </summary>
        /// <param name="context">Contexto de la base de datos</param>
        public ProyectosController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Acción que muestra la lista de todos los proyectos.
        /// </summary>
        /// <returns>Vista con la lista de proyectos</returns>
        // GET: Proyectos
        public async Task<IActionResult> Index()
        {
            // Obtener todos los proyectos de la base de datos
            return View(await _context.Proyectos.ToListAsync());
        }

        /// <summary>
        /// Acción que muestra los detalles de un proyecto específico.
        /// </summary>
        /// <param name="id">ID del proyecto</param>
        /// <returns>Vista con los detalles del proyecto</returns>
        // GET: Proyectos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Obtener el proyecto con sus tareas relacionadas
            var proyecto = await _context.Proyectos
                .Include(p => p.Tareas)
                .FirstOrDefaultAsync(m => m.ProyectoID == id);

            if (proyecto == null)
            {
                return NotFound();
            }

            return View(proyecto);
        }

        /// <summary>
        /// Acción que muestra el formulario para crear un nuevo proyecto.
        /// </summary>
        /// <returns>Vista con el formulario de creación</returns>
        // GET: Proyectos/Create
        [Authorize(Roles = "Administrador")] // Solo los administradores pueden crear proyectos
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Acción que procesa el formulario de creación de un proyecto.
        /// </summary>
        /// <param name="proyecto">Datos del nuevo proyecto</param>
        /// <returns>Redirección a la lista de proyectos o formulario con errores</returns>
        // POST: Proyectos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")] // Solo los administradores pueden crear proyectos
        public async Task<IActionResult> Create([Bind("ProyectoID,Nombre,Descripcion,FechaInicio,Estado")] Proyecto proyecto)
        {
            if (ModelState.IsValid)
            {
                // Agregar el proyecto a la base de datos
                _context.Add(proyecto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(proyecto);
        }

        /// <summary>
        /// Acción que muestra el formulario para editar un proyecto existente.
        /// </summary>
        /// <param name="id">ID del proyecto a editar</param>
        /// <returns>Vista con el formulario de edición</returns>
        // GET: Proyectos/Edit/5
        [Authorize(Roles = "Administrador")] // Solo los administradores pueden editar proyectos
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proyecto = await _context.Proyectos.FindAsync(id);
            if (proyecto == null)
            {
                return NotFound();
            }
            return View(proyecto);
        }

        /// <summary>
        /// Acción que procesa el formulario de edición de un proyecto.
        /// </summary>
        /// <param name="id">ID del proyecto</param>
        /// <param name="proyecto">Datos actualizados del proyecto</param>
        /// <returns>Redirección a la lista de proyectos o formulario con errores</returns>
        // POST: Proyectos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")] // Solo los administradores pueden editar proyectos
        public async Task<IActionResult> Edit(int id, [Bind("ProyectoID,Nombre,Descripcion,FechaInicio,Estado")] Proyecto proyecto)
        {
            if (id != proyecto.ProyectoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Actualizar el proyecto en la base de datos
                    _context.Update(proyecto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProyectoExists(proyecto.ProyectoID))
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
            return View(proyecto);
        }

        /// <summary>
        /// Acción que muestra la confirmación para eliminar un proyecto.
        /// </summary>
        /// <param name="id">ID del proyecto a eliminar</param>
        /// <returns>Vista de confirmación de eliminación</returns>
        // GET: Proyectos/Delete/5
        [Authorize(Roles = "Administrador")] // Solo los administradores pueden eliminar proyectos
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proyecto = await _context.Proyectos
                .FirstOrDefaultAsync(m => m.ProyectoID == id);
            if (proyecto == null)
            {
                return NotFound();
            }

            return View(proyecto);
        }

        /// <summary>
        /// Acción que procesa la eliminación de un proyecto.
        /// </summary>
        /// <param name="id">ID del proyecto a eliminar</param>
        /// <returns>Redirección a la lista de proyectos</returns>
        // POST: Proyectos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")] // Solo los administradores pueden eliminar proyectos
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var proyecto = await _context.Proyectos.FindAsync(id);
            if (proyecto != null)
            {
                _context.Proyectos.Remove(proyecto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Método auxiliar para verificar si un proyecto existe en la base de datos.
        /// </summary>
        /// <param name="id">ID del proyecto</param>
        /// <returns>True si el proyecto existe, False en caso contrario</returns>
        private bool ProyectoExists(int id)
        {
            return _context.Proyectos.Any(e => e.ProyectoID == id);
        }
    }
}