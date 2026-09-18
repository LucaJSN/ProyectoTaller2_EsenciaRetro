using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Proyecto_Taller_2.Negocio;

namespace Proyecto_Taller_2
{
    public partial class UC_Backup : UserControl
    {
        public UC_Backup()
        {
            InitializeComponent();
        }

        private void UC_Backup_Load(object sender, EventArgs e)
        {

        }

        private void btnGenerarBackup_Click(object sender, EventArgs e)
        {
            // 1. Manejamos la interfaz gráfica (las ventanitas)
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Archivos de Backup (*.sql)|*.sql|Archivos BACK (*.back)|*.back";
            sfd.Title = "Guardar copia de seguridad";
            sfd.FileName = "Backup_Usuarios_" + DateTime.Now.ToString("dd-MM-yyyy") + ".sql";

            // 2. Si el usuario elige dónde guardar...
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // 3. Le pasamos la ruta elegida a la capa de Negocio
                    BackNegocio negocio = new BackNegocio();
                    bool exito = negocio.GenerarBackup(sfd.FileName); // sfd.FileName tiene la ruta completa

                    if (exito)
                    {
                        MessageBox.Show("Backup generado y descargado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrió un error al generar el backup: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
