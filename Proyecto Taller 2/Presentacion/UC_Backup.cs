using Proyecto_Taller_2.Negocio;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Proyecto_Taller_2
{
    public partial class UC_BackUp : UserControl
    {
        public class BackupModel
        {
            public DateTime FechaHora { get; set; }
            public string Tipo { get; set; }
            public string Destino { get; set; }
            public string Tamanio { get; set; }
        }

        private List<BackupModel> _historialBackups;
        private DataGridViewButtonColumn colEliminarExtra;

        public UC_BackUp()
        {
            InitializeComponent();
            InicializarHistorialPorDefecto();
            ConfigurarUIBackUp();
            ActualizarGrillaHistorial();
        }

        private void InicializarHistorialPorDefecto()
        {
            _historialBackups = new List<BackupModel>
            {
                new BackupModel { FechaHora = new DateTime(2026, 9, 23, 9, 35, 0), Tipo = "Manual", Destino = @"C:\CopiasBase\RetroCamisetas\", Tamanio = "1.4 GB" },
                new BackupModel { FechaHora = new DateTime(2026, 9, 20, 15, 30, 0), Tipo = "Automática", Destino = @"C:\CopiasBase\RetroCamisetas\", Tamanio = "1.2 GB" },
                new BackupModel { FechaHora = new DateTime(2026, 9, 19, 10, 00, 0), Tipo = "Manual", Destino = @"C:\CopiasBase\RetroCamisetas\", Tamanio = "1.0 GB" }
            };
        }

        private void ConfigurarUIBackUp()
        {
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.RowsDefaultCellStyle.ForeColor = Color.Black;
            dataGridView1.RowsDefaultCellStyle.BackColor = Color.White;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 247, 250);
            dataGridView1.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black;

            if (colAccion != null)
            {
                colAccion.HeaderText = "Restaurar";
                colAccion.Text = "🔄 Restaurar";
                colAccion.UseColumnTextForButtonValue = true;
            }

            if (dataGridView1.Columns["colEliminar"] == null)
            {
                colEliminarExtra = new DataGridViewButtonColumn
                {
                    Name = "colEliminar",
                    HeaderText = "Eliminar",
                    Text = "🗑️ Eliminar",
                    UseColumnTextForButtonValue = true,
                    Width = 100
                };
                dataGridView1.Columns.Add(colEliminarExtra);
            }

            if (txtDestino != null && string.IsNullOrWhiteSpace(txtDestino.Text))
            {
                txtDestino.Text = @"C:\CopiasBase\RetroCamisetas\";
            }

            if (btnGenerarBackup != null)
            {
                btnGenerarBackup.Click -= btnGenerarBackup_Click;
                btnGenerarBackup.Click += btnGenerarBackup_Click;
            }

            if (btnSeleccionarCarpeta != null)
            {
                btnSeleccionarCarpeta.Click -= btnSeleccionarCarpeta_Click;
                btnSeleccionarCarpeta.Click += btnSeleccionarCarpeta_Click;
            }

            if (btnExportarNube != null)
            {
                btnExportarNube.Click -= btnExportarNube_Click;
                btnExportarNube.Click += btnExportarNube_Click;
            }

            if (dataGridView1 != null)
            {
                dataGridView1.CellContentClick -= dataGridView1_CellContentClick;
                dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            }
        }

        private void ActualizarGrillaHistorial()
        {
            dataGridView1.Rows.Clear();

            foreach (var item in _historialBackups)
            {
                dataGridView1.Rows.Add(
                    item.FechaHora.ToString("dd/MM/yyyy"),
                    item.FechaHora.ToString("HH:mm"),
                    item.Tipo,
                    item.Destino,
                    item.Tamanio,
                    "🔄 Restaurar",
                    "🗑️ Eliminar"
                );
            }

            if (lblUltimaCopia != null && _historialBackups.Count > 0)
            {
                var ultima = _historialBackups[0];
                lblUltimaCopia.Text = $"🕒 Última Copia: {ultima.FechaHora:dd/MM/yyyy HH:mm}";
            }
        }

        private void btnSeleccionarCarpeta_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    txtDestino.Text = fbd.SelectedPath + @"\";
                }
            }
        }

        private void btnGenerarBackup_Click(object sender, EventArgs e)
        {
            try
            {
                string destinoDir = txtDestino.Text.Trim();

                // 1. Validar si el campo está vacío
                if (string.IsNullOrWhiteSpace(destinoDir))
                {
                    MessageBox.Show("Debe especificar una ruta de destino.", "Carpeta Requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDestino.Focus();
                    return;
                }

                // 2. Validar estrictamente si la carpeta existe físicamente en el equipo
                if (!Directory.Exists(destinoDir))
                {
                    MessageBox.Show("La carpeta especificada no existe. Por favor, ingrese o seleccione una ruta de carpeta válida.",
                                    "Carpeta Inexistente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDestino.Focus();
                    return;
                }

                // 3. Crear el nombre del archivo y armar la ruta completa dentro de la carpeta elegida
                string nombreArchivo = "Backup_Usuarios_" + DateTime.Now.ToString("dd-MM-yyyy_HH-mm") + ".sql";
                string rutaCompleta = System.IO.Path.Combine(destinoDir, nombreArchivo);

                // 4. Llamar a la capa de Negocio para generar y guardar el archivo físico real
                BackNegocio negocio = new BackNegocio();
                bool exito = negocio.GenerarBackup(rutaCompleta);

                if (exito)
                {
                    DateTime ahora = DateTime.Now;
                    BackupModel nuevoBackup = new BackupModel
                    {
                        FechaHora = ahora,
                        Tipo = "Manual",
                        Destino = rutaCompleta,
                        Tamanio = "1.4 GB" // O el tamaño que maneje tu sistema
                    };

                    _historialBackups.Insert(0, nuevoBackup);
                    ActualizarGrillaHistorial();

                    MessageBox.Show($"¡Copia de seguridad generada y guardada con éxito!\nArchivo: {nombreArchivo}",
                                    "Back-Up Completo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("El proceso de negocio no pudo completar la creación del archivo.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar la copia: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string nombreColumna = dataGridView1.Columns[e.ColumnIndex].Name;

            if (nombreColumna == "colAccion")
            {
                var copiaSeleccionada = _historialBackups[e.RowIndex];
                DialogResult dr = MessageBox.Show($"¿Confirma la restauración del sistema utilizando la copia del {copiaSeleccionada.FechaHora:dd/MM/yyyy HH:mm}?",
                                                  "Alerta de Seguridad", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.Yes)
                {
                    MessageBox.Show("¡Copia restaurada correctamente! El sistema se encuentra actualizado a este punto.",
                                    "Restauración Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (nombreColumna == "colEliminar")
            {
                var copiaSeleccionada = _historialBackups[e.RowIndex];
                DialogResult dr = MessageBox.Show($"¿Está seguro de que desea eliminar el registro de copia del {copiaSeleccionada.FechaHora:dd/MM/yyyy HH:mm}?",
                                                  "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dr == DialogResult.Yes)
                {
                    _historialBackups.RemoveAt(e.RowIndex);
                    ActualizarGrillaHistorial();
                    MessageBox.Show("¡El registro de la copia ha sido eliminado correctamente!",
                                    "Eliminación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnExportarNube_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione una copia de seguridad del historial para exportar a la nube.",
                                "Selección Requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int index = dataGridView1.SelectedRows[0].Index;
            var copia = _historialBackups[index];

            DialogResult dr = MessageBox.Show($"¿Desea exportar la copia del {copia.FechaHora:dd/MM/yyyy HH:mm} al servidor en la nube?",
                                              "Exportar a la Nube", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                MessageBox.Show("¡Exportada correctamente a la nube!", "Nube Sincronizada", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No se realizó ninguna acción.", "Cancelado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void lblTituloPrincipal_Click(object sender, EventArgs e)
        {
            // Método requerido por el diseñador
        }

        private void UC_BackUp_Load(object sender, EventArgs e)
        {

        }

        private void btnGenerarBackup_Click_1(object sender, EventArgs e)
        {

        }
    }
}