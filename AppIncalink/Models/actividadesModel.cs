namespace AppIncalink.Models
{
    public class actividadesModel
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public int idGrupo { get; set; }
        public DateTime fechaInicio { get; set; }
        public DateTime fechaFin { get; set; }
        public string recursos { get; set; }
        public string responsables { get; set; }
        public string lugarDesde { get; set; }
        public string LugarHacia { get; set; }
        public string observaciones { get; set; }
        public int? idTipoActividad { get; set; }
        public int? idMenu { get; set; }
        public int? idVehiculo { get; set; }
    }
}
