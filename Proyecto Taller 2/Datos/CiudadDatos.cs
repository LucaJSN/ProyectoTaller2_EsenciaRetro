using Google.Protobuf.Collections;
using MySqlConnector;
using Proyecto_Taller_2.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Taller_2.Datos
{
    internal class CiudadDatos
    {
        private string connectionString = "Server=localhost;Port=33060;Database=esenciaretro2.0;Uid=root;Pwd=puntoybarraroot;"; //Luca
        //private string connectionString = "Server=localhost;Port=3306;Database=esenciaretro2.0;Uid=root;Pwd=puntoybarraroot;";//Samu

        public List<Ciudad> ListarCiudadesPorProvincia(int idProvincia)
        {
            List<Ciudad> lista = new List<Ciudad>();

            using (MySqlConnection conexion = new MySqlConnection(connectionString))
            {
                string consulta = @"
                SELECT id_ciudad, nombre, id_provincia
                FROM ciudad
                WHERE id_provincia = @idProvincia
                ORDER BY nombre";

                using (MySqlCommand comando = new MySqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@idProvincia", idProvincia);

                    conexion.Open();

                    using (MySqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Ciudad ciudad = new Ciudad
                            {
                                id_ciudad = Convert.ToInt32(reader["id_ciudad"]),
                                nombre = reader["nombre"].ToString(),
                                id_provincia = Convert.ToInt32(reader["id_provincia"])
                            };

                            lista.Add(ciudad);
                        }
                    }
                }
            }

            return lista;
        }

        public Ciudad consultarCiudad(int idCiud)
        {
            Ciudad ciudad = null;

            using (MySqlConnection conexion = new MySqlConnection(connectionString))
            {
                string consulta = @"
            SELECT *
            FROM ciudad
            WHERE id_ciudad = @idCiud";

                using (MySqlCommand comando = new MySqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@idCiud", idCiud);

                    conexion.Open();

                    using (MySqlDataReader reader = comando.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            ciudad = new Ciudad
                            {
                                id_ciudad = Convert.ToInt32(reader["id_ciudad"]),
                                nombre = reader["nombre"].ToString(),
                                id_provincia = Convert.ToInt32(reader["id_provincia"])
                            };
                        }
                    }
                }
            }
            return ciudad;
        }
    }
}

