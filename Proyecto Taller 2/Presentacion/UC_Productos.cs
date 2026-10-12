using MySqlConnector;
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
    public partial class UC_Productos : UserControl
    {
        private BindingList<Producto> listaProductos = new BindingList<Producto>();
        private int contadorId = 1;
        public UC_Productos()
        {
            InitializeComponent();

            DGVProductos.DataSource = listaProductos;

            AgregarColumnaBotonEliminar();
        }

        private void AgregarColumnaBotonEliminar()
        {
            // Verificar si no fue creada previamente
            if (!DGVProductos.Columns.Contains("btnColumnaEliminar"))
            {
                DataGridViewButtonColumn btnEliminar = new DataGridViewButtonColumn();
                btnEliminar.Name = "btnColumnaEliminar";
                btnEliminar.HeaderText = "Eliminar";
                btnEliminar.Text = "Eliminar";
                btnEliminar.UseColumnTextForButtonValue = true;

                DGVProductos.Columns.Add(btnEliminar);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TBNombre.Text) || string.IsNullOrWhiteSpace(TBDescripcion.Text) && string.IsNullOrWhiteSpace(CBTalle.Text) || string.IsNullOrWhiteSpace(CBCategoria.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Campos requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TBNombre.Focus();
                return;
            }

            Producto nuevoProducto = new Producto
            {
                IdProducto = contadorId++,
                Nombre = TBNombre.Text.Trim(),
                Descripcion = TBDescripcion.Text.Trim(),
                Talle = CBTalle.Text.Trim(),
                Categoria = CBCategoria.Text.Trim(),
                PrecioCosto = NUDCosto.Value,
                PrecioVenta = NUDVenta.Value,
                Cantidad = (int)NUDCantidad.Value,
                RutaImagen = pictureBox1.ImageLocation ?? string.Empty,
                Activo = true
            };

            listaProductos.Add(nuevoProducto);
            LimpiarFormulario();
        }

        private void DGVProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Corrección en el índice de la columna del botón de eliminar
            if (e.RowIndex >= 0 && DGVProductos.Columns[e.ColumnIndex].Name == "btnColumnaEliminar")
            {
                Producto productoSeleccionado = (Producto)DGVProductos.Rows[e.RowIndex].DataBoundItem;

                DialogResult confirmacion = MessageBox.Show(
                    $"¿Estás seguro de que querés eliminar el producto '{productoSeleccionado.Nombre}'?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (confirmacion == DialogResult.Yes)
                {
                    productoSeleccionado.Activo = false;
                    productoSeleccionado.FechaBaja = DateTime.Now;
                    DGVProductos.Refresh();
                }
            }
        }


        private void LimpiarFormulario()
        {
            TBNombre.Clear();
            TBDescripcion.Clear();
            NUDCosto.Value = 0;
            NUDVenta.Value = 0;
            NUDCantidad.Value = 0;
            pictureBox1.Image = null;
            pictureBox1.ImageLocation = null;
            TBNombre.Focus();
        }

        private void UC_Productos_Load(object sender, EventArgs e)
        {
            // Configuración de estilos y colores para asegurar legibilidad en el DataGridView
            DGVProductos.EnableHeadersVisualStyles = false;
            DGVProductos.BackgroundColor = Color.White;
            DGVProductos.ForeColor = Color.FromArgb(44, 62, 80); // Gris oscuro corporativo

            // Estilo para las celdas de datos
            DGVProductos.DefaultCellStyle.BackColor = Color.White;
            DGVProductos.DefaultCellStyle.ForeColor = Color.FromArgb(44, 62, 80);
            DGVProductos.DefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 152, 219);
            DGVProductos.DefaultCellStyle.SelectionForeColor = Color.White;

            // Estilo para la cabecera (Headers)
            DGVProductos.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94);
            DGVProductos.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            DGVProductos.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);

            // Filas alternadas para mayor prolijidad visual
            DGVProductos.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
        }

        private void LPrecio_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void TBNombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void TBNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != (char)Keys.Space)
            {
                e.Handled = true;
            }
        }

        private void TBDescripcion_TextChanged(object sender, EventArgs e)
        {

        }

        private void TBDescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != (char)Keys.Space)
            {
                e.Handled = true;
            }
        }

        private void TBCosto_TextChanged(object sender, EventArgs e)
        {

        }

        private void TBCosto_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != (char)Keys.Delete)
            {
                if (e.KeyChar == ',' || e.KeyChar == '.')
                {
                    e.KeyChar = ',';

                    if (textBox.Text.Contains(",") || textBox.SelectionStart == 0)
                    {
                        e.Handled = true;
                    }
                }
                else
                {
                    e.Handled = true;
                }
            }
        }

        private void TBVenta_TextChanged(object sender, EventArgs e)
        {

        }

        private string rutaImagenSeleccionada = string.Empty;
        private void BtnImagen_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Archivos de Imagen|*.jpg;*.jpeg;*.png;*.bmp";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    rutaImagenSeleccionada = ofd.FileName;
                    pictureBox1.ImageLocation = rutaImagenSeleccionada;
                }
            }
        }

        private void BCancelar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void BtnConexion_Click(object sender, EventArgs e)
        {
            string connectionString =
            "Server=127.0.0.1;Port=3306;Database=esenciaretro;Uid=root;Pwd=puntoybarraroot;";

            try
            {
                using (MySqlConnection conexion = new MySqlConnection(connectionString))
                {
                    conexion.Open();
                    MessageBox.Show("¡Conexión exitosa!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de conexión: " + ex.Message);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}