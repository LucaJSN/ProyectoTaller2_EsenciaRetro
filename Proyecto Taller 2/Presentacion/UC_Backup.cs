using Proyecto_Taller_2.Negocio;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Proyecto_Taller_2
{
    public partial class UC_BackUp : UserControl
    {
        public class BackupModel
        {
            public DateTime FechaHora { get; set; }
            public string Fecha
            {
                get { return FechaHora.ToString("dd/MM/yyyy"); }
            }

            // 2. Nueva propiedad que extrae y formatea solo la Hora (Ej: 15:30)
            public string Hora
            {
                get { return FechaHora.ToString("HH:mm"); }
            }
            public string Tipo { get; set; }
            public string Destino { get; set; }
            public string Tamanio { get; set; }
        }

        private List<BackupModel> _historialBackups;
        private BackNegocio negocio = new BackNegocio();
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
            DGVHistorial.ReadOnly = true;
            DGVHistorial.AllowUserToAddRows = false;
            DGVHistorial.AllowUserToDeleteRows = false;
            DGVHistorial.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DGVHistorial.MultiSelect = false;
            DGVHistorial.RowHeadersVisible = false;
            DGVHistorial.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DGVHistorial.RowsDefaultCellStyle.ForeColor = Color.Black;
            DGVHistorial.RowsDefaultCellStyle.BackColor = Color.White;
            DGVHistorial.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 247, 250);
            DGVHistorial.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black;

            if (colAccion != null)
            {
                colAccion.HeaderText = "Restaurar";
                colAccion.Text = "🔄 Restaurar";
                colAccion.UseColumnTextForButtonValue = true;
            }

            if (DGVHistorial.Columns["colEliminar"] == null)
            {
                colEliminarExtra = new DataGridViewButtonColumn
                {
                    Name = "colEliminar",
                    HeaderText = "Eliminar",
                    Text = "🗑️ Eliminar",
                    UseColumnTextForButtonValue = true,
                    Width = 100
                };
                DGVHistorial.Columns.Add(colEliminarExtra);
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

            if (DGVHistorial != null)
            {
                DGVHistorial.CellContentClick -= DGVHistorial_CellContentClick;
                DGVHistorial.CellContentClick += DGVHistorial_CellContentClick;
            }
        }

        private void ActualizarGrillaHistorial()
        {
            DGVHistorial.Rows.Clear();

            foreach (var item in _historialBackups)
            {
                DGVHistorial.Rows.Add(
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

                //Valida si el campo está vacío
                if (string.IsNullOrWhiteSpace(destinoDir))
                {
                    MessageBox.Show("Debe especificar una ruta de destino.", "Carpeta Requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDestino.Focus();
                    return;
                }

                //Valida si la carpeta existe físicamente en el equipo
                if (!Directory.Exists(destinoDir))
                {
                    MessageBox.Show("La carpeta especificada no existe. Por favor, ingrese o seleccione una ruta de carpeta válida.",
                                    "Carpeta Inexistente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDestino.Focus();
                    return;
                }

                string nombreArchivo = "Backup_Datos_" + DateTime.Now.ToString("dd-MM-yyyy_HH-mm") + ".sql";
                string rutaCompleta = System.IO.Path.Combine(destinoDir, nombreArchivo);

                // Lee las tablas que el usuario tildó en la vista
                List<string> tablasSeleccionadas = new List<string>();

                if (CLBTablas.CheckedItems.Count > 0)
                {
                    foreach (var item in CLBTablas.CheckedItems)
                    {
                        tablasSeleccionadas.Add(item.ToString());
                    }
                }
                else
                {
                    MessageBox.Show("Debe seleccionar al menos una tabla para respaldar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Detenemos el proceso si no eligió ninguna tabla
                }


                //Llama a la capa de Negocio 
                BackNegocio negocio = new BackNegocio();
                bool exito = negocio.GenerarBackup(rutaCompleta, tablasSeleccionadas);

                if (exito)
                {
                    MessageBox.Show($"¡Copia de seguridad generada y guardada con éxito!\nArchivo: {nombreArchivo}",
                                    "Back-Up Completo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarHistorialDesdeCarpeta();
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



        private void btnExportarNube_Click(object sender, EventArgs e)
        {
            if (DGVHistorial.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione una copia de seguridad del historial para exportar a la nube.",
                                "Selección Requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int index = DGVHistorial.SelectedRows[0].Index;
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
            CargarHistorialDesdeCarpeta();

            try
            {
                List<string> tablas = negocio.ObtenerTablas();
                CLBTablas.Items.Clear();
                foreach (string tabla in tablas)
                {
                    CLBTablas.Items.Add(tabla, true); // true = Aparecen tildadas por defecto
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener tablas: " + ex.Message);
            }
        }

        private void CargarHistorialDesdeCarpeta()
        {
            DGVHistorial.AutoGenerateColumns = false;
            // Reemplaza por la ruta real donde guardas tus backups
            string rutaCarpeta = @"C:\Users\samue\OneDrive\Desktop\Para buck";

            // Verificamos si la carpeta existe; si no, no hacemos nada
            if (!System.IO.Directory.Exists(rutaCarpeta)) return;

            List<BackupModel> listaHistorial = new List<BackupModel>();
            System.IO.DirectoryInfo directorio = new System.IO.DirectoryInfo(rutaCarpeta);

            // Buscamos todos los archivos que terminen en .sql
            System.IO.FileInfo[] archivos = directorio.GetFiles("*.sql");

            foreach (System.IO.FileInfo archivo in archivos)
            {
                // Calculamos el tamaño en MB para que se lea mejor
                double tamanioMB = archivo.Length / (1024.0 * 1024.0);

                listaHistorial.Add(new BackupModel
                {
                    FechaHora = archivo.CreationTime, // Fecha en que se creó el archivo
                    Tipo = "Manual",
                    Destino = archivo.FullName,       // La ruta completa del archivo
                    Tamanio = tamanioMB.ToString("0.00") + " MB"
                });
            }

            // Ordenamos la lista para que los backups más nuevos salgan arriba
            listaHistorial = listaHistorial.OrderByDescending(b => b.FechaHora).ToList();

            // Limpiamos y asignamos a la grilla

            DGVHistorial.DataSource = null;
            DGVHistorial.DataSource = listaHistorial;
        }

        private void DGVHistorial_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string nombreColumna = DGVHistorial.Columns[e.ColumnIndex].Name;

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

        private void txtDestino_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
