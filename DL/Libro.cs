using System;
using System.Collections.Generic;

namespace DL;

public partial class Libro
{
    public int IdLibro { get; set; }

    public string TituloLibro { get; set; } = null!;

    public string AnioPublicacion { get; set; } = null!;

    public byte[]? Portada { get; set; }

    public int? IdAutor { get; set; }

    public int? IdEditorial { get; set; }

    public virtual Autor? IdAutorNavigation { get; set; }

    public virtual Editorial? IdEditorialNavigation { get; set; }
}
