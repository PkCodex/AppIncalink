using AppIncalink.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AppIncalink.Datos
{
    public class habitacionDatos
    {
        //Metodo Listar
        public List<habitacionModel> listar()
        {
            var oLista = new List<habitacionModel>();
            var cn = new Conexion();
            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();

                SqlCommand cmd = new SqlCommand("ListarHabitaciones", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oLista.Add(new habitacionModel()
                        {
                            id = Convert.ToInt32(dr["id"]),
                            numCamas = Convert.ToInt32(dr["numCamas"])
                        });
                    }
                }
            }

            return oLista;
        }


        //Metodo obtener datos

        public habitacionModel Obtener(int id)
        {
            var ohabitacion = new habitacionModel();
            var cn = new Conexion();
            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();

                SqlCommand cmd = new SqlCommand("ObtenerHabitacion", conexion);
                cmd.Parameters.AddWithValue("idHabitacion", id);
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {
                        ohabitacion.id = Convert.ToInt32(dr["id"]);
                        ohabitacion.numCamas = Convert.ToInt32(dr["numCamas"]);
                    }
                }
            }

            return ohabitacion;
        }

        //Metodo de guardar 
        public bool Guardar(habitacionModel ohabitacion)
        {
            bool rpta;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();

                    SqlCommand cmd = new SqlCommand("InsertarHabitacion", conexion);
                    // No es necesario pasar el parámetro 'id' si es generado automáticamente
                    // cmd.Parameters.AddWithValue("id", ohabitacion.id);
                    cmd.Parameters.AddWithValue("@NumeroHabitacion", ohabitacion.id);
                    cmd.Parameters.AddWithValue("@NumeroCamas", ohabitacion.numCamas);
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
        public bool Editar(habitacionModel ohabitacion)
        {
            bool rpta;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();


                    SqlCommand cmd = new SqlCommand("EditarHabitacion", conexion);
                    cmd.Parameters.AddWithValue("@id", ohabitacion.id); // Asegúrate de usar @id aquí
                    cmd.Parameters.AddWithValue("@numeroHabitacion", ohabitacion.id); // Corrige el nombre del parámetro
                    cmd.Parameters.AddWithValue("@numCamas", ohabitacion.numCamas); // Mantén el nombre del parámetro
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

                    SqlCommand cmd = new SqlCommand("EliminarHabitacion", conexion);
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
