using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GestionTareas.Models;

public partial class GestionTareasContext : DbContext
{
    public GestionTareasContext()
    {
    }

    public GestionTareasContext(DbContextOptions<GestionTareasContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Equipo> Equipos { get; set; }

    public virtual DbSet<Estudiante> Estudiantes { get; set; }

    public virtual DbSet<Proyecto> Proyectos { get; set; }

    public virtual DbSet<Tarea> Tareas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Equipo>(entity =>
        {
            entity.HasKey(e => e.EquipoId).HasName("PK__Equipos__DE8A0BFFE07E359B");

            entity.Property(e => e.EquipoId).HasColumnName("EquipoID");
            entity.Property(e => e.LiderEquipoId).HasColumnName("LiderEquipoID");
            entity.Property(e => e.NombreEquipo)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.LiderEquipo).WithMany(p => p.Equipos)
                .HasForeignKey(d => d.LiderEquipoId)
                .HasConstraintName("FK_Equipos_Estudiantes");
        });

        modelBuilder.Entity<Estudiante>(entity =>
        {
            entity.HasKey(e => e.EstudianteId).HasName("PK__Estudian__6F7683386BE4391F");

            entity.Property(e => e.EstudianteId).HasColumnName("EstudianteID");
            entity.Property(e => e.EquipoId).HasColumnName("EquipoID");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Proyecto>(entity =>
        {
            entity.HasKey(e => e.ProyectoId).HasName("PK__Proyecto__CF241D45660DDC4C");

            entity.Property(e => e.ProyectoId).HasColumnName("ProyectoID");
            entity.Property(e => e.CodigoProyecto)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Descripcion).IsUnicode(false);
            entity.Property(e => e.Titulo)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Tarea>()
            .HasOne(t => t.Proyecto)
            .WithMany(p => p.Tareas)
            .HasForeignKey(t => t.ProyectoId);

        modelBuilder.Entity<Tarea>()
            .HasOne(t => t.Responsable)
            .WithMany()
            .HasForeignKey(t => t.ResponsableId);

        modelBuilder.Entity<Tarea>(entity =>
        {
            entity.HasKey(e => e.TareaId).HasName("PK__Tareas__5CD83671A6361820");

            entity.Property(e => e.TareaId).HasColumnName("TareaID");
            entity.Property(e => e.Descripcion).IsUnicode(false);
            entity.Property(e => e.ProyectoId).HasColumnName("ProyectoID");
            entity.Property(e => e.ResponsableId).HasColumnName("ResponsableID");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Titulo)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Proyecto).WithMany(p => p.Tareas)
                .HasForeignKey(d => d.ProyectoId)
                .HasConstraintName("FK_Tareas_Proyectos");

            entity.HasOne(d => d.Responsable).WithMany(p => p.Tareas)
                .HasForeignKey(d => d.ResponsableId)
                .HasConstraintName("FK_Tareas_Estudiantes");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
