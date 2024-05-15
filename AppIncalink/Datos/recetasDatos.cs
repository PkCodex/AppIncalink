using AppIncalink.Datos;
using AppIncalink.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AppIncalink.Datos
{
    public class recetasDatos
    {
        //Metodo Listar
        public List<recetasModel> listar()
        {
            var oLista = new List<recetasModel>();
            var cn = new Conexion();
            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();

                SqlCommand cmd = new SqlCommand("ListarRecetas", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oLista.Add(new recetasModel()
                        {
                            id = Convert.ToInt32(dr["id"]),
                            idMenu = Convert.ToInt32(dr["idMenu"]),
                            idProducto = Convert.ToInt32(dr["idProducto"]),
                            cantidad = Convert.ToSingle(dr["cantidad"])
                        });
                    }
                }
            }

            return oLista;
        }

        //Metodo Obtener 
        public recetasModel Obtener(int id)
        {
            var orecetas = new recetasModel();
            var cn = new Conexion();
            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();

                SqlCommand cmd = new SqlCommand("ObtenerRecetas", conexion);
                cmd.Parameters.AddWithValue("idRecetas", id);
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {
                        orecetas.id = Convert.ToInt32(dr["id"]);
                        orecetas.idMenu = Convert.ToInt32(dr["idMenu"]);
                        orecetas.idProducto = Convert.ToInt32(dr["idProducto"]);
                        orecetas.cantidad = Convert.ToSingle(dr["cantidad"]);
                    }
                }
            }

            return orecetas;
        }
        //Metodo Guardar
        public bool Guardar(recetasModel orecetas)
        {
            bool rpta;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();

                    SqlCommand cmd = new SqlCommand("InsertarRecetas", conexion);
                    cmd.Parameters.AddWithValue("@idMenu", orecetas.idMenu);
                    cmd.Parameters.AddWithValue("@idProducto", orecetas.idProducto);
                    cmd.Parameters.AddWithValue("@cantidad", orecetas.cantidad);
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

        // Recetas para el listado del reporte

        public List<recetasModel> ObtenerProductosPorReceta(int idReceta)
        {
            var productos = new List<recetasModel>();
            var cn = new Conexion();
            using (var connection = new SqlConnection(cn.getCadenaSQL()))
            {
                connection.Open();

                string query = "SELECT idProducto, cantidad FROM RecetaProducto WHERE idReceta = @IdReceta";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdReceta", idReceta);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var receta = new recetasModel
                            {
                                idProducto = Convert.ToInt32(reader["idProducto"]),
                                cantidad = Convert.ToSingle(reader["cantidad"])
                            };

                            productos.Add(receta);
                        }
                    }
                }
            }

            return productos;
        }
    }
}