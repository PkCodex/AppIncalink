using AppIncalink.Models;
using System.Data;
using System.Data.SqlClient;
namespace AppIncalink.Datos
{
    public class productosDatos
    {

        //Metodo Listar
        public List<productoModel> listar()
        {
            var oLista = new List<productoModel>();
            var cn = new Conexion();
            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();

                SqlCommand cmd = new SqlCommand("ListarProducto", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oLista.Add(new productoModel()
                        {
                            id = Convert.ToInt32(dr["id"]),
                            nombre = dr["nombre"].ToString(),
                            tipoMedida = dr["tipoMedida"].ToString()
                        });
                    }
                }
            }

            return oLista;
        }


        public productoModel Obtener(int id)
        {
            var oproducto = new productoModel();
            var cn = new Conexion();
            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();

                SqlCommand cmd = new SqlCommand("ObtenerProducto", conexion);
                cmd.Parameters.AddWithValue("idProducto", id);
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {
                        oproducto.id = Convert.ToInt32(dr["id"]);
                        oproducto.nombre = dr["nombre"].ToString();
                        oproducto.tipoMedida = dr["tipoMedida"].ToString();

                    }
                }
            }

            return oproducto;
        }

        //Metodo de guardar 
        public bool Guardar(productoModel oproducto)
        {
            bool rpta;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();

                    SqlCommand cmd = new SqlCommand("InsertarProducto", conexion);
                    cmd.Parameters.AddWithValue("@nombre", oproducto.nombre);
                    cmd.Parameters.AddWithValue("@tipoMedida", oproducto.tipoMedida);

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();
                }
                rpta = true;
            }
            catch (Exception e)
            {
                string error = e.Message;
                rpta = false;
            }

            return rpta;

        }

        //Metodo eliminar productos
        public bool Eliminar(int id)
        {
            bool rpta;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();

                    SqlCommand cmd = new SqlCommand("EliminarProductos", conexion);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                rpta = true;
            }
            catch (Exception e)
            {
                string error = e.Message;
                rpta = false;
            }

            return rpta;

        }
    }
}
