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
    public partial class UC_Ventas : UserControl
    {
        public class DetalleVentaItem
        {
            public string IdProducto { get; set; }
            public string Descripcion { get; set; }
            public int Cantidad { get; set; }
            public decimal SubTotal { get; set; }
        }

        private BindingList<DetalleVentaItem> listaCarrito = new BindingList<DetalleVentaItem>();

        public UC_Ventas()
        {
            InitializeComponent();
        }

        private void UC_Ventas_Load(object sender, EventArgs e)
        {
            // Configuración del DataGridView
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = listaCarrito;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (dataGridView1.Columns.Contains("idVentas"))
                dataGridView1.Columns["idVentas"].DataPropertyName = "IdProducto";

            if (dataGridView1.Columns.Contains("descripcion"))
                dataGridView1.Columns["descripcion"].DataPropertyName = "Descripcion";

            if (dataGridView1.Columns.Contains("cantidad"))
                dataGridView1.Columns["cantidad"].DataPropertyName = "Cantidad";

            if (dataGridView1.Columns.Contains("subTotal"))
                dataGridView1.Columns["subTotal"].DataPropertyName = "SubTotal";

            // Enlazar evento para borrar automáticamente el contenido al tocar CUALQUIER campo
            AsignarBorradoAlTocar(textBox1); // ID Cliente
            AsignarBorradoAlTocar(textBox2); // Nombre
            AsignarBorradoAlTocar(textBox3); // DNI
            AsignarBorradoAlTocar(textBox4); // Producto ID
            AsignarBorradoAlTocar(textBox5); // Descripción
            AsignarBorradoAlTocar(textBox6); // Stock Disponible
            AsignarBorradoAlTocar(textBox7); // Precio Venta

            // Validaciones de entrada por teclado
            textBox3.KeyPress += TextBox3_KeyPress; // Solo números en DNI
            textBox2.KeyPress += TextBox2_KeyPress; // Solo letras en Nombre
        }

        // Método que borra completamente el texto al hacer clic o ganar el foco
        private void AsignarBorradoAlTocar(TextBox txt)
        {
            txt.Click += (s, e) => txt.Clear();
            txt.GotFocus += (s, e) => txt.Clear();
        }

        // Restricción a solo números para el DNI
        private void TextBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // Restricción a solo letras para Nombre
        private void TextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        #region --- BOTONES DE BÚSQUEDA ---

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Debe completar los campos para buscar cliente.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void button3_Click(object sender, EventArgs e) => MostrarMensajeBusquedaProducto();
        private void button4_Click(object sender, EventArgs e) => MostrarMensajeBusquedaProducto();

        private void MostrarMensajeBusquedaProducto()
        {
            MessageBox.Show("Debe completar el campo ID Producto o Descripción para poder buscar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Seleccione un rango de fechas válido.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

        #region --- BOTONES ABAJO (AGREGAR Y ELIMINAR) ---

        // BOTÓN VERDE AGREGAR (button5)
        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("No se puede agregar si no se encontraron productos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        // BOTÓN ROJO ELIMINAR (button6)
        private void button6_Click(object sender, EventArgs e)
        {
            MessageBox.Show("No se puede eliminar si no hay productos agregados.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        #endregion

        // Eventos vacíos para no romper la compatibilidad con el Diseñador
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void label1_Click_1(object sender, EventArgs e) { }
        private void label5_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void label6_Click(object sender, EventArgs e) { }
        private void label8_Click(object sender, EventArgs e) { }
        private void label11_Click(object sender, EventArgs e) { }
        private void label14_Click(object sender, EventArgs e) { }
        private void label17_Click(object sender, EventArgs e) { }
        private void label16_Click(object sender, EventArgs e) { }
        private void textBox8_TextChanged(object sender, EventArgs e) { }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }
    }
}