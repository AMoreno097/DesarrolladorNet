using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Autor
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.LibreriaContext context = new DL.LibreriaContext())
                {
                    var filasafectadas = context.Autors.FromSqlRaw($"AutorGetAll").ToList();
                    if (filasafectadas != null)
                    {
                        result.Objects = new List<object>();

                        foreach (var obj in filasafectadas)
                        {
                            ML.Autor autor = new ML.Autor();
                            autor.IdAutor = obj.IdAutor;
                            autor.NombreAutor = obj.NombreAutor;
                            autor.ApellidoPaterno = obj.ApellidoPaterno;
                            autor.ApellidoMaterno = obj.ApellidoMaterno;
                            autor.Pais = obj.Pais;
                            autor.FechaNacimiento = obj.FechaNacimiento;


                            result.Objects.Add(autor);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo obtener los registros";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
    }
}
