﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Libros
    {
        public int IdLibro { get; set; }
        public string TituloLibro { get; set; }
        public string AnioPublicacion { get; set; }
        public string Portada { get; set; }
		public List<object> Libro { get; set; }
		public ML.Editorial Editorial { get; set; }
		public ML.Autor Autor { get; set; }
	}
}
