namespace AppIncalink.Models
{
    public class ListarPersonasNombresModel
    {
        public int id { get; set; }
        public string nombreCompleto { get; set; }
        public int idSexo { get; set; }
        public string documentoId { get; set; }
        public string correo { get; set; }
        public string telefono { get; set; }
        public string alergiaAlimentacion { get; set; }
        public string alegiaVarias { get; set; }
        public string observaciones { get; set; }
        public int? idGrupo { get; set; } // Usamos int? para permitir valores nulos
        public int? idHabitacion { get; set; } // Usamos int? para permitir valores nulos
        public int? idRol { get; set; } // Usamos int? para permitir valores nulos

        public string numeroHabitacion { get; set; }
        public string nombreSexo { get; set; }
        public string nombreGrupo { get; set; }
        public string nombreRol { get; set; }
    }
}
