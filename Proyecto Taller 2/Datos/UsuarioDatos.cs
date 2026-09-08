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
        private string connectionString = "Server=localhost;Database=TU_BASE_DE_DATOS;Uid=root;Pwd=;";

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
    }
}
