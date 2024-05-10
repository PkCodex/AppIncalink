using AppIncalink.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AppIncalink.Datos
{
    public class actividadesDatos
    {
        //Metodo Listar
        public List<actividadesModel> listar()
        {
            var oLista = new List<actividadesModel>();
            var cn = new Conexion();
            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();

                SqlCommand cmd = new SqlCommand("ListarActividades", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oLista.Add(new actividadesModel()
                        {
                            id = dr["id"] != DBNull.Value ? Convert.ToInt32(dr["id"]) : 0,
                            nombre = dr["nombre"] != DBNull.Value ? dr["nombre"].ToString() : string.Empty,
                            idGrupo = dr["idGrupo"] != DBNull.Value ? Convert.ToInt32(dr["idGrupo"]) : 0,
                            fechaInicio = dr["fechaInicio"] != DBNull.Value ? Convert.ToDateTime(dr["fechaInicio"]) : DateTime.MinValue,
                            fechaFin = dr["fechaFin"] != DBNull.Value ? Convert.ToDateTime(dr["fechaFin"]) : DateTime.MinValue,
                            recursos = dr["recursos"] != DBNull.Value ? dr["recursos"].ToString() : string.Empty,
                            responsables = dr["responsable"] != DBNull.Value ? dr["responsable"].ToString() : string.Empty,
                            lugarDesde = dr["lugarDesde"] != DBNull.Value ? dr["lugarDesde"].ToString() : string.Empty,
                            LugarHacia = dr["LugarHacia"] != DBNull.Value ? dr["LugarHacia"].ToString() : string.Empty,
                            observaciones = dr["observaciones"] != DBNull.Value ? dr["observaciones"].ToString() : string.Empty,
                            idTipoActividad = dr["idTipoActividad"] != DBNull.Value ? Convert.ToInt32(dr["idTipoActividad"]) : 0,
                            idMenu = dr["idMenu"] != DBNull.Value ? Convert.ToInt32(dr["idMenu"]) : 0,
                            idVehiculo = dr["idVehiculo"] != DBNull.Value ? Convert.ToInt32(dr["idVehiculo"]) : 0
                        });
                    }
                }
            }

            return oLista;
        }

        public actividadesModel Obtener(int id)
        {
            var oactividades = new actividadesModel();
            var cn = new Conexion();
            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();

                SqlCommand cmd = new SqlCommand("ObtenerActividad", conexion);
                cmd.Parameters.AddWithValue("@idActividad", id);
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {
                        oactividades.id = Convert.ToInt32(dr["id"]);
                        oactividades.nombre = dr["nombre"].ToString();
                        oactividades.idGrupo = Convert.ToInt32(dr["idGrupo"]);
                        oactividades.fechaInicio = Convert.ToDateTime(dr["fechaInicio"]);
                        oactividades.fechaFin = Convert.ToDateTime(dr["fechaFin"]);
                        oactividades.recursos = dr["recursos"].ToString();
                        oactividades.responsables = dr["responsable"].ToString();
                        oactividades.lugarDesde = dr["lugarDesde"].ToString();
                        oactividades.LugarHacia = dr["LugarHacia"].ToString();
                        oactividades.observaciones = dr["observaciones"].ToString();
                        oactividades.idTipoActividad = DBNull.Value.Equals(dr["idTipoActividad"]) ? 0 : Convert.ToInt32(dr["idTipoActividad"]);
                        oactividades.idMenu = DBNull.Value.Equals(dr["idMenu"]) ? 0 : Convert.ToInt32(dr["idMenu"]);
                        oactividades.idVehiculo = DBNull.Value.Equals(dr["idVehiculo"]) ? 0 : Convert.ToInt32(dr["idVehiculo"]);

                    }
                }
            }

            return oactividades;
        }


        //Metodo Guardar
        public bool Guardar(actividadesModel oactividades)
        {
            bool rpta;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();

                    SqlCommand cmd = new SqlCommand("InsertarActividad", conexion);
                    cmd.Parameters.AddWithValue("@nombre", oactividades.nombre);
                    cmd.Parameters.AddWithValue("@idGrupo", oactividades.idGrupo);
                    cmd.Parameters.AddWithValue("@fechaInicio", oactividades.fechaInicio);
                    cmd.Parameters.AddWithValue("@fechaFin", oactividades.fechaFin);
                    cmd.Parameters.AddWithValue("@recursos", oactividades.recursos);
                    cmd.Parameters.AddWithValue("@responsable", oactividades.responsables);
                    cmd.Parameters.AddWithValue("@lugarDesde", oactividades.lugarDesde);
                    cmd.Parameters.AddWithValue("@lugarHacia", oactividades.LugarHacia);
                    cmd.Parameters.AddWithValue("@observaciones", oactividades.observaciones);
                    cmd.Parameters.AddWithValue("@idTipoActividad", oactividades.idTipoActividad ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@idMenu", oactividades.idMenu ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@idVehiculo", oactividades.idVehiculo ?? (object)DBNull.Value);
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

                    // Verificar si la actividad con el ID proporcionado existe antes de eliminarla
                    SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM actividades WHERE id = @id", conexion);
                    checkCmd.Parameters.AddWithValue("@id", id);
                    int count = (int)checkCmd.ExecuteScalar();

                    if (count > 0)
                    {
                        // La actividad existe, proceder con la eliminación
                        SqlCommand cmd = new SqlCommand("EliminarActividades", conexion);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.ExecuteNonQuery();
                        rpta = true;
                    }
                    else
                    {
                        // La actividad con el ID proporcionado no existe, establecer rpta como falso
                        rpta = false;
                    }
                }
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
