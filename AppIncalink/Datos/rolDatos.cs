using AppIncalink.Models;
using System.Data.SqlClient;
using System.Data;
namespace AppIncalink.Datos
{
    public class rolDatos
    {
        public List<rolModel> listar()
        {
            var oLista = new List<rolModel>();
            var cn = new Conexion();
            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();

                SqlCommand cmd = new SqlCommand("ListarRol", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oLista.Add(new rolModel()
                        {
                            id = Convert.ToInt32(dr["id"]),
                            nombre = dr["nombre"].ToString()
                        });
                    }
                }
            }

            return oLista;
        }
    }
}
