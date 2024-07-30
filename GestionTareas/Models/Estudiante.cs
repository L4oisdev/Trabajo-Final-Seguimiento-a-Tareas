using System;
using System.Collections.Generic;

namespace GestionTareas.Models;

public partial class Estudiante
{
    public int EstudianteId { get; set; }

    public string Nombre { get; set; } = null!;

    public int? EquipoId { get; set; }

    public virtual ICollection<Equipo> Equipos { get; set; } = new List<Equipo>();

    public virtual Equipo? Equipo { get; set; }  
    
    // Relación con Equipo
    public virtual ICollection<Tarea> Tareas { get; set; } = new List<Tarea>();
}
