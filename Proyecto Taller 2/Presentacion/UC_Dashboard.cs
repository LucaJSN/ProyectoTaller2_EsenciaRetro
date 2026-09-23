using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_Taller_2
{
    public partial class UC_Dashboard : UserControl
    {
        public UC_Dashboard()
        {
            InitializeComponent();

            try
            {
                ConfigurarUIDashboard();
                CargarDatosEstaticosYCalcularMetricas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al inicializar el dashboard: " + ex.Message, "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ConfigurarUIDashboard()
        {
            if (dataGridView1 != null)
            {
                dataGridView1.ReadOnly = true;
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.AllowUserToDeleteRows = false;
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView1.MultiSelect = false;
                dataGridView1.RowHeadersVisible = false;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // Estilo visual corporativo idéntico al resto del sistema
                dataGridView1.EnableHeadersVisualStyles = false;
                dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 40, 50);
                dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

                dataGridView1.RowsDefaultCellStyle.ForeColor = Color.Black;
                dataGridView1.RowsDefaultCellStyle.BackColor = Color.White;
                dataGridView1.RowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(51, 153, 255);
                dataGridView1.RowsDefaultCellStyle.SelectionForeColor = Color.White;

                dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 247, 250);
            }
        }

        private void CargarDatosEstaticosYCalcularMetricas()
        {
            if (dataGridView1 != null)
            {
                dataGridView1.Rows.Clear();

                // Si la grilla no tiene columnas creadas en el diseñador, las creamos por seguridad
                if (dataGridView1.Columns.Count == 0)
                {
                    dataGridView1.Columns.Add("colIdVenta", "ID Venta");
                    dataGridView1.Columns.Add("colDescripcion", "Descripción Remera Retro");
                    dataGridView1.Columns.Add("colCantidad", "Cantidad");
                    dataGridView1.Columns.Add("colSubtotal", "SubTotal");
                }

                // Inserción de datos estáticos limpios de remeras retro
                dataGridView1.Rows.Add("01", "Remera Argentina '86 (Mundiales)", 1, "$ 35.000,00");
                dataGridView1.Rows.Add("02", "Remera Real Madrid (Champions)", 2, "$ 70.000,00");
                dataGridView1.Rows.Add("03", "Remera Boca Jrs (Libertadores)", 1, "$ 32.000,00");

                // Variables para las sumas automáticas
                int totalCantidad = 0;
                decimal totalRecaudacion = 0;

                // Recorremos cada fila utilizando los índices de columna (0: ID, 1: Descripción, 2: Cantidad, 3: Subtotal)
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells.Count >= 4)
                    {
                        // Sumar cantidad (Columna 2)
                        if (row.Cells[2].Value != null)
                        {
                            if (int.TryParse(row.Cells[2].Value.ToString(), out int cantidad))
                            {
                                totalCantidad += cantidad;
                            }
                        }

                        // Sumar subtotal (Columna 3)
                        if (row.Cells[3].Value != null)
                        {
                            string subTotalStr = row.Cells[3].Value.ToString()
                                .Replace("$", "")
                                .Replace(".", "")
                                .Replace(",", ".")
                                .Trim();

                            if (decimal.TryParse(subTotalStr, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal subtotal))
                            {
                                totalRecaudacion += subtotal;
                            }
                        }
                    }
                }

                // Actualizamos las etiquetas con los resultados calculados
                ActualizarTarjetasMetricas(totalRecaudacion, totalCantidad);
            }
        }

        private void ActualizarTarjetasMetricas(decimal recaudacion, int cantidad)
        {
            ActualizarEtiquetasRecursivo(this, recaudacion, cantidad);
        }

        private void ActualizarEtiquetasRecursivo(Control contenedor, decimal recaudacion, int cantidad)
        {
            foreach (Control ctrl in contenedor.Controls)
            {
                if (ctrl is Label lbl)
                {
                    // Detecta el label de recaudación
                    if (lbl.Text.Contains("$") || lbl.Text == "$ 0.00" || lbl.Text == "$0.00")
                    {
                        lbl.Text = $"$ {recaudacion:N2}";
                    }
                    // Detecta el label de cantidad de camisetas
                    else if (lbl.Text == "0")
                    {
                        lbl.Text = cantidad.ToString();
                    }
                }

                if (ctrl.HasChildren)
                {
                    ActualizarEtiquetasRecursivo(ctrl, recaudacion, cantidad);
                }
            }
        }

        private void UC_Dashboard_Load(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e) { }
        private void panel3_Paint(object sender, PaintEventArgs e) { }
        private void label5_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void panel1_Paint(object sender, PaintEventArgs e) { }
    }
}