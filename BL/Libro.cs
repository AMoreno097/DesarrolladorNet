using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Data.SqlClient; //Importar libreria
using System.Data;
using ML;
using DL;
using System.Xml;
using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace BL
{
    public class Libro
    {

		public static ML.Result GetAll(ML.Libros libro)
		{
			ML.Result result = new ML.Result();

			try
			{
				using (DL.LibreriaContext context = new DL.LibreriaContext())
				{
					var productos = context.Libros.FromSqlRaw($"GetAllLibro").ToList();
					result.Objects = new List<object>();


					//if (productos != null)
					//{
					// result.Objects = new List<object>();
					foreach (var productoObj in productos)
					{
						libro = new ML.Libros();

						libro.IdLibro = productoObj.IdLibro;
						libro.TituloLibro = productoObj.TituloLibro;
						libro.AnioPublicacion = productoObj.AnioPublicacion;
						//libro.Portada = productoObj.Portada;
						

						libro.Editorial = new ML.Editorial();
						libro.Editorial.IdEditorial = productoObj.IdEditorial.Value;
						libro.Editorial.NombreEditorial = productoObj.NombreEditorial;

						libro.Autor = new ML.Autor();
						libro.Autor.IdAutor = productoObj.IdAutor.Value;
						libro.Autor.NombreAutor = productoObj.NombreAutor;
						libro.Autor.ApellidoPaterno = productoObj.ApellidoPaterno;
						libro.Autor.ApellidoMaterno = productoObj.ApellidoMaterno;
						libro.Autor.Pais = productoObj.Pais;
						libro.Autor.FechaNacimineto = productoObj.FechaNacimineto;

						




						result.Objects.Add(libro);
					}
					result.Correct = true;
					//}
					//else
					//{
					//    result.Correct = false;
					//    result.ErrorMessage = "Ocurrio un error";
					//}
				}
			}
			catch (Exception ex)
			{
				result.Correct = false;
				result.ErrorMessage = ex.Message;
				result.Ex = ex;
			}

			return result;

		}

	}
}