using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

namespace Proyecto_Taller_2.Datos
{
    internal class RolDatos
    {
        private string connectionString = "Server=localhost;Port=33060;Database=esenciaretro;Uid=root;Pwd=puntoybarraroot;";

        List<Rol> listaRoles = new List<Rol>();

        public List<Rol> ObtenerRoles()
        {
            using (MySqlConnection conexion = new MySqlConnection(connectionString))
            {
                string query = "SELECT id_rol, tipo FROM rol";
                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    conexion.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read()) // Mientras haya filas por leer...
                        {
                            Rol unRol = new Rol();
                            unRol.id_rol = Convert.ToInt32(reader["id_rol"]);
                            unRol.tipo = reader["tipo"].ToString();
                            listaRoles.Add(unRol); // Agregamos el rol a la lista
                        }
                    }
                }
            }
            return listaRoles;
        }
    }
}
