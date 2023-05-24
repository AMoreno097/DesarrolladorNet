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

		//public static ML.Result GetAll(ML.Libros libro)
		//{
		//	ML.Result result = new ML.Result();

		//	try
		//	{
		//		using (DL.LibreriaContext context = new DL.LibreriaContext())
		//		{
		//			var libros = context.Libros.FromSqlRaw($"GetAllLibro").ToList();
		//			result.Objects = new List<object>();


		//			//if (productos != null)
		//			//{
		//			// result.Objects = new List<object>();
		//			foreach (var librpObj in libros)
		//			{
		//				libro = new ML.Libros();

		//				libro.IdLibro = librpObj.IdLibro;
		//				libro.TituloLibro = librpObj.TituloLibro;
		//				libro.AnioPublicacion = librpObj.AnioPublicacion;
		//				//libro.Portada = productoObj.Portada;
						

		//				libro.Editorial = new ML.Editorial();
		//				libro.Editorial.IdEditorial = librpObj.IdEditorial.Value;
		//				libro.Editorial.NombreEditorial = librpObj.NombreEditorial;

		//				libro.Autor = new ML.Autor();
		//				libro.Autor.IdAutor = librpObj.IdAutor.Value;
		//				libro.Autor.NombreAutor = librpObj.NombreAutor;
		//				libro.Autor.ApellidoPaterno = librpObj.ApellidoPaterno;
		//				libro.Autor.ApellidoMaterno = librpObj.ApellidoMaterno;
		//				libro.Autor.Pais = librpObj.Pais;
		//				libro.Autor.FechaNacimiento = librpObj.FechaNacimiento;


		//				result.Objects.Add(libro);
		//			}
		//			result.Correct = true;
		//			//}
		//			//else
		//			//{
		//			//    result.Correct = false;
		//			//    result.ErrorMessage = "Ocurrio un error";
		//			//}
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		result.Correct = false;
		//		result.ErrorMessage = ex.Message;
		//		result.Ex = ex;
		//	}

		//	return result;

		//}


        public static ML.Result Add(ML.Libros libro)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.LibreriaContext context = new DL.LibreriaContext())
                {
                    // var query = context.ProductoAdd(producto.NombreProducto, producto.PrecioUnitario, producto.Stock, producto.Descripcion, producto.Proveedor.IdProveedor, producto.Departamento.IdDepartamento);
                    var query = context.Database.ExecuteSqlRaw($"AddLibro '{libro.TituloLibro}', '{libro.AnioPublicacion}', {libro.Portada}, {libro.Autor.IdAutor}, {libro.Editorial.IdEditorial}");

                    //var query = context.Database.ExecuteSqlRaw($"ProductoAdd", producto.NombreProducto, producto.PrecioUnitario, producto.Stock, producto.Descripcion, producto.Proveedor.IdProveedor, producto.Departamento.IdDepartamento);
                    //var query = 0;
                    if (query > 0)
                    {
                        result.Correct = true;

                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "OCURRIO UN ERROR";
                    }
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