using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
	public class LibroController : Controller
	{
		[HttpGet]
		public ActionResult GetAll()
		{
			//ML.Result resulta = new ML.Result();
			//resulta.Objects = new List<object>();

			ML.Libros resultLibros = new ML.Libros();
			resultLibros.Libro = new List<object>();

			//string urlApi = System.Configuration.ConfigurationManager.AppSettings["WebApi"];

			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri("https://localhost:5016/api/");
				//client.BaseAddress = new Uri(urlApi);

				var responseTask = client.GetAsync("Libro");
				responseTask.Wait(); //esperar a que se resuelva la llamada al servicio

				var result = responseTask.Result;

				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<ML.Result>();
					readTask.Wait();

					foreach (var resultItem in readTask.Result.Objects)
					{
						//ML.Producto resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Producto>(resultItem.ToString());
						//resulta.Objects.Add(resultItemList);


						ML.Libros resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Libros>(resultItem.ToString());
						resultLibros.Libro.Add(resultItemList);
					}
				}
			}
			ML.Libros libro = new ML.Libros();
			libro.Libro = resultLibros.Libro;
			return View(libro);
			//return View(resultAlumnos);

		}
	}
}
