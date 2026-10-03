using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Taller_2.Datos
{
    public class UsuarioDatos
    {
        // La conexión se guarda EXCLUSIVAMENTE en esta capa
        private string connectionString = "Server=localhost;Port=33060;Database=esenciaretro2.0;Uid=root;Pwd=puntoybarraroot;";

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
                            cmdDir.Parameters.AddWithValue("@prov", direccion.provincia);
                            cmdDir.Parameters.AddWithValue("@ciud", direccion.ciudad);
                            cmdDir.Parameters.AddWithValue("@calle", direccion.calle);
                            cmdDir.Parameters.AddWithValue("@alt", direccion.altura);

                            // Capturamos el ID generado y se lo asignamos a la entidad Usuario
                            usuario.id_direccion = Convert.ToInt32(cmdDir.ExecuteScalar());
                        }

                        // --- PASO 2: INSERTAR USUARIO ---
                        string queryUsu = @"INSERT INTO usuario (nombre, apellido, correo, password, telefono, id_direccion, id_rol) 
                                            VALUES (@nom, @ape, @correo, @pass, @tel, @dirId, @rolId);";

                        using (MySqlCommand cmdUsu = new MySqlCommand(queryUsu, conexion, transaccion))
                        {
                            cmdUsu.Parameters.AddWithValue("@nom", usuario.nombre);
                            cmdUsu.Parameters.AddWithValue("@ape", usuario.apellido);
                            cmdUsu.Parameters.AddWithValue("@correo", usuario.correo);
                            cmdUsu.Parameters.AddWithValue("@pass", usuario.password);
                            cmdUsu.Parameters.AddWithValue("@tel", usuario.telefono);
                            cmdUsu.Parameters.AddWithValue("@dirId", usuario.id_direccion);
                            cmdUsu.Parameters.AddWithValue("@rolId", usuario.id_rol);

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

        // Le agregamos idUsuarioAExcluir = 0 para que por defecto no excluya a nadie (ideal para cuando registras uno nuevo)
        public bool ExisteCorreo(string correo, int idUsuarioAExcluir = 0)
        {
            // Agregamos la condición AND id_usuario != @Id
            string query = "SELECT COUNT(*) FROM usuario WHERE correo = @Correo AND id_usuario != @Id";

            using (MySqlConnection conexion = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@Correo", correo);
                    cmd.Parameters.AddWithValue("@Id", idUsuarioAExcluir); // Pasamos el ID a la consulta

                    conexion.Open();
                    int cantidad = Convert.ToInt32(cmd.ExecuteScalar());
                    return cantidad > 0;
                }
            }
        }

        public bool ActualizarUsuarioYDireccion(Usuario usuario, Direccion direccion)
        {
            using (MySqlConnection conexion = new MySqlConnection(connectionString))
            {
                conexion.Open();
                using (MySqlTransaction transaccion = conexion.BeginTransaction())
                {
                    try
                    {
                        // --- PASO 1: ACTUALIZAR DIRECCIÓN ---
                        string queryDir = @"UPDATE direccion 
                                    SET provincia = @prov, ciudad = @ciud, calle = @calle, altura = @alt 
                                    WHERE id_direccion = @dirId;";

                        using (MySqlCommand cmdDir = new MySqlCommand(queryDir, conexion, transaccion))
                        {
                            cmdDir.Parameters.AddWithValue("@prov", direccion.provincia);
                            cmdDir.Parameters.AddWithValue("@ciud", direccion.ciudad);
                            cmdDir.Parameters.AddWithValue("@calle", direccion.calle);
                            cmdDir.Parameters.AddWithValue("@alt", direccion.altura);
                            cmdDir.Parameters.AddWithValue("@dirId", usuario.id_direccion); // El ID de la dirección de ese usuario

                            cmdDir.ExecuteNonQuery();
                        }

                        // --- PASO 2: ACTUALIZAR USUARIO ---
                        string queryUsu = @"UPDATE usuario 
                                    SET nombre = @nom, apellido = @ape, correo = @correo, password = @pass, telefono = @tel, id_rol = @rolId 
                                    WHERE id_usuario = @idUsu;";

                        using (MySqlCommand cmdUsu = new MySqlCommand(queryUsu, conexion, transaccion))
                        {
                            cmdUsu.Parameters.AddWithValue("@nom", usuario.nombre);
                            cmdUsu.Parameters.AddWithValue("@ape", usuario.apellido);
                            cmdUsu.Parameters.AddWithValue("@correo", usuario.correo);
                            cmdUsu.Parameters.AddWithValue("@tel", usuario.telefono);
                            cmdUsu.Parameters.AddWithValue("@rolId", usuario.id_rol);
                            cmdUsu.Parameters.AddWithValue("@idUsu", usuario.id_usuario);
                            cmdUsu.Parameters.AddWithValue("@pass", usuario.password);

                            cmdUsu.ExecuteNonQuery();
                        }

                        transaccion.Commit();
                        return true;
                    }
                    catch (Exception)
                    {
                        transaccion.Rollback();
                        throw;
                    }
                }
            }
        }
        public bool DarDeBajaUsuario(int idUsuario)
        {
            using (MySqlConnection conexion = new MySqlConnection(connectionString))
            {
                string query = "UPDATE usuario SET fecha_baja = NOW() WHERE id_usuario = @id";

                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@id", idUsuario);
                    conexion.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
            }
        }

        public void ActivarUsuario(int idUsuario)
        {
            using (MySqlConnection conexion = new MySqlConnection(connectionString))
            {
                // El UPDATE simplemente pone la fecha_baja en NULL
                string query = "UPDATE usuario SET fecha_baja = NULL WHERE id_usuario = @idUsu;";

                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@idUsu", idUsuario);

                    conexion.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public List<Usuario> ObtenerTodosLosUsuarios()
        {
            List<Usuario> lista = new List<Usuario>();

            using (MySqlConnection conexion = new MySqlConnection(connectionString))
            {
                // 1. Agregamos 'u.password' al SELECT y usamos LEFT JOIN por mayor seguridad
                string query = @"SELECT u.id_usuario, u.nombre, u.apellido, u.correo, u.password, u.telefono, u.id_rol, u.fecha_baja, 
                                u.fecha_alta, u.fecha_modificacion,
                                d.id_direccion, d.provincia, d.ciudad, d.calle, d.altura 
                         FROM usuario u
                         LEFT JOIN direccion d ON u.id_direccion = d.id_direccion";

                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    conexion.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Usuario usu = new Usuario
                            {
                                id_usuario = Convert.ToInt32(reader["id_usuario"]),
                                nombre = reader["nombre"].ToString(),
                                apellido = reader["apellido"].ToString(),
                                correo = reader["correo"].ToString(),
                                password = reader["password"].ToString(), // <-- AQUÍ CARGAMOS LA CONTRASEÑA
                                telefono = reader["telefono"].ToString(),
                                id_rol= Convert.ToInt32(reader["id_rol"]),
                                fecha_baja = reader["fecha_baja"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["fecha_baja"]),
                                fecha_alta = Convert.ToDateTime(reader["fecha_alta"]),
                                fecha_modificacion = Convert.ToDateTime(reader["fecha_modificacion"]),
                                id_direccion = reader["id_direccion"] == DBNull.Value ? 0 : Convert.ToInt32(reader["id_direccion"])
                            };

                            // 2. Validamos que la dirección no sea nula en la BD antes de crear el objeto
                            if (reader["provincia"] != DBNull.Value)
                            {
                                usu.direccion = new Direccion
                                {
                                    id_direccion = Convert.ToInt32(reader["id_direccion"]),
                                    provincia = reader["provincia"].ToString(),
                                    ciudad = reader["ciudad"].ToString(),
                                    calle = reader["calle"].ToString(),
                                    altura = Convert.ToInt32(reader["altura"])
                                };
                            }

                            lista.Add(usu);
                        }
                    }
                }
            }
            return lista;
        }
    }
}
