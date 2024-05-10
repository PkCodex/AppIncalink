using AppIncalink.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace AppIncalink.Datos
{
    public class personaDatos
    {

        //Metodo Listar
        public List<ListarPersonasNombresModel> listar()
        {
            var oLista = new List<ListarPersonasNombresModel>();
            var cn = new Conexion();
            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();

                SqlCommand cmd = new SqlCommand("ListarPersonasNombres", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oLista.Add(new ListarPersonasNombresModel()
                        {
                            id = Convert.ToInt32(dr["id"]),
                            nombreCompleto = dr["nombreCompleto"].ToString(),
                            documentoId = dr["documentoId"].ToString(),
                            correo = dr["correo"].ToString(),
                            telefono = dr["telefono"].ToString(),
                            alergiaAlimentacion = dr["alergiaAlimentacion"].ToString(),
                            alegiaVarias = dr["alegiaVarias"].ToString(),
                            observaciones = dr["observaciones"].ToString(),
                            numeroHabitacion = dr["numeroHabitacion"].ToString(),
                            nombreGrupo = dr["nombreGrupo"].ToString(),
                            nombreRol = dr["nombreRol"].ToString(),
                            nombreSexo = dr["nombreSexo"].ToString()
                        });
                    }
                }
            }

            return oLista;
        }


        public personaModel Obtener(int id)
        {
            var opersona = new personaModel();
            var cn = new Conexion();
            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();

                SqlCommand cmd = new SqlCommand("ObtenerPersona", conexion);
                cmd.Parameters.AddWithValue("@idPersona", id);
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {
                        opersona.id = Convert.ToInt32(dr["id"]);
                        opersona.nombreCompleto = dr["nombreCompleto"].ToString();
                        opersona.idSexo = Convert.ToInt32(dr["idSexo"]);
                        opersona.documentoId = dr["documentoId"].ToString();
                        opersona.correo = dr["correo"].ToString();
                        opersona.telefono = dr["telefono"].ToString();
                        opersona.alergiaAlimentacion = dr["alergiaAlimentacion"].ToString();
                        opersona.alegiaVarias = dr["alegiaVarias"].ToString();
                        opersona.observaciones = dr["observaciones"].ToString();
                        opersona.idGrupo = Convert.ToInt32(dr["idGrupo"]);
                        opersona.idHabitacion = Convert.ToInt32(dr["idHabitacion"]);
                        opersona.idRol = Convert.ToInt32(dr["idRol"]);
                    }
                }
            }

            return opersona;
        }


        //Metodo Guardar
        public bool Guardar(personaModel opersona)
        {
            bool rpta;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();

                    SqlCommand cmd = new SqlCommand("InsertarPersona", conexion);
                    cmd.Parameters.AddWithValue("@nombreCompleto", opersona.nombreCompleto);
                    cmd.Parameters.AddWithValue("@idSexo", opersona.idSexo);
                    cmd.Parameters.AddWithValue("@documentoId", opersona.documentoId);
                    cmd.Parameters.AddWithValue("@correo", opersona.correo);
                    cmd.Parameters.AddWithValue("@telefono", opersona.telefono);
                    cmd.Parameters.AddWithValue("@alergiaAlimentacion", opersona.alergiaAlimentacion);
                    cmd.Parameters.AddWithValue("@alegiaVarias", opersona.alegiaVarias);
                    cmd.Parameters.AddWithValue("@observaciones", opersona.observaciones);
                    cmd.Parameters.AddWithValue("@idGrupo", opersona.idGrupo ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@idhabitacion", opersona.idHabitacion ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@idRol", opersona.idRol ?? (object)DBNull.Value);
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



        //Metodo Editar
        public bool Editar(personaModel opersona)
        {
            bool rpta;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();

                    SqlCommand cmd = new SqlCommand("EditarPersona", conexion);
                    cmd.Parameters.AddWithValue("@id", opersona.id);
                    cmd.Parameters.AddWithValue("@nombreCompleto", opersona.nombreCompleto);
                    cmd.Parameters.AddWithValue("@idSexo", opersona.idSexo);
                    cmd.Parameters.AddWithValue("@documentoId", opersona.documentoId);
                    cmd.Parameters.AddWithValue("@correo", opersona.correo);
                    cmd.Parameters.AddWithValue("@telefono", opersona.telefono);
                    cmd.Parameters.AddWithValue("@alergiaAlimentacion", opersona.alergiaAlimentacion);
                    cmd.Parameters.AddWithValue("@alegiaVarias", opersona.alegiaVarias);
                    cmd.Parameters.AddWithValue("@observaciones", opersona.observaciones);
                    cmd.Parameters.AddWithValue("@idGrupo", opersona.idGrupo);
                    cmd.Parameters.AddWithValue("@idhabitacion", opersona.idHabitacion);
                    cmd.Parameters.AddWithValue("@idRol", opersona.idRol);
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

        //Metodo Eliminar
        public bool Eliminar(int id)
        {
            bool rpta;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();

                    SqlCommand cmd = new SqlCommand("EliminarPersona", conexion);
                    cmd.Parameters.AddWithValue("@idPersona", id);
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
