using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_Taller_2.Datos
{
    internal class BackDatos
    {
        // Nota: Cuando pases definitivamente a la nube, recuerda cambiar esta línea por la de Railway
        private string connectionString = "Server=localhost;Port=3306;Database=esenciaretro2.0;Uid=root;Pwd=puntoybarraroot;";

        // 1. NUEVO: Obtener todas las tablas dinámicamente
        public List<string> ObtenerTablas()
        {
            List<string> tablas = new List<string>();
            try
            {
                using (MySqlConnection conexion = new MySqlConnection(connectionString))
                {
                    string query = "SHOW TABLES;";
                    using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                    {
                        conexion.Open();
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Agregamos el nombre de cada tabla a la lista
                                tablas.Add(reader.GetString(0));
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return tablas;
        }

        // 2. MODIFICADO: Agregamos la lista de tablas seleccionadas
        public bool GenerarBackup(string rutaArchivo, List<string> tablasSeleccionadas)
        {
            try
            {
                using (MySqlConnection conexion = new MySqlConnection(connectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        using (MySqlBackup mb = new MySqlBackup(cmd))
                        {
                            cmd.Connection = conexion;
                            conexion.Open();

                            // Si la lista de tablas tiene elementos, configuramos el backup parcial
                            // Si está vacía o es nula, hará el backup completo por defecto
                            // Si la lista de tablas tiene elementos, configuramos el backup parcial
                            if (tablasSeleccionadas != null && tablasSeleccionadas.Count > 0)
                            {
                                Dictionary<string, string> diccionarioTablas = new Dictionary<string, string>();

                                foreach (string tabla in tablasSeleccionadas)
                                {
                                    // Le indicamos a la librería que de cada tabla seleccionada haga un SELECT completo
                                    diccionarioTablas.Add(tabla, $"SELECT * FROM `{tabla}`;");
                                }

                                mb.ExportInfo.TablesToBeExportedDic = diccionarioTablas;
                            }

                            mb.ExportToFile(rutaArchivo);
                            conexion.Close();
                        }
                    }
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // 3. NUEVO: Método para Restaurar la base de datos
        public bool RestaurarBackup(string rutaArchivo)
        {
            try
            {
                using (MySqlConnection conexion = new MySqlConnection(connectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        using (MySqlBackup mb = new MySqlBackup(cmd))
                        {
                            cmd.Connection = conexion;
                            conexion.Open();

                            // Importamos el archivo .sql directamente a la base de datos
                            mb.ImportFromFile(rutaArchivo);

                            conexion.Close();
                        }
                    }
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}