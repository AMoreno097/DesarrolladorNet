using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{
	public class LibroController : Controller
	{
		[HttpGet]
		[Route("api/Libro")]
		public IActionResult GetAll()
		{
			ML.Libros libro = new ML.Libros();
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
	}
}
