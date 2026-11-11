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

namespace Proyecto_Taller_2.Presentacion
{
    public partial class FormBuscarProducto : Form
    {
        // Propiedad pública para devolver el producto seleccionado al Form principal
        public DataRow FilaProductoSeleccionada { get; private set; }

        private DataTable dtProductos;

        public FormBuscarProducto()
        {
            InitializeComponent();
        }

        private void FormBuscarProducto_Load(object sender, EventArgs e)
        {
            CargarProductos();
        }

        private void CargarProductos()
        {
            try
            {
                VentaNegocio negocio = new VentaNegocio();
                dtProductos = negocio.ObtenerProductos();

                if (dtProductos != null)
                {
                    dgvProductos.DataSource = dtProductos;
                    FormatearGrilla();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar productos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatearGrilla()
        {
            if (dgvProductos == null || dgvProductos.Columns.Count == 0) return;

            dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProductos.MultiSelect = false;
            dgvProductos.ReadOnly = true;
            dgvProductos.RowHeadersVisible = false;
            dgvProductos.AllowUserToAddRows = false;
            dgvProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Estética visual corporativa idéntico al resto del sistema
            dgvProductos.EnableHeadersVisualStyles = false;
            dgvProductos.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 40, 50);
            dgvProductos.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvProductos.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            dgvProductos.RowsDefaultCellStyle.ForeColor = Color.Black;
            dgvProductos.RowsDefaultCellStyle.BackColor = Color.White;
            dgvProductos.RowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(51, 153, 255);
            dgvProductos.RowsDefaultCellStyle.SelectionForeColor = Color.White;
            dgvProductos.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 247, 250);

            // Ocultar columnas técnicas
            string[] columnasOcultas = { "fecha_alta", "fecha_baja", "fecha_modificacion", "precio_costo", "url_imagen" };
            foreach (string col in columnasOcultas)
            {
                if (dgvProductos.Columns.Contains(col))
                    dgvProductos.Columns[col].Visible = false;
            }

            // Renombrar encabezados para que se lean de forma profesional
            if (dgvProductos.Columns.Contains("id_producto"))
            {
                dgvProductos.Columns["id_producto"].HeaderText = "ID";
                dgvProductos.Columns["id_producto"].FillWeight = 40;
            }
            if (dgvProductos.Columns.Contains("nombre"))
            {
                dgvProductos.Columns["nombre"].HeaderText = "Producto";
                dgvProductos.Columns["nombre"].FillWeight = 120;
            }
            if (dgvProductos.Columns.Contains("descripcion"))
            {
                dgvProductos.Columns["descripcion"].HeaderText = "Descripción";
                dgvProductos.Columns["descripcion"].FillWeight = 150;
            }
            if (dgvProductos.Columns.Contains("talle"))
            {
                dgvProductos.Columns["talle"].HeaderText = "Talle";
                dgvProductos.Columns["talle"].FillWeight = 50;
            }
            if (dgvProductos.Columns.Contains("stock"))
            {
                dgvProductos.Columns["stock"].HeaderText = "Stock";
                dgvProductos.Columns["stock"].FillWeight = 50;
            }
            if (dgvProductos.Columns.Contains("precio_venta"))
            {
                dgvProductos.Columns["precio_venta"].HeaderText = "Precio Unit.";
                dgvProductos.Columns["precio_venta"].FillWeight = 70;
                dgvProductos.Columns["precio_venta"].DefaultCellStyle.Format = "C2"; // Formato de moneda
            }
        }

        private void txtBuscar_TextChanged_1(object sender, EventArgs e)
        {
            AplicarFiltroProductos();
        }

        private void ConfirmarSeleccion()
        {
            if (dgvProductos.CurrentRow != null)
            {
                DataRowView drv = (DataRowView)dgvProductos.CurrentRow.DataBoundItem;
                FilaProductoSeleccionada = drv.Row;
                this.DialogResult = DialogResult.OK; // Cierra el modal indicando éxito
                this.Close();
            }
        }

        private void dgvProductos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                ConfirmarSeleccion();
            }
        }

        private void btnSeleccionar_Click_1(object sender, EventArgs e)
        {
            ConfirmarSeleccion();
        }

        private void cmbTalle_SelectedIndexChanged(object sender, EventArgs e)
        {
            AplicarFiltroProductos();
        }

        private void AplicarFiltroProductos()
        {
            if (dtProductos == null) return;

            string texto = txtBuscar.Text.Trim().Replace("'", "''");
            List<string> condiciones = new List<string>();

            // Filtro por texto (Nombre, Edición o Descripción)
            if (!string.IsNullOrEmpty(texto))
            {
                List<string> condTexto = new List<string>();

                if (dtProductos.Columns.Contains("nombre"))
                    condTexto.Add($"nombre LIKE '%{texto}%'");

                if (dtProductos.Columns.Contains("edicion"))
                    condTexto.Add($"edicion LIKE '%{texto}%'");

                if (dtProductos.Columns.Contains("descripcion"))
                    condTexto.Add($"descripcion LIKE '%{texto}%'");

                if (condTexto.Count > 0)
                {
                    condiciones.Add($"({string.Join(" OR ", condTexto)})");
                }
            }

            // Aplicar el RowFilter combinado
            DataView dv = dtProductos.DefaultView;
            dv.RowFilter = condiciones.Count > 0 ? string.Join(" AND ", condiciones) : string.Empty;
            dgvProductos.DataSource = dv;
        }
    }
}