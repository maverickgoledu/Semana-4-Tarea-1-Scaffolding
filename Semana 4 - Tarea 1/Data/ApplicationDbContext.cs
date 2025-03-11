// Data/ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;
using GestorProyectos.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace GestorProyectos.Data
{
    /// <summary>
    /// Clase que representa el contexto de la base de datos para la aplicación.
    /// Gestiona las conexiones y define los conjuntos de entidades (DbSets).
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Constructor que recibe las opciones de configuración desde la inyección de dependencias.
        /// </summary>
        /// <param name="options">Opciones de configuración para el contexto</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Conjunto de entidades Usuario en la base de datos
        /// </summary>
        public DbSet<Usuario> Usuarios { get; set; }

        /// <summary>
        /// Conjunto de entidades Proyecto en la base de datos
        /// </summary>
        public DbSet<Proyecto> Proyectos { get; set; }

        /// <summary>
        /// Conjunto de entidades Tarea en la base de datos
        /// </summary>
        public DbSet<Tarea> Tareas { get; set; }

        /// <summary>
        /// Conjunto de entidades Comentario en la base de datos
        /// </summary>
        public DbSet<Comentario> Comentarios { get; set; }

        /// <summary>
        /// Conjunto de entidades Archivo en la base de datos
        /// </summary>
        public DbSet<Archivo> Archivos { get; set; }

        /// <summary>
        /// Método que configura el modelo de la base de datos.
        /// Define las relaciones, restricciones y configuraciones adicionales de las entidades.
        /// </summary>
        /// <param name="modelBuilder">Constructor del modelo de datos</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de la entidad Usuario
            modelBuilder.Entity<Usuario>(entity =>
            {
                // Establece el email como único para evitar duplicados
                entity.HasIndex(e => e.Email).IsUnique();
            });

            // Configuración de la entidad Proyecto
            modelBuilder.Entity<Proyecto>(entity =>
            {
                // Define la relación uno a muchos entre Proyecto y Tarea
                entity.HasMany(p => p.Tareas)
                      .WithOne(t => t.Proyecto)
                      .HasForeignKey(t => t.ProyectoID)
                      .OnDelete(DeleteBehavior.Cascade); // Si se elimina un proyecto, se eliminan sus tareas
            });

            // Configuración de la entidad Tarea
            modelBuilder.Entity<Tarea>(entity =>
            {
                // Define la relación uno a muchos entre Tarea y Comentario
                entity.HasMany(t => t.Comentarios)
                      .WithOne(c => c.Tarea)
                      .HasForeignKey(c => c.TareaID)
                      .OnDelete(DeleteBehavior.Cascade); // Si se elimina una tarea, se eliminan sus comentarios

                // Define la relación uno a muchos entre Tarea y Archivo
                entity.HasMany(t => t.Archivos)
                      .WithOne(a => a.Tarea)
                      .HasForeignKey(a => a.TareaID)
                      .OnDelete(DeleteBehavior.Cascade); // Si se elimina una tarea, se eliminan sus archivos
            });

            // Configuración de la entidad Comentario
            modelBuilder.Entity<Comentario>(entity =>
            {
                // Define la relación entre Comentario y Usuario
                entity.HasOne(c => c.Usuario)
                      .WithMany(u => u.Comentarios)
                      .HasForeignKey(c => c.UsuarioID)
                      .OnDelete(DeleteBehavior.Restrict); // No permite eliminar un usuario si tiene comentarios
            });

            // Datos semilla para pruebas
            SeedData(modelBuilder);
        }

        /// <summary>
        /// Método para poblar la base de datos con datos iniciales de prueba.
        /// </summary>
        /// <param name="modelBuilder">Constructor del modelo de datos</param>
        private void SeedData(ModelBuilder modelBuilder)
        {
            // Agrega un usuario administrador para pruebas
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario
                {
                    UsuarioID = 1,
                    Nombre = "Administrador",
                    Email = "admin@example.com",
                    // Nota: En producción, usar un hash de contraseña en lugar de texto plano
                    Contraseña = "Admin123*",
                    Rol = "Administrador"
                },
                new Usuario
                {
                    UsuarioID = 2,
                    Nombre = "Usuario Estándar",
                    Email = "usuario@example.com",
                    Contraseña = "Usuario123*",
                    Rol = "Usuario"
                }
            );

            // Agrega un proyecto de prueba
            modelBuilder.Entity<Proyecto>().HasData(
                new Proyecto
                {
                    ProyectoID = 1,
                    Nombre = "Proyecto de Ejemplo",
                    Descripcion = "Este es un proyecto de ejemplo para demostrar la funcionalidad del sistema",
                    FechaInicio = new DateTime(2025, 3, 11),
                    Estado = "En Progreso"
                }
            );

            // Agrega una tarea de prueba
            modelBuilder.Entity<Tarea>().HasData(
                new Tarea
                {
                    TareaID = 1,
                    ProyectoID = 1,
                    Titulo = "Tarea de Ejemplo",
                    Descripcion = "Esta es una tarea de ejemplo asociada al proyecto de ejemplo",
                    Estado = "Pendiente"
                }
            );
        }
    }
}