using System;
using System.Collections.Generic;

namespace GestionTareas.Models;

public partial class Equipo
{
    public int EquipoId { get; set; }

    public string NombreEquipo { get; set; } = null!;

    public int? LiderEquipoId { get; set; }

    public virtual Estudiante? LiderEquipo { get; set; }
}
