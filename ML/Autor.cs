namespace ML
{
    public class Autor

    {
        public int IdAutor { get; set; }
        public string NombreAutor { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Pais { get; set; }
        public string FechaNacimiento { get; set; }

		public List<object> Autors { get; set; }
	}
}