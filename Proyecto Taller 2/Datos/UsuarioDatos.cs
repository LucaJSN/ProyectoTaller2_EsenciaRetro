using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Taller_2.Datos
{
    public class UsuarioDatos
    {
        // La conexión se guarda EXCLUSIVAMENTE en esta capa
        private string connectionString = "Server=localhost;Port=33060;Database=esenciaretro;Uid=root;Pwd=puntoybarraroot;";

        // Recibe las dos entidades ya cargadas con los datos desde la vista
        public bool InsertarUsuarioYDireccion(Usuario usuario, Direccion direccion)
        {
            using (MySqlConnection conexion = new MySqlConnection(connectionString))
            {
                conexion.Open();

                using (MySqlTransaction transaccion = conexion.BeginTransaction())
                {
                    try
                    {
                        // --- PASO 1: INSERTAR DIRECCIÓN ---
                        string queryDir = @"INSERT INTO direccion (provincia, ciudad, calle, altura) 
                                            VALUES (@prov, @ciud, @calle, @alt);
                                            SELECT LAST_INSERT_ID();";

                        using (MySqlCommand cmdDir = new MySqlCommand(queryDir, conexion, transaccion))
                        {
                            cmdDir.Parameters.AddWithValue("@prov", direccion.Provincia);
                            cmdDir.Parameters.AddWithValue("@ciud", direccion.Ciudad);
                            cmdDir.Parameters.AddWithValue("@calle", direccion.Calle);
                            cmdDir.Parameters.AddWithValue("@alt", direccion.Altura);

                            // Capturamos el ID generado y se lo asignamos a la entidad Usuario
                            usuario.DireccionId = Convert.ToInt32(cmdDir.ExecuteScalar());
                        }

                        // --- PASO 2: INSERTAR USUARIO ---
                        string queryUsu = @"INSERT INTO usuario (nombre, apellido, correo, password, telefono, Direccion_id, Rol_id) 
                                            VALUES (@nom, @ape, @correo, @pass, @tel, @dirId, @rolId);";

                        using (MySqlCommand cmdUsu = new MySqlCommand(queryUsu, conexion, transaccion))
                        {
                            cmdUsu.Parameters.AddWithValue("@nom", usuario.Nombre);
                            cmdUsu.Parameters.AddWithValue("@ape", usuario.Apellido);
                            cmdUsu.Parameters.AddWithValue("@correo", usuario.Correo);
                            cmdUsu.Parameters.AddWithValue("@pass", usuario.Password);
                            cmdUsu.Parameters.AddWithValue("@tel", usuario.Telefono);
                            cmdUsu.Parameters.AddWithValue("@dirId", usuario.DireccionId);
                            cmdUsu.Parameters.AddWithValue("@rolId", usuario.rol_id);

                            cmdUsu.ExecuteNonQuery();
                        }

                        transaccion.Commit();
                        return true; // Todo salió bien
                    }
                    catch (Exception)
                    {
                        transaccion.Rollback();
                        throw; // ¡Importante! Lanzamos el error para que la capa de Presentación lo atrape y muestre el MessageBox
                    }
                }
            }
        }
        public bool DarDeBajaUsuario(int idUsuario)
        {
            using (MySqlConnection conexion = new MySqlConnection(connectionString))
            {
                string query = "UPDATE usuario SET fecha_baja = NOW() WHERE IdUsuario = @id";

                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@id", idUsuario);
                    conexion.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
            }
        }
        public List<Usuario> ObtenerTodosLosUsuarios()
        {
            List<Usuario> lista = new List<Usuario>();

            using (MySqlConnection conexion = new MySqlConnection(connectionString))
            {
                // Traemos todos los usuarios, sin filtrar por fecha_baja
                string query = "SELECT id_usuario, nombre, apellido, correo, telefono, Rol_id, fecha_baja FROM usuario";

                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    conexion.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Usuario usu = new Usuario
                            {
                                IdUsuario = Convert.ToInt32(reader["IdUsuario"]),
                                Nombre = reader["nombre"].ToString(),
                                Apellido = reader["apellido"].ToString(),
                                Correo = reader["correo"].ToString(),
                                Telefono = reader["telefono"].ToString(),
                                rol_id = Convert.ToInt32(reader["Rol_id"]),

                                // Si en la base de datos es NULL, en C# se guarda como null, de lo contrario se convierte a DateTime
                                FechaBaja = reader["fecha_baja"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["fecha_baja"])
                            };
                            lista.Add(usu);
                        }
                    }
                }
            }
            return lista;
        }
    }
}
