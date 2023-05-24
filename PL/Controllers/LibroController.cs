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

				var responseTask = client.GetAsync("Libro/GetAll");
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


        [HttpPost] //metodo con servicios web
        public ActionResult Form(ML.Libros libro)
        {

            ML.Result result = new ML.Result();
            //add o update
            if (libro.IdLibro == 0)
            {
                //add
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:5016/api/");

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<ML.Libros>("Libro/Add", libro);
                    postTask.Wait();

                    var resultAlumno = postTask.Result;
                    if (resultAlumno.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "Se ha insertado el Libro";
                        return PartialView("Modal");
                    }

                    else
                    {
                        ViewBag.Message = "No se inserto el Libro";
                        return PartialView("Modal");
                    }
                }

            }
            else
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:5016/api/");

                    //HTTP POST
                    var postTask = client.PutAsJsonAsync<ML.Libros>("Libro/Update/" + libro.IdLibro, libro);
                    postTask.Wait();

                    var resultAlumno = postTask.Result;
                    if (resultAlumno.IsSuccessStatusCode)
                    {
                        ViewBag.Menssaje = "Se ha actualizado el Libro";
                        return PartialView("Modal");
                    }
                }
                return PartialView("Modal");
            }
        }
    }
}
