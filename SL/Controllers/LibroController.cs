using Microsoft.AspNetCore.Mvc;


//using System.Web.Http;

namespace SL.Controllers
{
    public class LibroController : Controller
	{
		//[HttpGet]
		//[Route("api/Libro/GetAll")]
		//public IActionResult GetAll()
		//{
		//	ML.Libros libro = new ML.Libros();
		//	ML.Result result = BL.Libro.GetAll(libro);

		//	if (result.Correct)
		//	{
		//		return Ok(result);
		//	}
		//	else
		//	{
		//		return NotFound();
		//	}
		//}


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
                return NotFound(result);
            }
        }
    }
}
