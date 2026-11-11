using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Proyecto_Taller_2.Negocio;

namespace Proyecto_Taller_2
{
    public partial class UC_Dashboard : UserControl
    {
        private DashboardNegocio negocio = new DashboardNegocio();

        public UC_Dashboard()
        {
            InitializeComponent();

            try
            {
                ConfigurarUIDashboard();
                CargarDatosDesdeBaseDeDatos();
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

                // Mapeamos las columnas existentes del diseñador para que no se dupliquen
                dataGridView1.AutoGenerateColumns = false;
                if (idVenta != null) idVenta.DataPropertyName = "ID Venta";
                if (descripcion != null) descripcion.DataPropertyName = "Nombre";
                if (cantidad != null) cantidad.DataPropertyName = "Cantidad";
                if (Column2 != null) Column2.DataPropertyName = "Cliente";
                if (Column1 != null) Column1.DataPropertyName = "Metodo de Pago";
                if (Column3 != null) Column3.DataPropertyName = "Sub Total";

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

        private void CargarDatosDesdeBaseDeDatos()
        {
            try
            {
                // 1. Obtenemos las métricas reales del día
                decimal recaudacion = negocio.ObtenerRecaudacionDelDia();
                int camisetas = negocio.ObtenerCamisetasVendidasDelDia();

                // 2. Actualizamos las tarjetas métricas superiores
                ActualizarTarjetasMetricas(recaudacion, camisetas);

                // 3. Cargamos el DataGridView usando el mapeo de columnas
                if (dataGridView1 != null)
                {
                    DataTable dtVentas = negocio.ObtenerVentasDelDiaParaGrilla();
                    dataGridView1.DataSource = dtVentas;

                    // Formato de moneda para la columna Sub Total
                    if (dataGridView1.Columns.Contains("Column3") && dataGridView1.Columns["Column3"] != null)
                    {
                        dataGridView1.Columns["Column3"].DefaultCellStyle.Format = "C2";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos del dashboard desde la base de datos: " + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    if (lbl.Text.Contains("$") || lbl.Text == "$ 0.00" || lbl.Text == "$0.00")
                    {
                        lbl.Text = $"$ {recaudacion:N2}";
                    }
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