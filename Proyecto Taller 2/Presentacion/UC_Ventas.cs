using Proyecto_Taller_2.Entidades;
using Proyecto_Taller_2.Negocio;
using Proyecto_Taller_2.Presentacion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TextBox = System.Windows.Forms.TextBox;

namespace Proyecto_Taller_2
{
    public partial class UC_Ventas : UserControl
    {
        private BindingList<DetalleVenta> listaCarrito = new BindingList<DetalleVenta>();

        private int idUsuarioActual = 7;
        private bool clienteValidado = false;
        private bool productoValidado = false;
        private int stockActualProducto = 0;
        private int idClienteSeleccionado = 0;

        private BindingSource bsNombre = new BindingSource();
        private BindingSource bsApellido = new BindingSource();
        private BindingSource bsProducto = new BindingSource();

        public UC_Ventas()
        {
            InitializeComponent();
        }

        private void UC_Ventas_Load(object sender, EventArgs e)
        {
            ConfigurarGrid();
            ConfigurarGridRegistro();
            ConfigurarComboBoxCantidad();
            CargarComboBoxMetodoPago();
            CargarDatosComboBox();
            ConfigurarFiltrosFechas();
            ConfigurarCamposEstaticos();
            VincularEventosYValidaciones();
            LimpiarCamposInicio();
            ActualizarTotalVenta();
            CargarRegistroVentas();
        }

        private void LimpiarCamposInicio()
        {
            if (textBox1 != null) textBox1.Text = "";
            if (textBox5 != null) textBox5.Text = "";
            if (textBox2 != null) textBox2.Text = "";
            if (textBox6 != null) textBox6.Text = "";
            if (textBox7 != null) textBox7.Text = "";

            if (comboBox2 != null) { comboBox2.SelectedIndex = -1; comboBox2.Text = ""; }
            if (comboBox3 != null) { comboBox3.SelectedIndex = -1; comboBox3.Text = ""; }
            if (comboBox4 != null) { comboBox4.SelectedIndex = -1; comboBox4.Text = ""; }

            if (label35 != null) label35.Text = "";
        }

        private bool cargandoDatos = false;

        private void ComboBox2_DropDown(object sender, EventArgs e)
        {
            // Al abrir el combo de nombres, quitamos el filtro de nombres para mostrar todos
            bsNombre.Filter = string.Empty;
        }

        private void ComboBox3_DropDown(object sender, EventArgs e)
        {
            // Al abrir el combo de apellidos, quitamos el filtro de apellidos para mostrar todos
            bsApellido.Filter = string.Empty;
        }

        private void button2_Click_1(object sender, EventArgs e) 
        {
            using (FormBuscarProducto modal = new FormBuscarProducto())
            {
                // Abrimos la ventana de búsqueda como diálogo modal
                if (modal.ShowDialog() == DialogResult.OK && modal.FilaProductoSeleccionada != null)
                {
                    DataRow producto = modal.FilaProductoSeleccionada;

                    // Cargar los controles visuales del Formulario Principal
                    int idProducto = Convert.ToInt32(producto["id_producto"]); //[cite: 4]
                    int stockDisponible = Convert.ToInt32(producto["stock"]); //[cite: 4]
                    decimal precioVenta = Convert.ToDecimal(producto["precio_venta"]); //[cite: 4]

                    textBox5.Text = idProducto.ToString(); //
                    //txtNombreProducto.Text = $"{producto["nombre"]} ({producto["edicion"]} - Talle {producto["talle"]})"; //[cite: 3, 4]
                    textBox2.Text = producto["descripcion"]?.ToString() ?? ""; //[cite: 3, 4]
                    textBox6.Text = stockDisponible.ToString(); //
                    textBox7.Text = precioVenta.ToString("N2"); //
                    comboBox1.Text = stockDisponible.ToString();
                }
            }
        }
        private void CargarDatosComboBox()
        {
            try
            {
                cargandoDatos = true;
                VentaNegocio negocio = new VentaNegocio();

                // 1. Cargar tabla de clientes
                DataTable dtClientes = negocio.ObtenerClientes();

                if (dtClientes != null && dtClientes.Rows.Count > 0)
                {
                    bsNombre.DataSource = dtClientes;
                    bsApellido.DataSource = dtClientes;

                    if (comboBox2 != null)
                    {
                        comboBox2.DataSource = bsNombre;
                        comboBox2.DisplayMember = "nombre";
                        comboBox2.ValueMember = "id_cliente";
                        comboBox2.DropDownStyle = ComboBoxStyle.DropDown;
                        comboBox2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        comboBox2.AutoCompleteSource = AutoCompleteSource.ListItems;
                    }

                    if (comboBox3 != null)
                    {
                        comboBox3.DataSource = bsApellido;
                        comboBox3.DisplayMember = "apellido";
                        comboBox3.ValueMember = "id_cliente";
                        comboBox3.DropDownStyle = ComboBoxStyle.DropDown;
                        comboBox3.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        comboBox3.AutoCompleteSource = AutoCompleteSource.ListItems;
                    }
                }

                // 2. Cargar tabla de productos (comboBox4)
                DataTable dtProductos = negocio.ObtenerProductos();
                if (dtProductos != null && dtProductos.Rows.Count > 0)
                {
                    bsProducto.DataSource = dtProductos;
                    if (comboBox4 != null)
                    {
                        comboBox4.DataSource = bsProducto;
                        comboBox4.DisplayMember = "nombre";
                        comboBox4.ValueMember = "id_producto";
                        comboBox4.DropDownStyle = ComboBoxStyle.DropDown;
                        comboBox4.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        comboBox4.AutoCompleteSource = AutoCompleteSource.ListItems;
                    }
                }

                // 3. Suscribir eventos y limpiar selecciones por defecto
                if (comboBox2 != null)
                {
                    comboBox2.SelectedIndexChanged -= ComboBox2_SelectedIndexChanged;
                    comboBox2.SelectedIndexChanged += ComboBox2_SelectedIndexChanged;
                    comboBox2.SelectedIndex = -1;
                }

                if (comboBox3 != null)
                {
                    comboBox3.SelectedIndexChanged -= ComboBox3_SelectedIndexChanged;
                    comboBox3.SelectedIndexChanged += ComboBox3_SelectedIndexChanged;
                    comboBox3.SelectedIndex = -1;
                }

                if (comboBox4 != null)
                {
                    comboBox4.SelectedIndexChanged -= ComboBox4_SelectedIndexChanged;
                    comboBox4.SelectedIndexChanged += ComboBox4_SelectedIndexChanged;
                    comboBox4.KeyUp -= ComboBox4_KeyUp;
                    comboBox4.KeyUp += ComboBox4_KeyUp;
                    comboBox4.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos en los selectores: " + ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                cargandoDatos = false;
            }
        }

        private void ComboBox4_KeyUp(object sender, KeyEventArgs e)
        {
            if (cargandoDatos) return;
            if (comboBox4 != null)
            {
                string textoBusqueda = comboBox4.Text;
                int cursorPosition = comboBox4.SelectionStart;

                if (string.IsNullOrWhiteSpace(textoBusqueda))
                {
                    bsProducto.Filter = string.Empty;
                }
                else
                {
                    bsProducto.Filter = string.Format("nombre LIKE '%{0}%'", textoBusqueda.Replace("'", "''"));
                }

                comboBox4.Text = textoBusqueda;
                comboBox4.SelectionStart = cursorPosition;
                comboBox4.SelectionLength = 0;

                if (!comboBox4.DroppedDown && comboBox4.Focused && textoBusqueda.Length > 0)
                {
                    comboBox4.DroppedDown = true;
                }
            }
        }

        private void ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cargandoDatos) return;

            if (comboBox3 != null && comboBox3.SelectedItem is DataRowView row)
            {
                cargandoDatos = true;
                try
                {
                    string apellidoSeleccionado = row["apellido"]?.ToString() ?? "";
                    int idCliente = Convert.ToInt32(row["id_cliente"]);

                    // 1. Apagamos autocompletado del combo 2 para que el control nativo Win32 no intente leer memoria destruida
                    if (comboBox2 != null) comboBox2.AutoCompleteMode = AutoCompleteMode.None;

                    // 2. Creamos una vista filtrada de los nombres correspondientes a este apellido
                    DataTable dtOriginal = (DataTable)bsNombre.DataSource;
                    if (dtOriginal != null)
                    {
                        DataView dvNombres = new DataView(dtOriginal);
                        dvNombres.RowFilter = $"apellido = '{apellidoSeleccionado.Replace("'", "''")}'";

                        if (comboBox2 != null)
                        {
                            comboBox2.DataSource = dvNombres;
                            comboBox2.DisplayMember = "nombre";
                            comboBox2.ValueMember = "id_cliente";

                            // Si hay varios (ej: Pérez Juan y Pérez Pedro), desmarcamos para requerir la selección de nombre
                            if (dvNombres.Count > 1)
                            {
                                comboBox2.SelectedIndex = -1;
                                idClienteSeleccionado = 0;
                                if (textBox1 != null) textBox1.Text = string.Empty;
                                if (label35 != null) label35.Text = string.Empty;
                                clienteValidado = false;
                            }
                            else if (dvNombres.Count == 1)
                            {
                                comboBox2.SelectedValue = idCliente;
                                AsignarDatosCliente(row);
                            }
                        }
                    }
                }
                finally
                {
                    // 3. Reactivamos el autocompletado de forma segura
                    if (comboBox2 != null) comboBox2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cargandoDatos = false;
                }
            }
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cargandoDatos) return;

            if (comboBox2 != null && comboBox2.SelectedItem is DataRowView row)
            {
                cargandoDatos = true;
                try
                {
                    string nombreSeleccionado = row["nombre"]?.ToString() ?? "";
                    int idCliente = Convert.ToInt32(row["id_cliente"]);

                    if (comboBox3 != null) comboBox3.AutoCompleteMode = AutoCompleteMode.None;

                    DataTable dtOriginal = (DataTable)bsApellido.DataSource;
                    if (dtOriginal != null)
                    {
                        DataView dvApellidos = new DataView(dtOriginal);
                        dvApellidos.RowFilter = $"nombre = '{nombreSeleccionado.Replace("'", "''")}'";

                        if (comboBox3 != null)
                        {
                            comboBox3.DataSource = dvApellidos;
                            comboBox3.DisplayMember = "apellido";
                            comboBox3.ValueMember = "id_cliente";

                            if (dvApellidos.Count > 1 && comboBox3.SelectedIndex == -1)
                            {
                                // Si hay múltiples (ej: Juan Pérez y Juan Domingo), dejamos que elija apellido
                                comboBox3.SelectedIndex = -1;
                            }
                            else
                            {
                                comboBox3.SelectedValue = idCliente;
                            }
                        }
                    }

                    AsignarDatosCliente(row);
                }
                finally
                {
                    if (comboBox3 != null) comboBox3.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cargandoDatos = false;
                }
            }
        }

        private void AsignarDatosCliente(DataRowView row)
        {
            idClienteSeleccionado = Convert.ToInt32(row["id_cliente"]);
            if (textBox1 != null) textBox1.Text = idClienteSeleccionado.ToString();
            if (label35 != null) label35.Text = row["documento"]?.ToString() ?? "";
            clienteValidado = true;
        }

        // Método auxiliar opcional para mantener limpio el estado cuando la selección está incompleta
        private void LimpiarDatosCliente()
        {
            idClienteSeleccionado = 0;
            if (comboBox2 != null) comboBox2.Text = null;
            if (comboBox3 != null) comboBox3.Text = null;
            if (textBox1 != null) textBox1.Text = string.Empty;
            if (label35 != null) label35.Text = string.Empty;
            clienteValidado = false;
        }
        private void ComboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cargandoDatos) return;
            if (comboBox4 != null && comboBox4.SelectedItem is DataRowView row)
            {
                cargandoDatos = true;

                if (textBox5 != null) textBox5.Text = row["id_producto"].ToString();
                if (textBox2 != null) textBox2.Text = row["descripcion"].ToString();

                int.TryParse(row["stock"]?.ToString(), out stockActualProducto);
                if (textBox6 != null) textBox6.Text = stockActualProducto.ToString();

                if (textBox7 != null && row["precio_venta"] != DBNull.Value)
                {
                    decimal precio = Convert.ToDecimal(row["precio_venta"]);
                    textBox7.Text = precio.ToString("N2");
                }

                productoValidado = true;
                cargandoDatos = false;
            }
        }

        private void ConfigurarCamposEstaticos()
        {
            if (textBox2 != null) textBox2.ReadOnly = true;
            if (textBox6 != null) textBox6.ReadOnly = true;
            if (textBox7 != null) textBox7.ReadOnly = true;

            if (textBox1 != null)
            {
                textBox1.KeyPress += (s, ev) => {
                    if (!char.IsControl(ev.KeyChar) && !char.IsDigit(ev.KeyChar)) ev.Handled = true;
                };
            }

            if (textBox5 != null)
            {
                textBox5.KeyPress += (s, ev) => {
                    if (!char.IsControl(ev.KeyChar) && !char.IsDigit(ev.KeyChar)) ev.Handled = true;
                };
            }

            Action<object, KeyPressEventArgs> soloLetras = (s, ev) => {
                if (!char.IsControl(ev.KeyChar) && !char.IsLetter(ev.KeyChar) && !char.IsWhiteSpace(ev.KeyChar))
                {
                    ev.Handled = true;
                }
            };

            if (comboBox2 != null) comboBox2.KeyPress += new KeyPressEventHandler(soloLetras);
            if (comboBox3 != null) comboBox3.KeyPress += new KeyPressEventHandler(soloLetras);
            if (comboBox4 != null) comboBox4.KeyPress += new KeyPressEventHandler(soloLetras);
        }

        private void ConfigurarFiltrosFechas()
        {
            if (dateTimePicker1 != null) dateTimePicker1.Value = DateTime.Now.AddMonths(-1);
            if (dateTimePicker2 != null) dateTimePicker2.Value = DateTime.Now;
        }

        private void ConfigurarComboBoxCantidad()
        {
            if (comboBox1 != null)
            {
                comboBox1.Items.Clear();
                comboBox1.Items.AddRange(new object[] { "1", "2", "3", "4", "5", "10", "15", "20" });
                comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
                comboBox1.SelectedItem = "1";
            }
        }

        private void CargarComboBoxMetodoPago()
        {
            if (cboMetodoPago != null)
            {
                try
                {
                    MetodoPagoNegocio negocioMetodoPago = new MetodoPagoNegocio();
                    cboMetodoPago.DataSource = negocioMetodoPago.CargarMetodosPago();
                    cboMetodoPago.DisplayMember = "Nombre";
                    cboMetodoPago.ValueMember = "IdMetodoPago";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se pudieron cargar los métodos de pago: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ConfigurarGridEstilo(DataGridView gv)
        {
            if (gv == null) return;
            gv.AutoGenerateColumns = false;
            gv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gv.AllowUserToAddRows = false;
            gv.AllowUserToDeleteRows = false;
            gv.ReadOnly = true;
            gv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gv.MultiSelect = false;
            gv.RowHeadersVisible = false;
            gv.BackgroundColor = Color.White;

            gv.EnableHeadersVisualStyles = false;
            gv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 40, 50);
            gv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            gv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            gv.RowsDefaultCellStyle.ForeColor = Color.Black;
            gv.RowsDefaultCellStyle.BackColor = Color.White;
            gv.RowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(51, 153, 255);
            gv.RowsDefaultCellStyle.SelectionForeColor = Color.White;

            gv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 247, 250);
            gv.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black;
            gv.DefaultCellStyle.ForeColor = Color.Black;
            gv.DefaultCellStyle.BackColor = Color.White;
        }

        private void ConfigurarGrid()
        {
            ConfigurarGridEstilo(dataGridView1);
            dataGridView1.DataSource = listaCarrito;
            if (dataGridView1.Columns.Count >= 5)
            {
                dataGridView1.Columns[0].DataPropertyName = "IdProducto";
                dataGridView1.Columns[0].HeaderText = "ID Producto";
                dataGridView1.Columns[1].DataPropertyName = "Nombre";
                dataGridView1.Columns[2].DataPropertyName = "Descripcion";
                dataGridView1.Columns[3].DataPropertyName = "Cantidad";
                dataGridView1.Columns[4].DataPropertyName = "SubTotal";
                dataGridView1.Columns[4].DefaultCellStyle.Format = "N2";
            }
            dataGridView1.CellClick -= DataGridView1_CellClick;
            dataGridView1.CellClick += DataGridView1_CellClick;
        }

        private void ConfigurarGridRegistro()
        {
            ConfigurarGridEstilo(dataGridView2);
            if (dataGridView2 != null && dataGridView2.Columns.Count >= 6)
            {
                dataGridView2.Columns[0].DataPropertyName = "IdVenta";
                dataGridView2.Columns[0].HeaderText = "ID Venta";
                dataGridView2.Columns[1].DataPropertyName = "Descripcion";
                dataGridView2.Columns[1].HeaderText = "Nombre";
                dataGridView2.Columns[2].DataPropertyName = "Fecha";
                dataGridView2.Columns[2].HeaderText = "Fecha";
                dataGridView2.Columns[3].DataPropertyName = "Cliente";
                dataGridView2.Columns[3].HeaderText = "Cliente";
                dataGridView2.Columns[4].DataPropertyName = "Total";
                dataGridView2.Columns[4].HeaderText = "Total";
                dataGridView2.Columns[4].DefaultCellStyle.Format = "N2";
                dataGridView2.Columns[5].DataPropertyName = "MetodoPago";
                dataGridView2.Columns[5].HeaderText = "Metodo de Pago";
            }
            dataGridView2.CellClick -= DataGridView2_CellClick;
            dataGridView2.CellClick += DataGridView2_CellClick;
        }

        private void VincularEventosYValidaciones()
        {
            if (button1 != null) { button1.Click -= ExecBuscarCliente; button1.Click += ExecBuscarCliente; }
            if (button2 != null) { button2.Click -= ExecBuscarProducto; button2.Click += ExecBuscarProducto; }
            if (button4 != null) { button4.Click -= ExecAgregarProducto; button4.Click += ExecAgregarProducto; }
            if (button5 != null) { button5.Click -= ExecRegistrarVenta; button5.Click += ExecRegistrarVenta; }
            if (button6 != null) { button6.Click -= BtnFiltrarFechasHistorial_Click; button6.Click += BtnFiltrarFechasHistorial_Click; }

            if (comboBox2 != null) { comboBox2.SelectedIndexChanged -= ComboBox2_SelectedIndexChanged; comboBox2.SelectedIndexChanged += ComboBox2_SelectedIndexChanged; }
            if (comboBox3 != null) { comboBox3.SelectedIndexChanged -= ComboBox3_SelectedIndexChanged; comboBox3.SelectedIndexChanged += ComboBox3_SelectedIndexChanged; }
            if (comboBox4 != null) { comboBox4.SelectedIndexChanged -= ComboBox4_SelectedIndexChanged; comboBox4.SelectedIndexChanged += ComboBox4_SelectedIndexChanged; }
        }

        private void BtnFiltrarFechasHistorial_Click(object sender, EventArgs e)
        {
            DateTime desde = dateTimePicker1 != null ? dateTimePicker1.Value.Date : DateTime.Now.AddMonths(-1);
            DateTime hasta = dateTimePicker2 != null ? dateTimePicker2.Value.Date : DateTime.Now;

            if (desde > hasta)
            {
                MessageBox.Show("La fecha 'Desde' no puede ser posterior a la fecha 'Hasta'.", "Filtro Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CargarRegistroVentas(desde, hasta);
        }

        private void GuardarControlComoImagen(Control controlAImprimir, string tituloDocumento, string nombreArchivoBase)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Imagen PNG (*.png)|*.png";
                sfd.FileName = $"{nombreArchivoBase}_{DateTime.Now:yyyyMMdd_HHmmss}.png";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (Bitmap bmp = new Bitmap(controlAImprimir.Width, controlAImprimir.Height))
                        {
                            controlAImprimir.DrawToBitmap(bmp, new Rectangle(0, 0, controlAImprimir.Width, controlAImprimir.Height));
                            bmp.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Png);

                            MessageBox.Show($"¡{tituloDocumento} guardado con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al exportar imagen: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // TICKET DINÁMICO QUE CONSULTA LOS PRODUCTOS REALES Y LOS LISTA EN LÍNEAS SEPARADAS
        private void MostrarComprobanteVenta(int idVenta, string fecha, string cliente, string totalGeneral)
        {
            Form frmTicket = new Form
            {
                Text = $"Comprobante de Venta #{idVenta}",
                Size = new Size(500, 680),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                BackColor = Color.FromArgb(245, 247, 250)
            };

            Panel pnlContenidoImprimible = new Panel { Dock = DockStyle.Fill, BackColor = Color.White };
            Panel headerPanel = new Panel { Dock = DockStyle.Top, Height = 90, BackColor = Color.FromArgb(30, 40, 50) };

            Label lblBrand = new Label
            {
                Text = "⚽ ESENCIA RETRO CAMISETAS ⚽",
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                ForeColor = Color.Gold,
                Dock = DockStyle.Top,
                Height = 45,
                TextAlign = ContentAlignment.MiddleCenter
            };

            Label lblSubBrand = new Label
            {
                Text = "Comprobante Oficial de Venta - Ticket Interno",
                Font = new Font("Segoe UI", 9, FontStyle.Italic),
                ForeColor = Color.White,
                Dock = DockStyle.Bottom,
                Height = 35,
                TextAlign = ContentAlignment.MiddleCenter
            };

            headerPanel.Controls.Add(lblBrand);
            headerPanel.Controls.Add(lblSubBrand);

            VentaNegocio negocio = new VentaNegocio();
            DataTable dtDetalle = negocio.ObtenerDetalleVentaPorId(idVenta);

            string lineasProductos = "";
            if (dtDetalle != null)
            {
                foreach (DataRow r in dtDetalle.Rows)
                {
                    string nombreProd = r["Nombre"]?.ToString() ?? "Artículo";
                    string cant = r["Cantidad"]?.ToString() ?? "1";

                    if (decimal.TryParse(r["SubTotal"]?.ToString(), out decimal subTotalDec))
                    {
                        string subTotal = subTotalDec.ToString("N2");
                        lineasProductos += $" {nombreProd,-26} {cant,3}   $ {subTotal,8}\r\n";
                    }
                }
            }

            TextBox txtBody = new TextBox
            {
                Multiline = true,
                ReadOnly = true,
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                ForeColor = Color.Black,
                Font = new Font("Consolas", 10f),
                BorderStyle = BorderStyle.None,
                Text = $"\r\n" +
                       $"==================================================\r\n" +
                       $"                 TICKET DE VENTA                  \r\n" +
                       $"==================================================\r\n" +
                       $" ID Venta    : #{idVenta}\r\n" +
                       $" Fecha/Hora  : {fecha}\r\n" +
                       $" Cliente     : {cliente}\r\n" +
                       $"--------------------------------------------------\r\n" +
                       $" DESCRIPCIÓN                 CANT.     SUBTOTAL \r\n" +
                       $"--------------------------------------------------\r\n" +
                       lineasProductos +
                       $"==================================================\r\n" +
                       $" TOTAL A PAGAR:                   $ {totalGeneral,8}\r\n" +
                       $"==================================================\r\n\r\n" +
                       $"       ¡Gracias por elegir Esencia Retro!         "
            };

            pnlContenidoImprimible.Controls.Add(txtBody);
            pnlContenidoImprimible.Controls.Add(headerPanel);

            Panel panelBotones = new Panel { Dock = DockStyle.Bottom, Height = 60, BackColor = Color.FromArgb(230, 235, 240) };

            System.Windows.Forms.Button btnImprimir = new System.Windows.Forms.Button
            {
                Text = "🖨️ Imprimir Ticket",
                Width = 200,
                Height = 42,
                Left = 20,
                Top = 9,
                BackColor = Color.FromArgb(40, 167, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5f, FontStyle.Bold)
            };
            btnImprimir.Click += (s, ev) => GuardarControlComoImagen(pnlContenidoImprimible, "Comprobante", $"Ticket_Venta_{idVenta}");

            System.Windows.Forms.Button btnCerrar = new System.Windows.Forms.Button
            {
                Text = "❌ Cerrar",
                Width = 200,
                Height = 42,
                Left = 255,
                Top = 9,
                BackColor = Color.FromArgb(220, 53, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5f, FontStyle.Bold)
            };
            btnCerrar.Click += (s, ev) => frmTicket.Close();

            panelBotones.Controls.Add(btnImprimir);
            panelBotones.Controls.Add(btnCerrar);
            frmTicket.Controls.Add(pnlContenidoImprimible);
            frmTicket.Controls.Add(panelBotones);
            frmTicket.ShowDialog();
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string nombreColumna = dataGridView1.Columns[e.ColumnIndex].Name.ToLower();

                if (nombreColumna.Contains("eliminar") || dataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
                {
                    if (dataGridView1.Rows[e.RowIndex].DataBoundItem is DetalleVenta item)
                    {
                        DialogResult resultado = MessageBox.Show($"¿Desea eliminar '{item.Nombre}' de la lista?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (resultado == DialogResult.Yes)
                        {
                            listaCarrito.Remove(item);
                            ActualizarTotalVenta();
                        }
                    }
                }
            }
        }

        private void DataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView2.Rows[e.RowIndex];
                string nombreColumna = dataGridView2.Columns[e.ColumnIndex].Name.ToLower();

                bool esColumnaVer = nombreColumna.Contains("ver") || dataGridView2.Columns[e.ColumnIndex] is DataGridViewButtonColumn;

                if (esColumnaVer)
                {
                    string idVentaStr = row.Cells.Count > 0 ? row.Cells[0].Value?.ToString() ?? "0" : "0";
                    string fecha = row.Cells.Count > 2 ? row.Cells[2].Value?.ToString() ?? DateTime.Now.ToShortDateString() : DateTime.Now.ToShortDateString();
                    string cliente = row.Cells.Count > 3 ? row.Cells[3].Value?.ToString() ?? "Cliente General" : "Cliente General";
                    string total = row.Cells.Count > 4 ? row.Cells[4].Value?.ToString() ?? "0.00" : "0.00";

                    int.TryParse(idVentaStr, out int idVentaNum);

                    MostrarComprobanteVenta(idVentaNum, fecha, cliente, total);
                }
            }
        }

        private void ExecBuscarCliente(object sender, EventArgs e)
        {
            if (textBox1 == null || string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Por favor, ingrese un ID de cliente válido.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                clienteValidado = false;
                return;
            }

            if (int.TryParse(textBox1.Text.Trim(), out int idCliente))
            {
                try
                {
                    VentaNegocio negocio = new VentaNegocio();
                    DataTable dtCliente = negocio.ObtenerClientePorId(idCliente);

                    if (dtCliente != null && dtCliente.Rows.Count > 0)
                    {
                        DataRow row = dtCliente.Rows[0];
                        idClienteSeleccionado = idCliente;

                        cargandoDatos = true;
                        if (comboBox2 != null) comboBox2.SelectedValue = idClienteSeleccionado;
                        if (comboBox3 != null) comboBox3.SelectedValue = idClienteSeleccionado;
                        if (label35 != null) label35.Text = row["documento"].ToString();
                        cargandoDatos = false;

                        clienteValidado = true;
                    }
                    else
                    {
                        MessageBox.Show("No se encontró ningún cliente con ese ID.", "No Encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clienteValidado = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al buscar cliente: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    clienteValidado = false;
                }
            }
        }

        private void ExecBuscarProducto(object sender, EventArgs e)
        {
            if (textBox5 == null || string.IsNullOrWhiteSpace(textBox5.Text))
            {
                MessageBox.Show("Por favor, ingrese un producto válido para poder realizar la venta.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                productoValidado = false;
                return;
            }

            if (int.TryParse(textBox5.Text.Trim(), out int idProducto))
            {
                try
                {
                    VentaNegocio negocio = new VentaNegocio();
                    DataTable dtProducto = negocio.ObtenerProductoPorId(idProducto);

                    if (dtProducto != null && dtProducto.Rows.Count > 0)
                    {
                        DataRow row = dtProducto.Rows[0];

                        cargandoDatos = true;
                        if (comboBox4 != null) comboBox4.SelectedValue = idProducto;
                        cargandoDatos = false;

                        if (textBox2 != null) textBox2.Text = row["descripcion"].ToString();

                        int.TryParse(row["stock"]?.ToString(), out stockActualProducto);
                        if (textBox6 != null) textBox6.Text = stockActualProducto.ToString();

                        if (textBox7 != null && row["precio_venta"] != DBNull.Value)
                        {
                            decimal precio = Convert.ToDecimal(row["precio_venta"]);
                            textBox7.Text = precio.ToString("N2");
                        }

                        productoValidado = true;
                    }
                    else
                    {
                        MessageBox.Show("No se encontró ningún producto con ese ID.", "No Encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        productoValidado = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al buscar producto: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    productoValidado = false;
                }
            }
        }

        private void ExecAgregarProducto(object sender, EventArgs e)
        {
            if (!clienteValidado)
            {
                MessageBox.Show("Debe buscar y seleccionar un cliente válido antes de agregar productos.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!productoValidado || textBox5 == null)
            {
                MessageBox.Show("Debe buscar y seleccionar un producto válido primero.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int cantidadSeleccionada = 1;
            if (comboBox1 != null && int.TryParse(comboBox1.Text, out int cant))
            {
                cantidadSeleccionada = cant;
            }

            if (cantidadSeleccionada > stockActualProducto)
            {
                MessageBox.Show($"La cantidad solicitada supera el stock disponible ({stockActualProducto}).", "Stock Insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal precioUnitario = 0;
            if (textBox7 != null && !string.IsNullOrEmpty(textBox7.Text))
            {
                string precioLimpio = textBox7.Text.Replace("$", "").Replace(".", "").Replace(",", ".").Trim();
                decimal.TryParse(precioLimpio, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out precioUnitario);
            }

            int.TryParse(textBox5.Text.Trim(), out int idProdNum);
            string nombreProdStr = comboBox4 != null ? comboBox4.Text.Trim() : "";

            listaCarrito.Add(new DetalleVenta
            {
                IdProducto = idProdNum,
                Nombre = nombreProdStr,
                Descripcion = textBox2 != null ? textBox2.Text.Trim() : "",
                Cantidad = cantidadSeleccionada,
                PrecioUnitario = precioUnitario
            });

            ActualizarTotalVenta();
        }

        private void ExecRegistrarVenta(object sender, EventArgs e)
        {
            if (!clienteValidado)
            {
                MessageBox.Show("Debe buscar y cargar un cliente válido.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (listaCarrito.Count == 0)
            {
                MessageBox.Show("El carrito de compras está vacío.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int idMetodo = 1;
                if (cboMetodoPago != null && cboMetodoPago.SelectedValue != null)
                {
                    int.TryParse(cboMetodoPago.SelectedValue.ToString(), out idMetodo);
                }

                decimal totalVenta = listaCarrito.Sum(x => x.SubTotal);
                List<DetalleVenta> listaParaNegocio = listaCarrito.ToList();

                VentaNegocio negocio = new VentaNegocio();
                bool exito = negocio.RegistrarVenta(idUsuarioActual, idClienteSeleccionado, idMetodo, "B", totalVenta, listaParaNegocio, out string mensaje);

                if (exito)
                {
                    MessageBox.Show(mensaje, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    listaCarrito.Clear();
                    LimpiarCamposInicio();
                    clienteValidado = false;
                    productoValidado = false;
                    ActualizarTotalVenta();
                    CargarRegistroVentas();
                }
                else
                {
                    MessageBox.Show(mensaje, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar venta: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarRegistroVentas(DateTime? fechaDesde = null, DateTime? fechaHasta = null)
        {
            try
            {
                VentaNegocio negocio = new VentaNegocio();
                DataTable dtVentas = negocio.ObtenerHistorialVentas();

                if (fechaDesde.HasValue && fechaHasta.HasValue && dtVentas.Columns.Contains("Fecha"))
                {
                    var rowsFiltradas = dtVentas.AsEnumerable().Where(r =>
                    {
                        DateTime.TryParse(r["Fecha"].ToString(), out DateTime fVenta);
                        return fVenta.Date >= fechaDesde.Value.Date && fVenta.Date <= fechaHasta.Value.Date;
                    });
                    if (rowsFiltradas.Any())
                        dtVentas = rowsFiltradas.CopyToDataTable();
                    else
                        dtVentas.Rows.Clear();
                }

                dataGridView2.DataSource = dtVentas;

                decimal totalFacturado = 0;
                foreach (DataRow row in dtVentas.Rows)
                {
                    if (row["Total"] != DBNull.Value)
                    {
                        decimal.TryParse(row["Total"].ToString(), out decimal subTotal);
                        totalFacturado += subTotal;
                    }
                }

                if (lblTotalFacturado != null) lblTotalFacturado.Text = $"$ {totalFacturado:N2}";
                if (lblCantidadVentas != null) lblCantidadVentas.Text = dtVentas.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar historial: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualizarTotalVenta()
        {
            decimal total = listaCarrito.Sum(x => x.SubTotal);
            if (label17 != null)
            {
                label17.Text = $"$ {total:N2}";
            }
        }

        #region --- MÉTODOS REQUERIDOS POR EL DESIGNER ---
        private void button1_Click_1(object sender, EventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }

        private void button4_Click(object sender, EventArgs e) { }
        private void textBox6_TextChanged(object sender, EventArgs e) { }
        private void label1_Click_1(object sender, EventArgs e) { }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void button5_Click(object sender, EventArgs e) { }
        private void panel4_Paint(object sender, PaintEventArgs e) { }
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void panel6_Paint(object sender, PaintEventArgs e) { }
        private void cboMetodoPago_SelectedIndexChanged(object sender, EventArgs e) { }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e) { }
        private void label20_Click(object sender, EventArgs e) { }
        private void tabPage3_Click(object sender, EventArgs e) { }
        private void tabPage1_Click(object sender, EventArgs e) { }
        private void panel12_Paint(object sender, PaintEventArgs e) { }
        private void label14_Click(object sender, EventArgs e) { }
        private void label11_Click(object sender, EventArgs e) { }
        private void label5_Click(object sender, EventArgs e) { }
        private void label16_Click(object sender, EventArgs e) { }
        private void label17_Click(object sender, EventArgs e) { }
        private void label6_Click(object sender, EventArgs e) { }
        private void panel5_Paint(object sender, PaintEventArgs e) { }
        private void panel11_Paint(object sender, PaintEventArgs e) { }
        #endregion

        private void BtnLimpiarCliente_Click(object sender, EventArgs e)
        {
            cargandoDatos = true;
            try
            {
                if (comboBox2 != null)
                {
                    comboBox2.DataSource = bsNombre;
                    comboBox2.DisplayMember = "nombre";
                    comboBox2.ValueMember = "id_cliente";
                    comboBox2.SelectedIndex = -1;
                }

                if (comboBox3 != null)
                {
                    comboBox3.DataSource = bsApellido;
                    comboBox3.DisplayMember = "apellido";
                    comboBox3.ValueMember = "id_cliente";
                    comboBox3.SelectedIndex = -1;
                }

                idClienteSeleccionado = 0;
                if (textBox1 != null) textBox1.Text = string.Empty;
                if (label35 != null) label35.Text = string.Empty;
                clienteValidado = false;
            }
            finally
            {
                cargandoDatos = false;
            }
        }

        private void LimpiarSeleccionCliente()
        {
            cargandoDatos = true;
            try
            {
                // 1. Quitar filtros
                if (bsNombre != null) bsNombre.Filter = string.Empty;
                if (bsApellido != null) bsApellido.Filter = string.Empty;

                // 2. Desmarcar selecciones
                if (comboBox2 != null) comboBox2.SelectedIndex = -1;
                if (comboBox3 != null) comboBox3.SelectedIndex = -1;

                // 3. Resetear datos
                ResetearDatosCliente();
            }
            finally
            {
                cargandoDatos = false;
            }
        }
        private void ResetearDatosCliente()
        {
            idClienteSeleccionado = 0;
            if (textBox1 != null) textBox1.Text = string.Empty;
            if (label35 != null) label35.Text = string.Empty;
            clienteValidado = false;
        }
    }
}