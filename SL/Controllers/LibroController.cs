using Microsoft.AspNetCore.Mvc;


namespace SL.Controllers
{
    public class LibroController : Controller
	{
		[HttpGet]
		[Route("api/Libro/GetAll/Titulo/Fecha/IdAutor/IdEditorial")]
		public IActionResult GetAll(string Titulo, string Fecha, int IdAutor, int IdEditorial)
		{
			ML.Libros libro = new ML.Libros();
			libro.TituloLibro = Titulo;
			libro.AnioPublicacion = Fecha;
			libro.Autor = new ML.Autor();
			libro.Autor.IdAutor = IdAutor;



			libro.Editorial = new ML.Editorial();
			libro.Editorial.IdEditorial = IdEditorial;


			ML.Result result = BL.Libro.GetAll(libro);





			if (result.Correct)
			{
				return Ok(result);
			}
			else
			{
				return NotFound();
			}
		}


		[HttpPost]
		[Route("api/Libro/Add")]
		public IActionResult Post([FromBody] ML.Libros libro)
		{
			ML.Result result = BL.Libro.Add(libro);

			if (result.Correct)
			{
				return Ok(result);
			}
			else
			{
				return NotFound();
			}
		}
	}
}
