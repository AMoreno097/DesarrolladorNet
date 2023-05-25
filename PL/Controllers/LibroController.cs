using Microsoft.AspNetCore.Mvc;


namespace PL.Controllers
{
    public class LibroController : Controller
	{

        private readonly IConfiguration _configuration;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        public LibroController(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }


        [HttpPost]

        public ActionResult GetAll(ML.Libros libro)
        {

            ML.Result result = BL.Libro.GetAll(libro);
            libro.Libro= result.Objects;

            ML.Result resultEditoriales = BL.Editorial.GetAll();
            libro.Editorial = new ML.Editorial();
            libro.Editorial.Editorials = resultEditoriales.Objects;

            ML.Result resultAutores = BL.Autor.GetAll();
            libro.Autor = new ML.Autor();
            libro.Autor.Autors = resultAutores.Objects;


            return View(libro);
            //return View(resultAlumnos);

        }
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
                client.BaseAddress = new Uri(_configuration["WebApi"]);

                //client.BaseAddress = new Uri(urlApi);

                var responseTask = client.GetAsync("Libro/GetAll/Titulo/Fecha/IdAutor/IdEditorial");
                responseTask.Wait(); //esperar a que se resuelva la llamada al servicio

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    foreach (var resultItem in readTask.Result.Objects)
                    {
                        ML.Libros resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Libros>(resultItem.ToString());
                        resultLibros.Libro.Add(resultItemList);
                    }

                }
            }
            ML.Libros libro = new ML.Libros();
            libro.Libro = resultLibros.Libro;

            ML.Result resultEditoriales = BL.Editorial.GetAll();
            libro.Editorial = new ML.Editorial();
            libro.Editorial.Editorials = resultEditoriales.Objects;

            ML.Result resultAutores = BL.Autor.GetAll();
            libro.Autor = new ML.Autor();
            libro.Autor.Autors = resultAutores.Objects;


            return View(libro);
            //return View(resultAlumnos);

        }
        [HttpGet]
        public ActionResult Form(int? IdLibro)
        {
            ML.Result resultAutor = BL.Autor.GetAll();
            ML.Result resultEditorial = BL.Editorial.GetAll();

            ML.Libros libros= new ML.Libros();

            libros.Autor = new ML.Autor();
            libros.Editorial = new ML.Editorial();
           

            if (resultAutor.Correct)
            {
                libros.Autor.Autors = resultAutor.Objects;
                libros.Editorial.Editorials = resultEditorial.Objects;
            }

            if (IdLibro == null)
            {
                return View(libros);
            }
            else
            {
                //ML.Result result = BL.Producto.GetById(IdProducto.Value);
                ML.Result result = new ML.Result();
                try
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(_configuration["WebApi"]);
                        var responseTask = client.GetAsync("Producto/GetById/" + IdLibro);
                        responseTask.Wait();
                        var resultAPI = responseTask.Result;
                        if (resultAPI.IsSuccessStatusCode)
                        {
                            var readTask = resultAPI.Content.ReadAsAsync<ML.Result>();
                            readTask.Wait();
                            ML.Libros resultItemList = new ML.Libros();
                            resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Libros>(readTask.Result.Object.ToString());
                            result.Object = resultItemList;

                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No existen registros en la tabla Departamento";
                        }
                    }

                    if (result.Correct)
                    {

                        libros = (ML.Libros)result.Object;

                        //ML.Result resultDepartamento = BL.Libro.GetByIdArea(producto.Departamento.Area.IdArea);

                        //producto.Proveedor.Proveedores = resultProveedor.Objects;
                        //producto.Departamento.Area.Areas = resultArea.Objects;
                        //producto.Departamento.Departamentos = resultDepartamento.Objects;
                        //return View(producto);

                    }
                    else
                    {
                        ViewBag.Message = "Ocurrio un error al hacer la consulta del usuario " + result.ErrorMessage;
                        return View("Modal");
                    }
                }
                catch
                {

                }
                return View();
            }
        }
        [HttpPost] //metodo con servicios web
        public ActionResult Form(ML.Libros libro)
        {
            if (ModelState.IsValid)
            {
                IFormFile file = Request.Form.Files["inpImagen"];

                if (file != null)
                {
                    
                    libro.Portada = Convert.ToBase64String(ConvertToBytes(file));

                }
            }
            ML.Result result = new ML.Result();
            //add o update
            if (libro.IdLibro == 0)
            {
                //add
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration["WebApi"]);

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


            //return View(); 
        }
        public static byte[] ConvertToBytes(IFormFile imagen)
        {
            using var fileStream = imagen.OpenReadStream();
            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)fileStream.Length);

            return bytes;
        }
    }
}
