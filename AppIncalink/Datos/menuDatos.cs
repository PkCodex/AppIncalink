using AppIncalink.Models;
using System.Data;
using System.Data.SqlClient;
namespace AppIncalink.Datos
{
    public class menuDatos
    {
        //Metodo Listar
        public List<menuModel> listar()
        {
            var oLista = new List<menuModel>();
            var cn = new Conexion();
            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();

                SqlCommand cmd = new SqlCommand("ListarMenu", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oLista.Add(new menuModel()
                        {
                            id = Convert.ToInt32(dr["id"]),
                            nombre = dr["nombre"].ToString(),                       
                        });
                    }
                }
            }

            return oLista;
        }

        public menuModel Obtener(int id)
        {
            var omenu = new menuModel();
            var cn = new Conexion();
            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();

                SqlCommand cmd = new SqlCommand("ObtenerMenu", conexion);
                cmd.Parameters.AddWithValue("idMenu", id);
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {
                        omenu.id = Convert.ToInt32(dr["id"]);
                        omenu.nombre = dr["nombre"].ToString();

                    }
                }
            }

            return omenu;
        }

        //Metodo de guardar 
        public bool Guardar(menuModel omenu)
        {
            bool rpta;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();

                    SqlCommand cmd = new SqlCommand("InsertarMenu", conexion);
                    cmd.Parameters.AddWithValue("@Nombre", omenu.nombre);

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

        //Metodo editar 
        public bool Editar(menuModel omenu)
        {
            bool rpta;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();


                    SqlCommand cmd = new SqlCommand("EditarMenu", conexion);
                    cmd.Parameters.AddWithValue("@id", omenu.id); // Asegúrate de usar @id aquí
                    cmd.Parameters.AddWithValue("@nombre", omenu.nombre); // Corrige el nombre del parámetro
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

        //Metodo eliminar
        public bool Eliminar(int id)
        {
            bool rpta;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();

                    SqlCommand cmd = new SqlCommand("EliminarMenu", conexion);
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
