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
        private string connectionString = "Server=localhost;Port=3306;Database=esenciaretro;Uid=root;Pwd=puntoybarraroot;";

        public bool GenerarBackup(string rutaArchivo)
        {
            try
            {
                // Usamos tu connectionString de siempre, la que ya funciona en esta clase
                using (MySqlConnection conexion = new MySqlConnection(connectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        // Recuerda tener instalado MySqlBackup.NET.MySqlConnector
                        using (MySqlBackup mb = new MySqlBackup(cmd))
                        {
                            cmd.Connection = conexion;
                            conexion.Open();

                            // Exportamos a la ruta que nos pasaron desde la vista
                            mb.ExportToFile(rutaArchivo);

                            conexion.Close();
                        }
                    }
                }
                return true; // Retornamos éxito
            }
            catch (Exception)
            {
                throw; // Si hay error, lo lanzamos para que la vista lo atrape
            }
        }
    }
}
