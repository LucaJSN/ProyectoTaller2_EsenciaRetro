using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Proyecto_Taller_2.Negocio;

namespace Proyecto_Taller_2
{
    public partial class UC_Balance : UserControl
    {
        public UC_Balance()
        {
            InitializeComponent();
            ConfigurarControlesUI();
        }

        private void UC_Balance_Load(object sender, EventArgs e)
        {
            // Rango por defecto del año actual
            if (dateTimePicker1 != null) dateTimePicker1.Value = new DateTime(DateTime.Now.Year, 1, 1);
            if (dateTimePicker2 != null) dateTimePicker2.Value = DateTime.Now;

            CargarGraficosEstadisticos(dateTimePicker1.Value.Date, dateTimePicker2.Value.Date);
        }

        private void ConfigurarControlesUI()
        {
            if (button6 != null)
            {
                button6.Click -= BtnFiltrarFechas_Click;
                button6.Click += BtnFiltrarFechas_Click;
            }
        }

        private void BtnFiltrarFechas_Click(object sender, EventArgs e)
        {
            DateTime desde = dateTimePicker1 != null ? dateTimePicker1.Value.Date : DateTime.Now.AddMonths(-1);
            DateTime hasta = dateTimePicker2 != null ? dateTimePicker2.Value.Date : DateTime.Now;

            // VALIDACIÓN ESTRICTA SOLICITADA 
            if (desde > hasta)
            {
                MessageBox.Show("La fecha 'Desde' no puede ser posterior a la fecha 'Hasta'.",
                                "Filtro Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CargarGraficosEstadisticos(desde, hasta);
        }

        private void CargarGraficosEstadisticos(DateTime desde, DateTime hasta)
        {
            BalanceNegocio negocio = new BalanceNegocio();

            try
            {
                // --- GRÁFICO 1 (chart3 - Izquierda): Evolución de Ventas Mensuales (Gráfico de Líneas) ---
                if (chart3 != null)
                {
                    chart3.Series.Clear();
                    chart3.Titles.Clear();
                    chart3.Titles.Add("Evolución de Ventas por Mes ($)");
                    chart3.Titles[0].Font = new Font("Segoe UI", 11, FontStyle.Bold);

                    Series serieMeses = chart3.Series.Add("VentasMensuales");
                    serieMeses.ChartType = SeriesChartType.Spline; // Línea curva suave y moderna
                    serieMeses.BorderWidth = 3;
                    serieMeses.Color = Color.FromArgb(41, 128, 185);
                    serieMeses.IsValueShownAsLabel = true;
                    serieMeses.MarkerStyle = MarkerStyle.Circle;
                    serieMeses.MarkerSize = 8;
                    serieMeses.MarkerColor = Color.FromArgb(231, 76, 60);

                    DataTable dtMeses = negocio.ObtenerVentasPorMes(desde, hasta);
                    if (dtMeses != null && dtMeses.Rows.Count > 0)
                    {
                        foreach (DataRow row in dtMeses.Rows)
                        {
                            string mes = row["NombreMes"].ToString();
                            decimal total = Convert.ToDecimal(row["TotalVentas"]);
                            serieMeses.Points.AddXY(mes, total);
                        }
                    }
                    else
                    {
                        serieMeses.Points.AddXY("Sin Ventas", 0);
                    }

                    // Estética del área del gráfico
                    chart3.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;
                    chart3.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;
                }

                // --- GRÁFICO 2 (chart1 - Derecha Superior): Recaudación por Método de Pago (Gráfico de Torta / Pie limpio) ---
                if (chart1 != null)
                {
                    chart1.Series.Clear();
                    chart1.Titles.Clear();
                    chart1.Titles.Add("Recaudación por Método de Pago");
                    chart1.Titles[0].Font = new Font("Segoe UI", 10, FontStyle.Bold);

                    Series serieMetodos = chart1.Series.Add("MetodosPago");
                    serieMetodos.ChartType = SeriesChartType.Pie; // Tipo Torta
                    serieMetodos.IsValueShownAsLabel = true;
                    serieMetodos.Label = "#PERCENT"; // Solo muestra el porcentaje dentro del gráfico

                    DataTable dtMetodos = negocio.ObtenerVentasPorMetodoPago(desde, hasta);
                    if (dtMetodos != null && dtMetodos.Rows.Count > 0)
                    {
                        foreach (DataRow row in dtMetodos.Rows)
                        {
                            string metodo = row["Metodo"].ToString();
                            decimal total = Convert.ToDecimal(row["TotalRecaudado"]);

                            int index = serieMetodos.Points.AddXY(metodo, total);
                            serieMetodos.Points[index].LegendText = $"{metodo} (${total:N0})"; // Texto claro para la leyenda lateral
                        }
                    }
                    else
                    {
                        serieMetodos.Points.AddXY("Sin Datos", 1);
                    }
                }

                // --- GRÁFICO 3 (chart2 - Derecha Inferior): Top 5 Diseños Más Vendidos ---
                if (chart2 != null)
                {
                    chart2.Series.Clear();
                    chart2.Titles.Clear();
                    chart2.Titles.Add("Top 5 Diseños Más Vendidos");
                    chart2.Titles[0].Font = new Font("Segoe UI", 10, FontStyle.Bold);

                    Series serieTop5 = chart2.Series.Add("TopDiseños");
                    serieTop5.ChartType = SeriesChartType.Bar;
                    serieTop5.IsValueShownAsLabel = true;
                    serieTop5.Color = Color.FromArgb(39, 174, 96);

                    DataTable dtTop5 = negocio.ObtenerTop5Productos(desde, hasta);
                    if (dtTop5 != null && dtTop5.Rows.Count > 0)
                    {
                        foreach (DataRow row in dtTop5.Rows)
                        {
                            string prod = row["Producto"].ToString();
                            int cant = Convert.ToInt32(row["TotalCantidad"]);
                            serieTop5.Points.AddXY(prod, cant);
                        }
                    }
                    else
                    {
                        serieTop5.Points.AddXY("Sin Datos", 0);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las estadísticas del balance: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }
    }
}