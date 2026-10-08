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
        
        private static string cadenaConexion = "Server=localhost;Port=3306;Database=esenciaretro2.0;Uid=root;Pwd=puntoybarraroot;";

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

            string query = @"SELECT u.id_usuario, u.nombre, u.apellido, u.correo, u.password, u.telefono, u.id_rol, u.fecha_baja, r.tipo 
                             FROM Usuario u 
                             INNER JOIN Rol r ON u.id_rol = r.id_rol 
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
                            if (reader["fecha_baja"] != DBNull.Value)
                            {
                                throw new Exception("Tu cuenta ha sido desactivada. Contacta al administrador.");
                            }
                            usuario = new Usuario
                            {
                                id_usuario = Convert.ToInt32(reader["id_usuario"]),
                                nombre = reader["nombre"].ToString(),
                                apellido = reader["apellido"].ToString(),
                                correo = reader["correo"].ToString(),
                                password = reader["password"].ToString(),
                                telefono = reader["telefono"].ToString(),
                                id_rol = Convert.ToInt32(reader["id_rol"]),
                                rol = new Rol
                                {
                                    id_rol = Convert.ToInt32(reader["id_rol"]),
                                    tipo = reader["tipo"].ToString()
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