using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Proyecto_Taller_2
{
    public partial class UC_Balance : UserControl
    {
        // Modelo interno de Factura
        public class FacturaModel
        {
            public string Numero { get; set; }
            public DateTime Fecha { get; set; }
            public string DNI { get; set; }
            public string Cliente { get; set; }
            public decimal MontoTotal { get; set; }
            public string Estado { get; set; } // "Pagado" o "No Pagado"
        }

        private List<FacturaModel> _listaFacturas;
        private FacturaModel _facturaEnEdicion = null; // Controla si estamos en modo edición

        public UC_Balance()
        {
            InitializeComponent();
            InicializarDatos();
            ConfigurarControlesUI();
            CargarGraficosEstadisticos();
            ActualizarTablaYTotales(_listaFacturas);
        }

        #region Inicialización y Configuración

        private void InicializarDatos()
        {
            _listaFacturas = new List<FacturaModel>
            {
                new FacturaModel { Numero = "001", Fecha = new DateTime(2026, 8, 10), DNI = "26201890", Cliente = "Juan Pérez", MontoTotal = 85000m, Estado = "Pagado" },
                new FacturaModel { Numero = "002", Fecha = new DateTime(2026, 8, 15), DNI = "26063002", Cliente = "María Gómez", MontoTotal = 120000m, Estado = "No Pagado" },
                new FacturaModel { Numero = "003", Fecha = new DateTime(2026, 9, 01), DNI = "30568803", Cliente = "Carlos López", MontoTotal = 95000m, Estado = "Pagado" },
                new FacturaModel { Numero = "004", Fecha = new DateTime(2026, 9, 12), DNI = "26567004", Cliente = "Ana Martínez", MontoTotal = 150000m, Estado = "Pagado" },
                new FacturaModel { Numero = "005", Fecha = new DateTime(2026, 9, 20), DNI = "30245018", Cliente = "Roberto Díaz", MontoTotal = 60000m, Estado = "No Pagado" }
            };
        }

        private void ConfigurarControlesUI()
        {
            // 1. Configuración de Filtros de Estado
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(new string[] { "Todos", "Pagado", "No Pagado" });
            comboBox1.SelectedIndex = 0;

            comboBox2.Items.Clear();
            comboBox2.Items.AddRange(new string[] { "Pagado", "No Pagado" });
            comboBox2.SelectedIndex = 0;

            // 2. Definir rango amplio por defecto en los Selectores de Fecha para mostrar todas las facturas
            dateTimePicker1.Value = new DateTime(2026, 1, 1);
            dateTimePicker2.Value = DateTime.Now;

            // 3. Ajustes de renderizado e interacciones del DataGridView
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.RowTemplate.Height = 28;
            dataGridView1.ScrollBars = ScrollBars.Both;

            // FUERZA EL COLOR DE LETRA NEGRO EN LAS CELDAS PARA QUE SE VEAN CLARAMENTE
            dataGridView1.RowsDefaultCellStyle.ForeColor = Color.Black;
            dataGridView1.RowsDefaultCellStyle.BackColor = Color.White;
            dataGridView1.DefaultCellStyle.ForeColor = Color.Black;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.White;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(51, 153, 255);

            // Personalización visual de DataGridView (Cabecera y filas alternadas)
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(40, 50, 70);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 247, 250);
            dataGridView1.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black;

            // 4. VALIDACIONES DE ENTRADA EN TEXTBOXES (KEYPRESS)
            textBox1.KeyPress -= TextBox1_KeyPress_SoloNumeros;
            textBox1.KeyPress += TextBox1_KeyPress_SoloNumeros;

            textBox2.KeyPress -= TextBox2_KeyPress_SoloLetras;
            textBox2.KeyPress += TextBox2_KeyPress_SoloLetras;

            textBox3.KeyPress -= TextBox3_KeyPress_SoloMonto;
            textBox3.KeyPress += TextBox3_KeyPress_SoloMonto;

            textBox4.KeyPress -= TextBox4_KeyPress_SoloFecha;
            textBox4.KeyPress += TextBox4_KeyPress_SoloFecha;

            textBox5.KeyPress -= TextBox5_KeyPress_SoloDni;
            textBox5.KeyPress += TextBox5_KeyPress_SoloDni;

            // 5. Ajustes del Botón "Ver Reporte" (button4) para evitar texto cortado
            button4.Padding = new Padding(0);
            button4.TextAlign = ContentAlignment.MiddleCenter;
            button4.TextImageRelation = TextImageRelation.ImageBeforeText;

            // 6. Suscripción de Eventos de Botones
            button1.Click -= BtnBuscarFecha_Click;
            button1.Click += BtnBuscarFecha_Click;

            button2.Click -= BtnBuscarEstado_Click;
            button2.Click += BtnBuscarEstado_Click;

            button3.Click -= BtnAgregarFacturacion_Click;
            button3.Click += BtnAgregarFacturacion_Click;

            button4.Click -= BtnVerReporteGeneral_Click;
            button4.Click += BtnVerReporteGeneral_Click;

            button5.Click -= BtnImprimirReporteGeneral_Click;
            button5.Click += BtnImprimirReporteGeneral_Click;

            // 7. Configurar Evento del DataGridView para acciones en celdas
            dataGridView1.CellContentClick -= DataGridView1_CellContentClick;
            dataGridView1.CellContentClick += DataGridView1_CellContentClick;
        }

        #endregion

        #region Validaciones de Campos (Keypress)

        private void TextBox1_KeyPress_SoloNumeros(object sender, KeyPressEventArgs e)
        {
            // Solo números para Nro de Factura
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TextBox2_KeyPress_SoloLetras(object sender, KeyPressEventArgs e)
        {
            // Solo letras y espacios para Cliente
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TextBox3_KeyPress_SoloMonto(object sender, KeyPressEventArgs e)
        {
            // Números y punto o coma decimal para Monto Total
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // Permitir solo un separador decimal
            TextBox txt = sender as TextBox;
            if ((e.KeyChar == '.' || e.KeyChar == ',') && (txt.Text.Contains(".") || txt.Text.Contains(",")))
            {
                e.Handled = true;
            }
        }

        private void TextBox4_KeyPress_SoloFecha(object sender, KeyPressEventArgs e)
        {
            // Solo números y la barra ('/') para Fecha
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '/')
            {
                e.Handled = true;
            }
        }

        private void TextBox5_KeyPress_SoloDni(object sender, KeyPressEventArgs e)
        {
            // Solo números para DNI
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        #endregion

        #region Gráficos Estadísticos

        private void CargarGraficosEstadisticos()
        {
            // --- GRÁFICO 1: Ventas por Colección de Remeras ---
            chart1.Series.Clear();
            chart1.Titles.Clear();
            chart1.Titles.Add("Ventas por Colección de Remeras (Unidades)");

            Series serieColecciones = chart1.Series.Add("Colecciones");
            serieColecciones.ChartType = SeriesChartType.Column;
            serieColecciones.IsValueShownAsLabel = true;

            serieColecciones.Points.AddXY("Champions", 105);
            serieColecciones.Points.AddXY("Libertadores", 70);
            serieColecciones.Points.AddXY("Mundiales", 120);
            serieColecciones.Points.AddXY("Ligas Europeas", 90);

            if (serieColecciones.Points.Count >= 4)
            {
                serieColecciones.Points[0].Color = Color.FromArgb(52, 120, 180);
                serieColecciones.Points[1].Color = Color.FromArgb(225, 155, 60);
                serieColecciones.Points[2].Color = Color.FromArgb(90, 165, 210);
                serieColecciones.Points[3].Color = Color.FromArgb(30, 75, 120);
            }

            // --- GRÁFICO 2: Top 5 Diseños Más Vendidos ---
            chart2.Series.Clear();
            chart2.Titles.Clear();
            chart2.Titles.Add("Top 5 Diseños Más Vendidos");

            Series serieTop5 = chart2.Series.Add("TopDiseños");
            serieTop5.ChartType = SeriesChartType.Bar;
            serieTop5.IsValueShownAsLabel = true;

            serieTop5.Points.AddXY("Champion Ligas Europeas", 13);
            serieTop5.Points.AddXY("Boca Jrs Mundiales", 22);
            serieTop5.Points.AddXY("Boca Jrs Libertadores", 25);
            serieTop5.Points.AddXY("Real Madrid Champions", 38);
            serieTop5.Points.AddXY("Argentina '86 Mundiales", 40);

            if (serieTop5.Points.Count >= 5)
            {
                serieTop5.Points[0].Color = Color.FromArgb(235, 160, 65);
                serieTop5.Points[1].Color = Color.FromArgb(190, 75, 70);
                serieTop5.Points[2].Color = Color.FromArgb(15, 70, 130);
                serieTop5.Points[3].Color = Color.FromArgb(200, 160, 110);
                serieTop5.Points[4].Color = Color.FromArgb(100, 175, 215);
            }
        }

        #endregion

        #region Métodos de Renderizado y Filtros

        private void ActualizarTablaYTotales(List<FacturaModel> origenDatos)
        {
            dataGridView1.Rows.Clear();

            foreach (var fact in origenDatos)
            {
                int rowIndex = dataGridView1.Rows.Add(
                    fact.Numero,
                    fact.Fecha.ToString("dd/MM/yyyy"),
                    fact.DNI,
                    fact.Cliente,
                    fact.MontoTotal.ToString("C2"),
                    fact.Estado,
                    "🖨️",
                    "✏️",
                    "🗑️"
                );

                // FORZAR COLOR NEGRO NÍTIDO CELDA POR CELDA EN CADA FILA NUEVA
                for (int i = 0; i < 6; i++)
                {
                    dataGridView1.Rows[rowIndex].Cells[i].Style.ForeColor = Color.Black;
                    dataGridView1.Rows[rowIndex].Cells[i].Style.BackColor = Color.White;
                }
            }

            decimal totalRecaudado = origenDatos.Where(f => f.Estado == "Pagado").Sum(f => f.MontoTotal);
            int cantidadEmitidas = origenDatos.Count;

            // ASIGNACIÓN A TUS PANELES INFERIORES:
            if (label17 != null) label17.Text = totalRecaudado.ToString("C2");
            if (label18 != null) label18.Text = cantidadEmitidas.ToString();
        }

        private void BtnBuscarFecha_Click(object sender, EventArgs e)
        {
            DateTime fechaDesde = dateTimePicker1.Value.Date;
            DateTime fechaHasta = dateTimePicker2.Value.Date;

            if (fechaHasta < fechaDesde)
            {
                MessageBox.Show("La fecha 'Hasta' no puede ser menor a la fecha 'Desde'. Verifique el rango de fechas.",
                                "Rango de Fechas Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var filtradas = _listaFacturas
                .Where(f => f.Fecha.Date >= fechaDesde && f.Fecha.Date <= fechaHasta)
                .ToList();

            ActualizarTablaYTotales(filtradas);
        }

        private void BtnBuscarEstado_Click(object sender, EventArgs e)
        {
            string estadoSeleccionado = comboBox1.SelectedItem != null ? comboBox1.SelectedItem.ToString() : "Todos";

            if (estadoSeleccionado == "Todos")
            {
                ActualizarTablaYTotales(_listaFacturas);
            }
            else
            {
                var filtradas = _listaFacturas.Where(f => f.Estado == estadoSeleccionado).ToList();
                ActualizarTablaYTotales(filtradas);
            }
        }

        #endregion

        #region Formulario de Agregar / Editar Facturación

        private void BtnAgregarFacturacion_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text) ||
                string.IsNullOrWhiteSpace(textBox4.Text) ||
                string.IsNullOrWhiteSpace(textBox5.Text))
            {
                MessageBox.Show("No se puede completar la operación porque falta ingresar información en algunos campos.",
                                "Campos Incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(textBox3.Text.Replace("$", "").Replace(".", ",").Trim(), out decimal montoValidado))
            {
                MessageBox.Show("El campo 'Monto Total' debe contener un valor numérico válido.",
                                "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!DateTime.TryParse(textBox4.Text.Trim(), out DateTime fechaValidada))
            {
                MessageBox.Show("El campo 'Fecha' debe tener un formato de fecha válido (Ej: 21/09/2026).",
                                "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_facturaEnEdicion != null)
            {
                _facturaEnEdicion.Numero = textBox1.Text.Trim();
                _facturaEnEdicion.Cliente = textBox2.Text.Trim();
                _facturaEnEdicion.MontoTotal = montoValidado;
                _facturaEnEdicion.Fecha = fechaValidada;
                _facturaEnEdicion.DNI = textBox5.Text.Trim();
                _facturaEnEdicion.Estado = comboBox2.SelectedItem.ToString();

                MessageBox.Show($"Facturación N° {_facturaEnEdicion.Numero} actualizada con éxito.",
                                "Edición Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _facturaEnEdicion = null;
                button3.Text = "➕  Agregar Facturación";
            }
            else
            {
                FacturaModel nuevaFactura = new FacturaModel
                {
                    Numero = textBox1.Text.Trim(),
                    Cliente = textBox2.Text.Trim(),
                    MontoTotal = montoValidado,
                    Fecha = fechaValidada,
                    DNI = textBox5.Text.Trim(),
                    Estado = comboBox2.SelectedItem.ToString()
                };

                _listaFacturas.Add(nuevaFactura);
                MessageBox.Show($"Facturación N° {nuevaFactura.Numero} registrada exitosamente.",
                                "Registro Completado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            LimpiarFormularioDetalle();
            ActualizarTablaYTotales(_listaFacturas);
        }

        private void LimpiarFormularioDetalle()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            if (comboBox2.Items.Count > 0) comboBox2.SelectedIndex = 0;
            _facturaEnEdicion = null;
            button3.Text = "➕  Agregar Facturación";
        }

        #endregion

        #region Acciones de la Grilla (Imprimir, Editar, Eliminar)

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string nroFactura = dataGridView1.Rows[e.RowIndex].Cells[0].Value?.ToString();
            string nombreCliente = dataGridView1.Rows[e.RowIndex].Cells[3].Value?.ToString();
            var factura = _listaFacturas.FirstOrDefault(f => f.Numero == nroFactura);

            if (e.ColumnIndex == 6)
            {
                MessageBox.Show($"Facturación N° {nroFactura} del cliente '{nombreCliente}' impresa correctamente.",
                                "Impresión de Factura", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (e.ColumnIndex == 7)
            {
                DialogResult dr = MessageBox.Show($"¿Desea editar los datos de la Factura N° {nroFactura}?",
                                                  "Confirmar Edición", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes && factura != null)
                {
                    _facturaEnEdicion = factura;
                    textBox1.Text = factura.Numero;
                    textBox2.Text = factura.Cliente;
                    textBox3.Text = factura.MontoTotal.ToString("0.00");
                    textBox4.Text = factura.Fecha.ToString("dd/MM/yyyy");
                    textBox5.Text = factura.DNI;
                    comboBox2.SelectedItem = factura.Estado;

                    button3.Text = "💾  Guardar Cambios";
                }
            }
            else if (e.ColumnIndex == 8)
            {
                DialogResult dr = MessageBox.Show($"¿Está seguro de que desea eliminar la Factura N° {nroFactura}?",
                                                  "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.Yes && factura != null)
                {
                    _listaFacturas.Remove(factura);
                    ActualizarTablaYTotales(_listaFacturas);
                    MessageBox.Show($"La Factura N° {nroFactura} ha sido eliminada.",
                                    "Registro Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        #endregion

        #region Reportes Generales

        private void BtnVerReporteGeneral_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Reporte General de Facturación y Balances abierto exitosamente.",
                            "Reporte General", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnImprimirReporteGeneral_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Reporte General impreso correctamente en la impresora predeterminada.",
                            "Reporte General", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion
    }
}