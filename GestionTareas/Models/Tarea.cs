using System;
using System.Collections.Generic;

namespace GestionTareas.Models;

public partial class Tarea
{
    public int TareaId { get; set; }

    public int? ProyectoId { get; set; }

    public string Titulo { get; set; } = null!;

    public string? Descripcion { get; set; }

    public int? ResponsableId { get; set; }
    public DateOnly? FechaInicio { get; set; }

    public DateOnly? FechaFin { get; set; }

    public string? Status { get; set; }

    public  Proyecto? Proyecto { get; set; }

    public  Estudiante? Responsable { get; set; }
}
