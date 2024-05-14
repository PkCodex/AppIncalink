namespace AppIncalink.Models
{
    public class recetasProductosModel
    {
        public int id { get; set; }
        public int idMenu { get; set; }
        public List<ProductoCantidad> Productos { get; set; }
    }

    public class ProductoCantidad
    {
        public int idProducto { get; set; }
        public float cantidad { get; set; }
    }
}
