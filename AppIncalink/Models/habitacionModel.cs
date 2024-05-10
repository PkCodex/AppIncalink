using System.ComponentModel.DataAnnotations;

namespace AppIncalink.Models
{
    public class habitacionModel
    {
        [Required(ErrorMessage = "El numero de habitación es obligatorio")]
        public int id { get; set; }
        [Required(ErrorMessage = "El numero de camas es obligatorio")]
        public int numCamas { get; set; }
    }
}
