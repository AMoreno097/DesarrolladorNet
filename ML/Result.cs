using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
	public class Result
	{
		public bool Correct { get; set; }  //La consulta fue exitosa
		public string ErrorMessage { get; set; } //Mensaje de error
		public Exception Ex { get; set; }
		public object Object { get; set; }  //guardar 1 dato
		public List<object> Objects { get; set; } //Guardar N datos

	}
}
