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
        public UC_Productos()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DGVProductos.Rows.Add(
                TBNombre.Text,
                TBDescripcion.Text,
                NUDCosto.Value,
                NUDVenta.Value,
                NUDCantidad.Value
            );

            LimpiarCampos();
        }

        private void LimpiarCampos()
        {
            TBNombre.Clear();
            TBDescripcion.Clear();
            TBNombre.Focus();
        }

        private void UC_Productos_Load(object sender, EventArgs e)
        {

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

        private void BtnImagen_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog archD = new OpenFileDialog())
            {
                archD.Title = "Seleccionar Imagen";

                archD.Filter = "Archivos de imagen|*.jpg;*.jpeg;*.png;*.bmp;*.gif|Todos los archivos|*.*";

                if (archD.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image = Image.FromFile(archD.FileName);

                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
        }

        private void BCancelar_Click(object sender, EventArgs e)
        {

        }

        private void BtnConexion_Click(object sender, EventArgs e)
        {
            string connectionString =
            "Server=localhost;Port=3306;Database=esencia-base;User=root;Password=123456;";

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
    }
}
