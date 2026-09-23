using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
            public decimal PrecioUnitario { get; set; }
            public decimal SubTotal => Cantidad * PrecioUnitario;
        }

        private BindingList<DetalleVentaItem> listaCarrito = new BindingList<DetalleVentaItem>();

        public UC_Ventas()
        {
            InitializeComponent();
        }

        private void UC_Ventas_Load(object sender, EventArgs e)
        {
            ConfigurarGrid();
            ConfigurarComboBoxCantidad();
            VincularEventosYValidaciones();
            ActualizarTotalVenta();
        }

        private void ConfigurarComboBoxCantidad()
        {
            if (comboBox1 != null)
            {
                comboBox1.Items.Clear();
                comboBox1.Items.AddRange(new object[] { "1", "2", "3", "4", "5" });
                comboBox1.DropDownStyle = ComboBoxStyle.DropDown;
                comboBox1.Text = "1";
            }
        }

        private void ConfigurarGrid()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = listaCarrito;

            // Bloqueo total de escritura manual y filas nuevas
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.RowHeadersVisible = false;

            // Fondo blanco para la zona vacía de la grilla
            dataGridView1.BackgroundColor = Color.White;

            // Estilo visual corporativo y fondo blanco en las celdas
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 40, 50);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            dataGridView1.RowsDefaultCellStyle.ForeColor = Color.Black;
            dataGridView1.RowsDefaultCellStyle.BackColor = Color.White;
            dataGridView1.RowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(51, 153, 255);
            dataGridView1.RowsDefaultCellStyle.SelectionForeColor = Color.White;

            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 247, 250);

            if (dataGridView1.Columns.Count >= 4)
            {
                dataGridView1.Columns[0].DataPropertyName = "IdProducto";
                dataGridView1.Columns[1].DataPropertyName = "Descripcion";
                dataGridView1.Columns[2].DataPropertyName = "Cantidad";
                dataGridView1.Columns[3].DataPropertyName = "SubTotal";
            }

            // Centrar el icono del tachito de eliminar en su respectiva columna si existe
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                if (col.Name.ToLower().Contains("eliminar") || col.HeaderText.ToLower().Contains("eliminar"))
                {
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }

            dataGridView1.CellClick -= DataGridView1_CellClick;
            dataGridView1.CellClick += DataGridView1_CellClick;
        }

        private void VincularEventosYValidaciones()
        {
            // Vinculación de Botones
            if (button1 != null) { button1.Click -= ExecBuscarCliente; button1.Click += ExecBuscarCliente; }
            if (button2 != null) { button2.Click -= ExecBuscarProducto; button2.Click += ExecBuscarProducto; }
            if (button4 != null) { button4.Click -= ExecAgregarProducto; button4.Click += ExecAgregarProducto; }
            if (button5 != null) { button5.Click -= ExecRegistrarVenta; button5.Click += ExecRegistrarVenta; }

            // Restricciones de teclado
            if (textBox1 != null) AsignarSoloNumeros(textBox1);   // ID Cliente
            if (textBox2 != null) AsignarSoloLetras(textBox2);    // Nombre Cliente
            if (textBox3 != null) AsignarSoloNumeros(textBox3);   // DNI Cliente

            if (textBox5 != null) AsignarSoloNumeros(textBox5);   // Producto ID
            if (textBox4 != null) AsignarSoloLetras(textBox4);    // Descripción
            if (textBox6 != null) AsignarSoloNumeros(textBox6);   // Stock
            if (textBox7 != null)                                 // Precio Venta
            {
                textBox7.ReadOnly = false;
                AsignarSoloDecimales(textBox7);
            }

            if (comboBox1 != null)
            {
                comboBox1.KeyPress -= SoloNumeros_KeyPress;
                comboBox1.KeyPress += SoloNumeros_KeyPress;
            }

            // Limpieza de placeholder al hacer click o entrar a las cajas de texto
            TextBox[] todosLosTextBox = { textBox1, textBox2, textBox3, textBox4, textBox5, textBox6, textBox7 };
            foreach (var txt in todosLosTextBox)
            {
                if (txt != null)
                {
                    txt.Click -= LimpiarPlaceHolder;
                    txt.Click += LimpiarPlaceHolder;
                    txt.Enter -= LimpiarPlaceHolder;
                    txt.Enter += LimpiarPlaceHolder;
                }
            }
        }

        #region --- REGLAS DE TECLADO Y LIMPIEZA ---

        private void AsignarSoloNumeros(TextBox txt)
        {
            txt.KeyPress -= SoloNumeros_KeyPress;
            txt.KeyPress += SoloNumeros_KeyPress;
        }

        private void AsignarSoloLetras(TextBox txt)
        {
            txt.KeyPress -= SoloLetras_KeyPress;
            txt.KeyPress += SoloLetras_KeyPress;
        }

        private void AsignarSoloDecimales(TextBox txt)
        {
            txt.KeyPress -= SoloDecimales_KeyPress;
            txt.KeyPress += SoloDecimales_KeyPress;
        }

        private void SoloNumeros_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void SoloLetras_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void SoloDecimales_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            if ((e.KeyChar == ',' || e.KeyChar == '.') && sender is TextBox txt)
            {
                if (txt.Text.Contains(",") || txt.Text.Contains("."))
                {
                    e.Handled = true;
                }
            }
        }

        private void LimpiarPlaceHolder(object sender, EventArgs e)
        {
            if (sender is TextBox txt)
            {
                string val = txt.Text.Trim().ToLower();
                if (val.Contains("id") || val.Contains("nombre") || val.Contains("dni") || val.Contains("produ") || val.Contains("descrip") || val.Contains("noescribir") || val.Contains("$"))
                {
                    txt.Clear();
                }
                else
                {
                    txt.SelectAll();
                }
            }
        }

        #endregion

        #region --- OPERACIONES ---

        private void ExecBuscarCliente(object sender, EventArgs e)
        {
            string idCliente = textBox1 != null ? textBox1.Text.Trim() : "";

            if (string.IsNullOrWhiteSpace(idCliente) || idCliente.ToLower().Contains("id"))
            {
                MessageBox.Show("Ingrese un ID de cliente válido para buscar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show($"Cliente con ID {idCliente} buscado correctamente.", "Buscar Cliente", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ExecBuscarProducto(object sender, EventArgs e)
        {
            string idProd = textBox5 != null ? textBox5.Text.Trim() : "";

            if (string.IsNullOrWhiteSpace(idProd) || idProd.ToLower().Contains("id") || idProd.ToLower().Contains("prod"))
            {
                MessageBox.Show("Ingrese un ID de producto válido para buscar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show($"Producto buscado (ID: {idProd}).", "Producto Buscado", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ExecAgregarProducto(object sender, EventArgs e)
        {
            string idProd = textBox5 != null ? textBox5.Text.Trim() : "";
            string desc = textBox4 != null ? textBox4.Text.Trim() : "";
            string stockRaw = textBox6 != null ? textBox6.Text.Trim() : "0";
            string precioRaw = textBox7 != null ? textBox7.Text.Trim() : "0";

            if (string.IsNullOrWhiteSpace(desc) || desc.ToLower().Contains("descrip"))
            {
                MessageBox.Show("Complete la descripción del producto antes de agregar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int cantSeleccionada = 1;
            if (comboBox1 != null && !string.IsNullOrWhiteSpace(comboBox1.Text))
            {
                int.TryParse(comboBox1.Text.Trim(), out cantSeleccionada);
            }
            if (cantSeleccionada <= 0) cantSeleccionada = 1;

            int stockDisponible = int.TryParse(stockRaw, out int s) ? s : 0;
            if (cantSeleccionada > stockDisponible && stockDisponible > 0)
            {
                MessageBox.Show($"La cantidad solicitada ({cantSeleccionada}) supera el stock disponible ({stockDisponible}).",
                            "Stock Insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string precioLimpio = new string(precioRaw.Where(c => char.IsDigit(c) || c == ',' || c == '.').ToArray()).Replace(".", ",");
            decimal precio = decimal.TryParse(precioLimpio, out decimal p) ? p : 0;

            listaCarrito.Add(new DetalleVentaItem
            {
                IdProducto = idProd,
                Descripcion = desc,
                Cantidad = cantSeleccionada,
                PrecioUnitario = precio
            });

            ActualizarTotalVenta();

            if (textBox5 != null) textBox5.Clear();
            if (textBox4 != null) textBox4.Clear();
            if (textBox6 != null) textBox6.Clear();
            if (textBox7 != null) textBox7.Clear();
            if (comboBox1 != null) comboBox1.Text = "1";
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && btnEliminarCol != null && e.ColumnIndex == btnEliminarCol.Index)
            {
                if (dataGridView1.Rows[e.RowIndex].DataBoundItem is DetalleVentaItem item)
                {
                    if (MessageBox.Show($"¿Desea eliminar '{item.Descripcion}' de la lista?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        listaCarrito.Remove(item);
                        ActualizarTotalVenta();
                    }
                }
            }
        }

        private void ExecRegistrarVenta(object sender, EventArgs e)
        {
            // 1. Validar que existan productos en el carrito
            if (listaCarrito.Count == 0)
            {
                MessageBox.Show("No hay productos en la lista.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. VALIDACIÓN OBLIGATORIA DE NOMBRE Y DNI
            string nombreIngresado = textBox2 != null ? textBox2.Text.Trim() : "";
            string dniIngresado = textBox3 != null ? textBox3.Text.Trim() : "";

            bool nombreInvalido = string.IsNullOrWhiteSpace(nombreIngresado) || nombreIngresado.Equals("nombre", StringComparison.OrdinalIgnoreCase);
            bool dniInvalido = string.IsNullOrWhiteSpace(dniIngresado) || dniIngresado.Equals("dni", StringComparison.OrdinalIgnoreCase);

            if (nombreInvalido || dniInvalido)
            {
                MessageBox.Show("Debe completar obligatoriamente el Nombre y el DNI del cliente para poder registrar la venta.",
                            "Datos Incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 3. Registrar Venta si pasa las validaciones
            decimal total = listaCarrito.Sum(x => x.SubTotal);

            MessageBox.Show($"¡Venta registrada con éxito!\n\nCliente: {nombreIngresado}\nDNI: {dniIngresado}\nTotal: $ {total:N2}",
                            "Venta Finalizada", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Reiniciar estado post-venta
            listaCarrito.Clear();
            ActualizarTotalVenta();

            if (textBox1 != null) textBox1.Text = "";
            if (textBox2 != null) textBox2.Text = "nombre";
            if (textBox3 != null) textBox3.Text = "dni";
        }

        private void ActualizarTotalVenta()
        {
            decimal total = listaCarrito.Sum(x => x.SubTotal);

            Label labelTotalAbajo = null;
            int mayorY = -1;

            foreach (Control c in this.Controls)
            {
                BuscarLabelInferior(c, ref labelTotalAbajo, ref mayorY);
            }

            if (labelTotalAbajo != null)
            {
                labelTotalAbajo.Text = $"$ {total:N2}";
            }
        }

        private void BuscarLabelInferior(Control contenedor, ref Label resultado, ref int mayorY)
        {
            foreach (Control c in contenedor.Controls)
            {
                if (c is Label lbl)
                {
                    Point posPantalla = lbl.Parent != null ? lbl.Parent.PointToScreen(lbl.Location) : lbl.Location;
                    if (posPantalla.Y > mayorY && (lbl.Text.Contains("$") || lbl.Text.Contains("0")))
                    {
                        mayorY = posPantalla.Y;
                        resultado = lbl;
                    }
                }
                if (c.HasChildren)
                {
                    BuscarLabelInferior(c, ref resultado, ref mayorY);
                }
            }
        }

        #endregion

        #region --- MÉTODOS REQUERIDOS POR EL DESIGNER ---

        private void button1_Click_1(object sender, EventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void button2_Click_1(object sender, EventArgs e) { }
        private void button4_Click(object sender, EventArgs e) { }
        private void textBox6_TextChanged(object sender, EventArgs e) { }
        private void label1_Click_1(object sender, EventArgs e) { }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void button5_Click(object sender, EventArgs e) { }

        private void label1_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void label5_Click(object sender, EventArgs e) { }
        private void label6_Click(object sender, EventArgs e) { }
        private void label7_Click(object sender, EventArgs e) { }
        private void label10_Click(object sender, EventArgs e) { }
        private void label11_Click(object sender, EventArgs e) { }
        private void label12_Click(object sender, EventArgs e) { }
        private void label13_Click(object sender, EventArgs e) { }
        private void label14_Click(object sender, EventArgs e) { }
        private void label15_Click(object sender, EventArgs e) { }
        private void label16_Click(object sender, EventArgs e) { }
        private void label17_Click(object sender, EventArgs e) { }

        #endregion

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}