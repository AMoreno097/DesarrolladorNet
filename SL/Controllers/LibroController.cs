using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{
	public class LibroController : Controller
	{
		[HttpGet]
		[Route("api/Libro")]
		public IActionResult GetAll()
		{
			ML.Libros producto = new ML.Libros();
			ML.Result result = BL.Libro.GetAll(producto);

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
