using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class LibroMaterialDTO
{
    public Guid? Id { get; set; }
    public string Titulo { get; set; }
    public DateTime? FechaPublicacion { get; set; }
    public Guid? AutorLibro { get; set; }
}

