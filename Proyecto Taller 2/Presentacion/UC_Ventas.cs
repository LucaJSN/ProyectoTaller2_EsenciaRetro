using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Proyecto_Taller_2.Negocio;
using TextBox = System.Windows.Forms.TextBox;

namespace Proyecto_Taller_2
{
    public partial class UC_Ventas : UserControl
    {
        public class DetalleVentaItem
        {
            public string IdProducto { get; set; }
            public string Nombre { get; set; }
            public string Descripcion { get; set; }
            public int Cantidad { get; set; }
            public decimal PrecioUnitario { get; set; }
            public decimal SubTotal => Cantidad * PrecioUnitario;
        }

        private BindingList<DetalleVentaItem> listaCarrito = new BindingList<DetalleVentaItem>();

        public string NombreUsuarioLogueado { get; set; } = "Samuel Ibarra (Administrador)";

        private bool clienteValidado = false;
        private bool productoValidado = false;
        private int stockActualProducto = 0;

        public UC_Ventas()
        {
            InitializeComponent();
        }

        private void UC_Ventas_Load(object sender, EventArgs e)
        {
            ConfigurarGrid();
            ConfigurarGridRegistro();
            ConfigurarGridFacturacion();
            ConfigurarComboBoxCantidad();
            CargarComboBoxMetodoPago();
            ConfigurarFiltrosFechas();
            ConfigurarCamposEstaticos();
            MostrarVendedorActual();
            VincularEventosYValidaciones();
            ActualizarTotalVenta();
            CargarRegistroVentas();
            CargarRegistroFacturas();
        }

        private void MostrarVendedorActual()
        {
            if (label3 != null)
            {
                label3.Text = NombreUsuarioLogueado;
            }
        }

        private void ConfigurarCamposEstaticos()
        {
            // Bloquear campos informativos para que no se pueda escribir en ellos manualmente
            if (textBox4 != null) textBox4.ReadOnly = true; // Nombre
            if (textBox2 != null) textBox2.ReadOnly = true; // Descripción
            if (textBox6 != null) textBox6.ReadOnly = true; // Stock
            if (textBox7 != null) textBox7.ReadOnly = true; // Precio Venta

            // Configurar ID Cliente (textBox1): solo números y selección al enfocar
            if (textBox1 != null)
            {
                textBox1.Enabled = true;
                textBox1.ReadOnly = false;
                textBox1.GotFocus += (s, ev) => { textBox1.SelectAll(); };
                textBox1.KeyPress += (s, ev) =>
                {
                    if (!char.IsControl(ev.KeyChar) && !char.IsDigit(ev.KeyChar))
                        ev.Handled = true; // Bloquea letras
                };
            }

            // Configurar ID Producto (textBox5): solo números y selección al enfocar
            if (textBox5 != null)
            {
                textBox5.Enabled = true;
                textBox5.ReadOnly = false;
                textBox5.GotFocus += (s, ev) => { textBox5.SelectAll(); };
                textBox5.KeyPress += (s, ev) =>
                {
                    if (!char.IsControl(ev.KeyChar) && !char.IsDigit(ev.KeyChar))
                        ev.Handled = true; // Bloquea letras
                };
            }

            // Configurar ComboBox de Cantidad (comboBox1): solo permitir números
            if (comboBox1 != null)
            {
                comboBox1.KeyPress += (s, ev) =>
                {
                    if (!char.IsControl(ev.KeyChar) && !char.IsDigit(ev.KeyChar))
                        ev.Handled = true;
                };
            }
        }

        private void ConfigurarFiltrosFechas()
        {
            if (dateTimePicker1 != null) dateTimePicker1.Value = DateTime.Now.AddMonths(-1);
            if (dateTimePicker2 != null) dateTimePicker2.Value = DateTime.Now;

            if (dateTimePicker4 != null) dateTimePicker4.Value = DateTime.Now.AddMonths(-1);
            if (dateTimePicker3 != null) dateTimePicker3.Value = DateTime.Now;
        }

        private void ConfigurarComboBoxCantidad()
        {
            if (comboBox1 != null)
            {
                comboBox1.Items.Clear();
                comboBox1.Items.AddRange(new object[] { "1", "2", "3", "4", "5", "10", "15", "20" });
                comboBox1.DropDownStyle = ComboBoxStyle.DropDown;
                comboBox1.Text = "1";
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
                dataGridView2.Columns[1].DataPropertyName = "Descripcion";
                dataGridView2.Columns[2].DataPropertyName = "Fecha";
                dataGridView2.Columns[3].DataPropertyName = "Cliente";
                dataGridView2.Columns[4].DataPropertyName = "Total";
                dataGridView2.Columns[5].DataPropertyName = "MetodoPago";
            }

            

            dataGridView2.CellClick -= DataGridView2_CellClick;
            dataGridView2.CellClick += DataGridView2_CellClick;
        }

        private void ConfigurarGridFacturacion()
        {
            ConfigurarGridEstilo(dataGridView3);
            if (dataGridView3 != null && dataGridView3.Columns.Count >= 6)
            {
                dataGridView3.Columns[0].DataPropertyName = "Numero";
                dataGridView3.Columns[1].DataPropertyName = "TipoFactura";
                dataGridView3.Columns[2].DataPropertyName = "Fecha";
                dataGridView3.Columns[3].DataPropertyName = "DNI";
                dataGridView3.Columns[4].DataPropertyName = "Cliente";
                dataGridView3.Columns[5].DataPropertyName = "MontoTotal";
            }
            dataGridView3.CellClick -= DataGridView3_CellClick;
            dataGridView3.CellClick += DataGridView3_CellClick;
        }

        private void VincularEventosYValidaciones()
        {
            if (button1 != null) { button1.Click -= ExecBuscarCliente; button1.Click += ExecBuscarCliente; }
            if (button2 != null) { button2.Click -= ExecBuscarProducto; button2.Click += ExecBuscarProducto; }
            if (button4 != null) { button4.Click -= ExecAgregarProducto; button4.Click += ExecAgregarProducto; }
            if (button5 != null) { button5.Click -= ExecRegistrarVenta; button5.Click += ExecRegistrarVenta; }
            if (button6 != null) { button6.Click -= BtnFiltrarFechasHistorial_Click; button6.Click += BtnFiltrarFechasHistorial_Click; }
            if (button8 != null) { button8.Click -= BtnFiltrarFechasFacturacion_Click; button8.Click += BtnFiltrarFechasFacturacion_Click; }
        }

        #region --- RESTRICCIÓN DE FECHAS ESTRICTA ---

        private void BtnFiltrarFechasHistorial_Click(object sender, EventArgs e)
        {
            DateTime desde = dateTimePicker1 != null ? dateTimePicker1.Value.Date : DateTime.Now.AddMonths(-1);
            DateTime hasta = dateTimePicker2 != null ? dateTimePicker2.Value.Date : DateTime.Now;

            if (desde > hasta)
            {
                MessageBox.Show("La fecha 'Desde' no puede ser posterior a la fecha 'Hasta'. Verifique el rango seleccionado.", "Filtro Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CargarRegistroVentas(desde, hasta);
        }

        private void BtnFiltrarFechasFacturacion_Click(object sender, EventArgs e)
        {
            DateTime desde = dateTimePicker4 != null ? dateTimePicker4.Value.Date : DateTime.Now.AddMonths(-1);
            DateTime hasta = dateTimePicker3 != null ? dateTimePicker3.Value.Date : DateTime.Now;

            if (desde > hasta)
            {
                MessageBox.Show("La fecha 'Desde' no puede ser posterior a la fecha 'Hasta'. Verifique el rango seleccionado.", "Filtro Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CargarRegistroFacturas(desde, hasta);
        }

        #endregion

        #region --- EXPORTACIÓN Y VISTAS ORIGINALES ---

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

                            MessageBox.Show($"¡{tituloDocumento} guardado con éxito en:\n{sfd.FileName}", "Exportación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al exportar la imagen: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void MostrarComprobanteVenta(string idVenta, string fecha, string cliente, string descripcion, string cantidad, string total)
        {
            Form frmTicket = new Form
            {
                Text = $"Comprobante de Venta #{idVenta}",
                Size = new Size(500, 660),
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

            TextBox txtBody = new TextBox
            {
                Multiline = true,
                ReadOnly = true,
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                ForeColor = Color.Black,
                Font = new Font("Consolas", 10.5f),
                BorderStyle = BorderStyle.None,
                Text = $"\r\n" +
                       $"==================================================\r\n" +
                       $"                 TICKET DE VENTA                  \r\n" +
                       $"==================================================\r\n" +
                       $" ID Venta    : #{idVenta}\r\n" +
                       $" Fecha/Hora  : {DateTime.Now:dd/MM/yyyy HH:mm:ss}\r\n" +
                       $" Cliente     : {cliente}\r\n" +
                       $"--------------------------------------------------\r\n" +
                       $" DESCRIPCIÓN                    CANT.    SUBTOTAL \r\n" +
                       $"--------------------------------------------------\r\n" +
                       $" {descripcion,-28} {cantidad,3}    $ {total,8}\r\n" +
                       $"                                                  \r\n" +
                       $"==================================================\r\n" +
                       $" TOTAL A PAGAR:                  $ {total,8}\r\n" +
                       $"==================================================\r\n\r\n" +
                       $"       ¡Gracias por elegir Esencia Retro!         \r\n" +
                       $"       Conservá este ticket como comprobante.     "
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
            btnImprimir.FlatAppearance.BorderSize = 0;
            btnImprimir.Click += (s, ev) => GuardarControlComoImagen(pnlContenidoImprimible, "Comprobante de Venta", $"Ticket_Venta_{idVenta}");

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
            btnCerrar.FlatAppearance.BorderSize = 0;
            btnCerrar.Click += (s, ev) => frmTicket.Close();

            panelBotones.Controls.Add(btnImprimir);
            panelBotones.Controls.Add(btnCerrar);
            frmTicket.Controls.Add(pnlContenidoImprimible);
            frmTicket.Controls.Add(panelBotones);
            frmTicket.ShowDialog();
        }

        private void MostrarFacturaOficial(string nroFactura, string fecha, string dni, string cliente, string total, string tipoFactura)
        {
            Form frmFactura = new Form
            {
                Text = $"Factura Oficial {tipoFactura} - #{nroFactura}",
                Size = new Size(540, 700),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                BackColor = Color.FromArgb(240, 243, 246)
            };

            Panel pnlContenidoImprimible = new Panel { Dock = DockStyle.Fill, BackColor = Color.White };
            Panel headerPanel = new Panel { Dock = DockStyle.Top, Height = 100, BackColor = Color.FromArgb(20, 40, 60) };

            Label lblTipoBox = new Label
            {
                Text = $"[{tipoFactura}]",
                Font = new Font("Segoe UI", 22, FontStyle.Bold),
                ForeColor = Color.Gold,
                Width = 70,
                Height = 70,
                Left = 20,
                Top = 15,
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.FromArgb(30, 50, 75)
            };

            Label lblBrand = new Label
            {
                Text = "ESENCIA RETRO S.R.L.",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.White,
                Left = 105,
                Top = 20,
                AutoSize = true
            };

            Label lblSubBrand = new Label
            {
                Text = $"FACTURA OFICIAL TIPO {tipoFactura}\nIVA Responsable Inscripto - CUIT: 30-71452983-4",
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                ForeColor = Color.LightGray,
                Left = 105,
                Top = 48,
                AutoSize = true
            };

            headerPanel.Controls.Add(lblTipoBox);
            headerPanel.Controls.Add(lblBrand);
            headerPanel.Controls.Add(lblSubBrand);

            TextBox txtBody = new TextBox
            {
                Multiline = true,
                ReadOnly = true,
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                ForeColor = Color.Black,
                Font = new Font("Consolas", 10.5f),
                BorderStyle = BorderStyle.None,
                Text = $"\r\n" +
                       $"==================================================\r\n" +
                       $" DATOS FISCALES Y COMERCIALES                     \r\n" +
                       $"==================================================\r\n" +
                       $" Nro Factura : {nroFactura}\r\n" +
                       $" Fecha/Hora  : {DateTime.Now:dd/MM/yyyy HH:mm:ss}\r\n" +
                       $" CUIT / DNI  : {dni}\r\n" +
                       $" Razón Social: {cliente}\r\n" +
                       $" Cond. Venta : Contado\r\n" +
                       $"--------------------------------------------------\r\n" +
                       $" CANT.  DETALLE                              SUBTOTAL  \r\n" +
                       $"--------------------------------------------------\r\n" +
                       $"    1   Indumentaria y Artículos Retro   $ {total,8}\r\n" +
                       $"                                                  \r\n" +
                       $"==================================================\r\n" +
                       $" SUBTOTAL NETO GRAVADO:              $ {total,8}\r\n" +
                       $" TRIBUTOS / IVA (21%):               $ 0.00    \r\n" +
                       $" IMPORTE TOTAL:                      $ {total,8}\r\n" +
                       $"==================================================\r\n\r\n" +
                       $" CAEM / C.A.I. Nro : 44892019382104               \r\n" +
                       $" Vencimiento C.A.I.: 31/12/2028                   "
            };

            pnlContenidoImprimible.Controls.Add(txtBody);
            pnlContenidoImprimible.Controls.Add(headerPanel);

            Panel panelBotones = new Panel { Dock = DockStyle.Bottom, Height = 60, BackColor = Color.FromArgb(230, 235, 240) };

            System.Windows.Forms.Button btnReimprimir = new System.Windows.Forms.Button
            {
                Text = "🖨️ Reimprimir Factura",
                Width = 210,
                Height = 42,
                Left = 20,
                Top = 9,
                BackColor = Color.FromArgb(0, 123, 255),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5f, FontStyle.Bold)
            };
            btnReimprimir.FlatAppearance.BorderSize = 0;
            btnReimprimir.Click += (s, ev) => GuardarControlComoImagen(pnlContenidoImprimible, $"Factura Oficial {tipoFactura}", $"Factura_{tipoFactura}_{nroFactura}");

            System.Windows.Forms.Button btnCerrar = new System.Windows.Forms.Button
            {
                Text = "❌ Cerrar",
                Width = 210,
                Height = 42,
                Left = 275,
                Top = 9,
                BackColor = Color.FromArgb(220, 53, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5f, FontStyle.Bold)
            };
            btnCerrar.FlatAppearance.BorderSize = 0;
            btnCerrar.Click += (s, ev) => frmFactura.Close();

            panelBotones.Controls.Add(btnReimprimir);
            panelBotones.Controls.Add(btnCerrar);
            frmFactura.Controls.Add(pnlContenidoImprimible);
            frmFactura.Controls.Add(panelBotones);
            frmFactura.ShowDialog();
        }

        #endregion

        #region --- EVENTOS GRILLAS ---

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string nombreColumna = dataGridView1.Columns[e.ColumnIndex].Name.ToLower();

                if (nombreColumna.Contains("eliminar") || dataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
                {
                    if (dataGridView1.Rows[e.RowIndex].DataBoundItem is DetalleVentaItem item)
                    {
                        DialogResult resultado = MessageBox.Show(
                            $"¿Desea eliminar '{item.Nombre}' de la lista?",
                            "Confirmar eliminación",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question
                        );

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

                bool esColumnaFacturacion = nombreColumna.Contains("factur") || nombreColumna.Contains("btnfacturacion");
                bool esColumnaVer = nombreColumna.Contains("ver") || nombreColumna.Contains("verbutton");

                if (esColumnaFacturacion)
                {
                    object valorIdVenta = row.Cells.Count > 0 ? row.Cells[0].Value : "1";
                    int.TryParse(valorIdVenta?.ToString(), out int idVentaNum);

                    try
                    {
                        VentaNegocio negocio = new VentaNegocio();

                        if (negocio.VentaYaFacturada(idVentaNum))
                        {
                            MessageBox.Show("Esta venta ya fue facturada anteriormente.\nPara obtener el comprobante, diríjase a la pestaña de Facturación y utilice la opción de Reimprimir Factura.", "Venta Ya Facturada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        string tipoFactura = "";
                        using (Form frmPrompt = new Form())
                        {
                            frmPrompt.Width = 380;
                            frmPrompt.Height = 180;
                            frmPrompt.FormBorderStyle = FormBorderStyle.FixedDialog;
                            frmPrompt.Text = "Emitir Factura";
                            frmPrompt.StartPosition = FormStartPosition.CenterParent;
                            frmPrompt.MaximizeBox = false;
                            frmPrompt.MinimizeBox = false;

                            Label lblText = new Label() { Left = 20, Top = 20, Text = "Seleccione el tipo de factura a emitir:", AutoSize = true, Font = new Font("Segoe UI", 10) };
                            System.Windows.Forms.Button btnA = new System.Windows.Forms.Button() { Text = "Factura A", Left = 20, Width = 95, Top = 75, Height = 40 };
                            System.Windows.Forms.Button btnB = new System.Windows.Forms.Button() { Text = "Factura B", Left = 125, Width = 95, Top = 75, Height = 40 };
                            System.Windows.Forms.Button btnCancel = new System.Windows.Forms.Button() { Text = "Cancelar", Left = 230, Width = 95, Top = 75, Height = 40 };

                            btnA.Click += (s, ev) => { tipoFactura = "A"; frmPrompt.Close(); };
                            btnB.Click += (s, ev) => { tipoFactura = "B"; frmPrompt.Close(); };
                            btnCancel.Click += (s, ev) => { tipoFactura = ""; frmPrompt.Close(); };

                            frmPrompt.Controls.Add(lblText);
                            frmPrompt.Controls.Add(btnA);
                            frmPrompt.Controls.Add(btnB);
                            frmPrompt.Controls.Add(btnCancel);
                            frmPrompt.ShowDialog();
                        }

                        if (string.IsNullOrEmpty(tipoFactura)) return;

                        object valorTotal = row.Cells.Count > 4 ? row.Cells[4].Value : "0";
                        decimal.TryParse(valorTotal?.ToString(), out decimal montoVenta);
                        string numFacturaRandom = "F-" + DateTime.Now.ToString("fff");

                        negocio.RegistrarFacturaBD(idVentaNum, numFacturaRandom, tipoFactura, montoVenta);
                        CargarRegistroFacturas();
                        MessageBox.Show($"¡Factura {tipoFactura} registrada con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al procesar la factura: " + ex.Message, "Error BD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (esColumnaVer)
                {
                    string idVenta = row.Cells.Count > 0 ? row.Cells[0].Value?.ToString() ?? "S/N" : "S/N";
                    string descripcion = row.Cells.Count > 1 ? row.Cells[1].Value?.ToString() ?? "Artículo Retro" : "Artículo Retro";
                    string fecha = row.Cells.Count > 2 ? row.Cells[2].Value?.ToString() ?? DateTime.Now.ToShortDateString() : DateTime.Now.ToShortDateString();
                    string cliente = row.Cells.Count > 3 ? row.Cells[3].Value?.ToString() ?? "Cliente General" : "Cliente General";
                    string total = row.Cells.Count > 4 ? row.Cells[4].Value?.ToString() ?? "0.00" : "0.00";

                    MostrarComprobanteVenta(idVenta, fecha, cliente, descripcion, "1", total);
                }
            }
        }

        private void DataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView3.Rows[e.RowIndex];

                string numero = row.Cells[0].Value?.ToString() ?? "S/N";
                string tipoFactura = row.Cells[1].Value?.ToString() ?? "A";
                string fecha = row.Cells[2].Value?.ToString() ?? DateTime.Now.ToShortDateString();

                // Extracción 100% dinámica del DNI y del Cliente desde la Base de Datos (grilla)
                string dni = row.Cells[3].Value?.ToString() ?? "";
                string cliente = row.Cells[4].Value?.ToString() ?? "";

                string total = row.Cells[5].Value?.ToString() ?? "0.00";

                // Muestra la factura oficial con los datos reales del cliente de esa compra
                MostrarFacturaOficial(numero, fecha, dni, cliente, total, tipoFactura);
            }
        }

        #endregion

        #region --- BÚSQUEDA Y VALIDACIONES REALES EN BASE DE DATOS ---

        private void ExecBuscarCliente(object sender, EventArgs e)
        {
            if (textBox1 == null || string.IsNullOrWhiteSpace(textBox1.Text) || textBox1.Text == "ID Cliente")
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

                        // ASIGNACIÓN EXACTA SEGÚN TUS LABELS:
                        if (label36 != null) label36.Text = row["nombre"].ToString();   // Nombre -> label36
                        if (label38 != null) label38.Text = row["apellido"].ToString(); // Apellido -> label38
                        if (label35 != null) label35.Text = row["dni"].ToString();       // DNI -> label35

                        clienteValidado = true;
                    }
                    else
                    {
                        MessageBox.Show("No se encontró ningún cliente con ese ID en la base de datos.", "Cliente No Encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (label36 != null) label36.Text = "Nombre";
                        if (label38 != null) label38.Text = "Apellido";
                        if (label35 != null) label35.Text = "DNI";
                        clienteValidado = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al buscar el cliente: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    clienteValidado = false;
                }
            }
            else
            {
                MessageBox.Show("El ID del cliente debe ser un número entero.", "Formato Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                clienteValidado = false;
            }
        }

        private void ExecBuscarProducto(object sender, EventArgs e)
        {
            if (textBox5 == null || string.IsNullOrWhiteSpace(textBox5.Text) || textBox5.Text == "ID Producto")
            {
                MessageBox.Show("Por favor, ingrese un ID de producto válido.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

                        // Asignación correcta según tus controles:
                        if (textBox4 != null) textBox4.Text = row["nombre"].ToString();      // Nombre -> textBox4
                        if (textBox2 != null) textBox2.Text = row["descripcion"].ToString(); // Descripción -> textBox2

                        int.TryParse(row["stock"]?.ToString(), out stockActualProducto);

                        if (textBox6 != null) textBox6.Text = stockActualProducto.ToString(); // Stock -> textBox6
                        if (textBox7 != null) textBox7.Text = Convert.ToDecimal(row["precio"]).ToString("N2"); // Precio -> textBox7

                        productoValidado = true;
                    }
                    else
                    {
                        MessageBox.Show("No se encontró ningún producto con ese ID en la base de datos.", "Producto No Encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (textBox4 != null) textBox4.Text = "";
                        if (textBox2 != null) textBox2.Text = "";
                        if (textBox6 != null) textBox6.Text = "0";
                        if (textBox7 != null) textBox7.Text = "0.00";
                        productoValidado = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al buscar el producto: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    productoValidado = false;
                }
            }
            else
            {
                MessageBox.Show("El ID del producto debe ser un número entero.", "Formato Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                productoValidado = false;
            }
        }

        private void ExecAgregarProducto(object sender, EventArgs e)
        {
            if (!clienteValidado)
            {
                MessageBox.Show("Debe buscar y seleccionar un cliente válido antes de agregar productos al carrito.", "Cliente Requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!productoValidado || textBox5 == null || string.IsNullOrWhiteSpace(textBox5.Text))
            {
                MessageBox.Show("Por favor, busque y seleccione un producto válido primero.", "Producto Requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int cantidadSeleccionada = 1;
            if (comboBox1 != null && int.TryParse(comboBox1.Text, out int cant))
            {
                cantidadSeleccionada = cant;
            }

            if (cantidadSeleccionada > stockActualProducto)
            {
                MessageBox.Show($"La cantidad solicitada ({cantidadSeleccionada}) supera el stock disponible ({stockActualProducto}) del producto.", "Stock Insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal precioUnitario = 0;
            if (textBox7 != null)
            {
                decimal.TryParse(textBox7.Text.Replace("$", "").Trim(), out precioUnitario);
            }

            listaCarrito.Add(new DetalleVentaItem
            {
                IdProducto = textBox5.Text.Trim(),
                Nombre = textBox4 != null ? textBox4.Text.Trim() : "",     // Guardamos el Nombre del producto
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
                MessageBox.Show("No se puede registrar la venta. Debe buscar y cargar un cliente válido.", "Cliente Requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (listaCarrito.Count == 0)
            {
                MessageBox.Show("No hay productos en la lista para registrar la venta.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int idMetodo = 1;
                if (cboMetodoPago != null && cboMetodoPago.SelectedValue != null)
                {
                    int.TryParse(cboMetodoPago.SelectedValue.ToString(), out idMetodo);
                }

                // Obtenemos el ID del cliente desde el textBox1
                int.TryParse(textBox1.Text.Trim(), out int idCliente);
                decimal totalVenta = listaCarrito.Sum(x => x.SubTotal);

                VentaNegocio negocio = new VentaNegocio();

                // Llamada al método transaccional pasando el carrito completo
                negocio.RegistrarVentaConDetalles(totalVenta, idMetodo, idCliente, 1, listaCarrito);

                MessageBox.Show("¡Venta y detalles registrados con éxito en la base de datos!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // --- LIMPIEZA COMPLETA DE LA PANTALLA ---
                listaCarrito.Clear();
                ActualizarTotalVenta();

                // Limpiar Cliente
                if (textBox1 != null) textBox1.Text = "";
                if (label36 != null) label36.Text = "Nombre";
                if (label38 != null) label38.Text = "Apellido";
                if (label35 != null) label35.Text = "DNI";
                clienteValidado = false;

                // Limpiar Producto
                if (textBox5 != null) textBox5.Text = "";
                if (textBox4 != null) textBox4.Text = "";
                if (textBox2 != null) textBox2.Text = "";
                if (textBox6 != null) textBox6.Text = "0";
                if (textBox7 != null) textBox7.Text = "0.00";
                if (comboBox1 != null) comboBox1.Text = "1";
                productoValidado = false;

                CargarRegistroVentas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar venta: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("Error al cargar el registro de ventas: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarRegistroFacturas(DateTime? fechaDesde = null, DateTime? fechaHasta = null)
        {
            try
            {
                VentaNegocio negocio = new VentaNegocio();
                DataTable dtFacturas = negocio.ObtenerHistorialFacturas();

                if (fechaDesde.HasValue && fechaHasta.HasValue && dtFacturas.Columns.Contains("Fecha"))
                {
                    var rowsFiltradas = dtFacturas.AsEnumerable().Where(r =>
                    {
                        DateTime.TryParse(r["Fecha"].ToString(), out DateTime fFactura);
                        return fFactura.Date >= fechaDesde.Value.Date && fFactura.Date <= fechaHasta.Value.Date;
                    });
                    if (rowsFiltradas.Any())
                        dtFacturas = rowsFiltradas.CopyToDataTable();
                    else
                        dtFacturas.Rows.Clear();
                }

                dataGridView3.DataSource = dtFacturas;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar facturas: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualizarTotalVenta()
        {
            decimal total = listaCarrito.Sum(x => x.SubTotal);

            // Actualizamos el label del Total a Pagar en pantalla (reemplaza lblTotalPagar si tu label tiene otro nombre como label16)
            if (label17 != null)
            {
                label17.Text = $"$ {total:N2}";
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
        private void label3_Click(object sender, EventArgs e) { }
        private void label5_Click(object sender, EventArgs e) { }
        private void label16_Click(object sender, EventArgs e) { }
        private void label17_Click(object sender, EventArgs e) { }
        private void label6_Click(object sender, EventArgs e) { }
        private void panel5_Paint(object sender, PaintEventArgs e) { }
        #endregion

        private void panel11_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}