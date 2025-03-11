﻿// <auto-generated />
using System;
using GestorProyectos.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GestorProyectos.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GestorProyectos.Models.Archivo", b =>
                {
                    b.Property<int>("ArchivoID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ArchivoID"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Ruta")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("TareaID")
                        .HasColumnType("int");

                    b.Property<string>("TipoArchivo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ArchivoID");

                    b.HasIndex("TareaID");

                    b.ToTable("Archivos");
                });

            modelBuilder.Entity("GestorProyectos.Models.Comentario", b =>
                {
                    b.Property<int>("ComentarioID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ComentarioID"));

                    b.Property<string>("Contenido")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<int>("TareaID")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioID")
                        .HasColumnType("int");

                    b.HasKey("ComentarioID");

                    b.HasIndex("TareaID");

                    b.HasIndex("UsuarioID");

                    b.ToTable("Comentarios");
                });

            modelBuilder.Entity("GestorProyectos.Models.Proyecto", b =>
                {
                    b.Property<int>("ProyectoID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProyectoID"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ProyectoID");

                    b.ToTable("Proyectos");

                    b.HasData(
                        new
                        {
                            ProyectoID = 1,
                            Descripcion = "Este es un proyecto de ejemplo para demostrar la funcionalidad del sistema",
                            Estado = "En Progreso",
                            FechaInicio = new DateTime(2025, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Nombre = "Proyecto de Ejemplo"
                        });
                });

            modelBuilder.Entity("GestorProyectos.Models.Tarea", b =>
                {
                    b.Property<int>("TareaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TareaID"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("ProyectoID")
                        .HasColumnType("int");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("TareaID");

                    b.HasIndex("ProyectoID");

                    b.ToTable("Tareas");

                    b.HasData(
                        new
                        {
                            TareaID = 1,
                            Descripcion = "Esta es una tarea de ejemplo asociada al proyecto de ejemplo",
                            Estado = "Pendiente",
                            ProyectoID = 1,
                            Titulo = "Tarea de Ejemplo"
                        });
                });

            modelBuilder.Entity("GestorProyectos.Models.Usuario", b =>
                {
                    b.Property<int>("UsuarioID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UsuarioID"));

                    b.Property<string>("Contraseña")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Rol")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("UsuarioID");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Usuarios");

                    b.HasData(
                        new
                        {
                            UsuarioID = 1,
                            Contraseña = "Admin123*",
                            Email = "admin@example.com",
                            Nombre = "Administrador",
                            Rol = "Administrador"
                        },
                        new
                        {
                            UsuarioID = 2,
                            Contraseña = "Usuario123*",
                            Email = "usuario@example.com",
                            Nombre = "Usuario Estándar",
                            Rol = "Usuario"
                        });
                });

            modelBuilder.Entity("GestorProyectos.Models.Archivo", b =>
                {
                    b.HasOne("GestorProyectos.Models.Tarea", "Tarea")
                        .WithMany("Archivos")
                        .HasForeignKey("TareaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tarea");
                });

            modelBuilder.Entity("GestorProyectos.Models.Comentario", b =>
                {
                    b.HasOne("GestorProyectos.Models.Tarea", "Tarea")
                        .WithMany("Comentarios")
                        .HasForeignKey("TareaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GestorProyectos.Models.Usuario", "Usuario")
                        .WithMany("Comentarios")
                        .HasForeignKey("UsuarioID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Tarea");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("GestorProyectos.Models.Tarea", b =>
                {
                    b.HasOne("GestorProyectos.Models.Proyecto", "Proyecto")
                        .WithMany("Tareas")
                        .HasForeignKey("ProyectoID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Proyecto");
                });

            modelBuilder.Entity("GestorProyectos.Models.Proyecto", b =>
                {
                    b.Navigation("Tareas");
                });

            modelBuilder.Entity("GestorProyectos.Models.Tarea", b =>
                {
                    b.Navigation("Archivos");

                    b.Navigation("Comentarios");
                });

            modelBuilder.Entity("GestorProyectos.Models.Usuario", b =>
                {
                    b.Navigation("Comentarios");
                });
#pragma warning restore 612, 618
        }
    }
}
