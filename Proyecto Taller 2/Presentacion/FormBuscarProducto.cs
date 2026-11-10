using Proyecto_Taller_2.Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                dtProductos = negocio.ObtenerProductos(); // Asegúrate de que traiga: id_producto, nombre, descripcion, edicion, talle, stock, precio_venta

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
            dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProductos.MultiSelect = false;
            dgvProductos.ReadOnly = true;

            // Ocultar columnas secundarias si no las quieres mostrar todas
            if (dgvProductos.Columns.Contains("fecha_alta")) dgvProductos.Columns["fecha_alta"].Visible = false;
            if (dgvProductos.Columns.Contains("fecha_baja")) dgvProductos.Columns["fecha_baja"].Visible = false;
            if (dgvProductos.Columns.Contains("fecha_modificacion")) dgvProductos.Columns["fecha_modificacion"].Visible = false;
            if (dgvProductos.Columns.Contains("precio_costo")) dgvProductos.Columns["precio_costo"].Visible = false;
            if (dgvProductos.Columns.Contains("url_imagen")) dgvProductos.Columns["url_imagen"].Visible = false;
        }

        // Evento que filtra en tiempo real mientras el usuario escribe
        private void txtBuscar_TextChanged_1(object sender, EventArgs e)
        {
            if (dtProductos != null)
            {
                string filtro = txtBuscar.Text.Trim().Replace("'", "''");

                if (string.IsNullOrEmpty(filtro))
                {
                    dtProductos.DefaultView.RowFilter = string.Empty;
                }
                else
                {
                    List<string> condiciones = new List<string>();

                    // Validar que las columnas existan en dtProductos antes de filtrar por ellas
                    if (dtProductos.Columns.Contains("nombre"))
                        condiciones.Add($"nombre LIKE '%{filtro}%'");

                    if (dtProductos.Columns.Contains("edicion"))
                        condiciones.Add($"edicion LIKE '%{filtro}%'");

                    if (dtProductos.Columns.Contains("talle"))
                        condiciones.Add($"talle LIKE '%{filtro}%'");

                    if (dtProductos.Columns.Contains("descripcion"))
                        condiciones.Add($"descripcion LIKE '%{filtro}%'");

                    DataView dv = dtProductos.DefaultView;
                    dv.RowFilter = string.Join(" OR ", condiciones);
                    dgvProductos.DataSource = dv;
                }
            }
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



        // Doble clic en la tabla para seleccionar rápido
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

            // 1. Filtro por texto (Nombre, Edición o Descripción)
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

            // 3. Aplicar el RowFilter combinado
            DataView dv = dtProductos.DefaultView;
            dv.RowFilter = condiciones.Count > 0 ? string.Join(" AND ", condiciones) : string.Empty;
            dgvProductos.DataSource = dv;
        }
    }
}
