using Proyecto_Taller_2.Entidades;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Taller_2.Datos
{
    internal class ProvinciaDatos
    {
        public List<Provincia> ObtenerProvincias()
        {
            List<Provincia> lista = new List<Provincia>();
            string query = "SELECT id_provincia, nombre FROM provincia ORDER BY nombre ASC";

            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                conexion.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Provincia
                            {
                                id_provincia = Convert.ToInt32(dr["id_provincia"]),
                                nombre = dr["nombre"].ToString()
                            });
                        }
                    }
                }
            }
            return lista;
        }
    }
}
