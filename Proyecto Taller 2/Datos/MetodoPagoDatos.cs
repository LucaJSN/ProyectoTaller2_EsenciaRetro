using System;
using System.Collections.Generic;
using MySqlConnector;
using Proyecto_Taller_2.Entidades;

namespace Proyecto_Taller_2.Datos
{
    public class MetodoPagoDatos
    {
        public List<MetodoPago> ObtenerMetodosPago()
        {
            List<MetodoPago> lista = new List<MetodoPago>();

            // Usa la conexión centralizada y pública de la clase Conexion
            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                try
                {
                    conexion.Open();
                    string query = "SELECT id_metodo_pago, nombre FROM metodo_pago";

                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                lista.Add(new MetodoPago()
                                {
                                    IdMetodoPago = Convert.ToInt32(reader["id_metodo_pago"]),
                                    Nombre = reader["nombre"].ToString()
                                });
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    lista = new List<MetodoPago>();
                }
            }
            return lista;
        }
    }
}