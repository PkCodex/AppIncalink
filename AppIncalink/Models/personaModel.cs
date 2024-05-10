using Microsoft.AspNetCore.Mvc.Rendering;
namespace AppIncalink.Models
{
    public class personaModel
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
        public int? idGrupo { get; set; }
        public int? idHabitacion { get; set; } 
        public int? idRol { get; set; } 
    }
}
