using System.ComponentModel.DataAnnotations;

namespace AppIncalink.Models
{
    public class grupoModel
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public DateTime fechaLlegada { get; set; }
        public DateTime fechaSalida { get; set; }
        public string informacionVuelo { get; set; }
        public string intinerarioVuelo { get; set; }
        public string observaciones { get; set; }
    }
}
