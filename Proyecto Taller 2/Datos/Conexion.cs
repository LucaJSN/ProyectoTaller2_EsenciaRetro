using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Taller_2
{
    public static class Conexion
    {
        // Ajustá con los datos de tu servidor MariaDB (puerto por defecto 3306)
        private static string cadenaConexion = "Server=localhost;Port=33060;Database=esenciaretro;Uid=root;Pwd=puntoybarraroot;";

        public static MySqlConnection ObtenerConexion()
        {
            return new MySqlConnection(cadenaConexion);
        }
    }

    public class UsuarioDAO
    {
        public Usuario ValidarUsuario(string correo, string contrasena)
        {
            Usuario usuario = null;

            string query = @"SELECT u.id_usuario, u.nombre, u.apellido, u.correo, u.password, u.telefono, u.Rol_id, r.tipo 
                             FROM Usuario u 
                             INNER JOIN Rol r ON u.Rol_id = r.id_rol 
                             WHERE u.correo = @correo AND u.password = @password";

            using (MySqlConnection conn = Conexion.ObtenerConexion())
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@correo", correo);
                    cmd.Parameters.AddWithValue("@password", contrasena);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            usuario = new Usuario
                            {
                                IdUsuario = Convert.ToInt32(reader["id_usuario"]),
                                Nombre = reader["nombre"].ToString(),
                                Apellido = reader["apellido"].ToString(),
                                Correo = reader["correo"].ToString(),
                                Password = reader["password"].ToString(),
                                Telefono = reader["telefono"].ToString(),
                                RolId = Convert.ToInt32(reader["Rol_id"]),
                                Rol = new Rol
                                {
                                    IdRol = Convert.ToInt32(reader["Rol_id"]),
                                    Tipo = reader["tipo"].ToString()
                                }
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al conectar con la base de datos: " + ex.Message);
                }
            }

            return usuario; // Retorna null si las credenciales son incorrectas
        }
    }
}
