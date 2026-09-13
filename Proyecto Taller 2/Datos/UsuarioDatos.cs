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
                            cmdUsu.Parameters.AddWithValue("@rolId", usuario.RolId);

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
                            cmdDir.Parameters.AddWithValue("@prov", direccion.Provincia);
                            cmdDir.Parameters.AddWithValue("@ciud", direccion.Ciudad);
                            cmdDir.Parameters.AddWithValue("@calle", direccion.Calle);
                            cmdDir.Parameters.AddWithValue("@alt", direccion.Altura);
                            cmdDir.Parameters.AddWithValue("@dirId", usuario.DireccionId); // El ID de la dirección de ese usuario

                            cmdDir.ExecuteNonQuery();
                        }

                        // --- PASO 2: ACTUALIZAR USUARIO ---
                        string queryUsu = @"UPDATE usuario 
                                    SET nombre = @nom, apellido = @ape, correo = @correo, password = @pass, telefono = @tel, Rol_id = @rolId 
                                    WHERE id_usuario = @idUsu;";

                        using (MySqlCommand cmdUsu = new MySqlCommand(queryUsu, conexion, transaccion))
                        {
                            cmdUsu.Parameters.AddWithValue("@nom", usuario.Nombre);
                            cmdUsu.Parameters.AddWithValue("@ape", usuario.Apellido);
                            cmdUsu.Parameters.AddWithValue("@correo", usuario.Correo);
                            cmdUsu.Parameters.AddWithValue("@tel", usuario.Telefono);
                            cmdUsu.Parameters.AddWithValue("@rolId", usuario.RolId);
                            cmdUsu.Parameters.AddWithValue("@idUsu", usuario.IdUsuario);
                            cmdUsu.Parameters.AddWithValue("@pass", usuario.Password);

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
        public List<Usuario> ObtenerTodosLosUsuarios()
        {
            List<Usuario> lista = new List<Usuario>();

            using (MySqlConnection conexion = new MySqlConnection(connectionString))
            {
                // 1. Agregamos 'u.password' al SELECT y usamos LEFT JOIN por mayor seguridad
                string query = @"SELECT u.id_usuario, u.nombre, u.apellido, u.correo, u.password, u.telefono, u.Rol_id, u.fecha_baja, 
                                u.fecha_alta, u.fecha_modificacion,
                                d.provincia, d.ciudad, d.calle, d.altura 
                         FROM usuario u
                         LEFT JOIN direccion d ON u.Direccion_id = d.id_direccion";

                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    conexion.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Usuario usu = new Usuario
                            {
                                IdUsuario = Convert.ToInt32(reader["id_usuario"]),
                                Nombre = reader["nombre"].ToString(),
                                Apellido = reader["apellido"].ToString(),
                                Correo = reader["correo"].ToString(),
                                Password = reader["password"].ToString(), // <-- AQUÍ CARGAMOS LA CONTRASEÑA
                                Telefono = reader["telefono"].ToString(),
                                RolId = Convert.ToInt32(reader["Rol_id"]),
                                FechaBaja = reader["fecha_baja"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["fecha_baja"]),
                                FechaAlta = Convert.ToDateTime(reader["fecha_alta"]),
                                FechaModificacion = Convert.ToDateTime(reader["fecha_modificacion"])
                            };

                            // 2. Validamos que la dirección no sea nula en la BD antes de crear el objeto
                            if (reader["provincia"] != DBNull.Value)
                            {
                                usu.Direccion = new Direccion
                                {
                                    Provincia = reader["provincia"].ToString(),
                                    Ciudad = reader["ciudad"].ToString(),
                                    Calle = reader["calle"].ToString(),
                                    Altura = Convert.ToInt32(reader["altura"])
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
