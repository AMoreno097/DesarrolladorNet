using System;
using System.Collections.Generic;

namespace DL;

public partial class Editorial
{
    public int IdEditorial { get; set; }

    public string NombreEditorial { get; set; } = null!;

    public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();
}
