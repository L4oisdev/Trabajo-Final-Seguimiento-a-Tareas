using System;
using System.Collections.Generic;

namespace GestionTareas.Models;

public partial class Proyecto
{
    public int ProyectoId { get; set; }

    public string CodigoProyecto { get; set; } = null!;

    public string Titulo { get; set; } = null!;

    public string? Descripcion { get; set; }

    public virtual ICollection<Tarea> Tareas { get; set; } = new List<Tarea>();
}
