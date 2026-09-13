namespace Proyecto_Taller_2
{
    partial class UC_Balance
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.CBMesDesde = new System.Windows.Forms.ComboBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.BtnAplicarFiltros = new System.Windows.Forms.Button();
            this.LTotalFacturado = new System.Windows.Forms.Label();
            this.LTicketPromedio = new System.Windows.Forms.Label();
            this.LTotalFacturas = new System.Windows.Forms.Label();
            this.chartEvolucion = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.DGVFacturas = new System.Windows.Forms.DataGridView();
            this.CBTipoFiltro = new System.Windows.Forms.ComboBox();
            this.CBMesHasta = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartEvolucion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGVFacturas)).BeginInit();
            this.SuspendLayout();
            // 
            // CBMesDesde
            // 
            this.CBMesDesde.FormattingEnabled = true;
            this.CBMesDesde.Location = new System.Drawing.Point(189, 87);
            this.CBMesDesde.Name = "CBMesDesde";
            this.CBMesDesde.Size = new System.Drawing.Size(121, 24);
            this.CBMesDesde.TabIndex = 1;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(489, 90);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 22);
            this.numericUpDown1.TabIndex = 2;
            // 
            // BtnAplicarFiltros
            // 
            this.BtnAplicarFiltros.Location = new System.Drawing.Point(727, 88);
            this.BtnAplicarFiltros.Name = "BtnAplicarFiltros";
            this.BtnAplicarFiltros.Size = new System.Drawing.Size(75, 23);
            this.BtnAplicarFiltros.TabIndex = 3;
            this.BtnAplicarFiltros.Text = "button1";
            this.BtnAplicarFiltros.UseVisualStyleBackColor = true;
            this.BtnAplicarFiltros.Click += new System.EventHandler(this.BtnAplicarFiltros_Click);
            // 
            // LTotalFacturado
            // 
            this.LTotalFacturado.AutoSize = true;
            this.LTotalFacturado.Location = new System.Drawing.Point(307, 156);
            this.LTotalFacturado.Name = "LTotalFacturado";
            this.LTotalFacturado.Size = new System.Drawing.Size(44, 16);
            this.LTotalFacturado.TabIndex = 4;
            this.LTotalFacturado.Text = "label1";
            // 
            // LTicketPromedio
            // 
            this.LTicketPromedio.AutoSize = true;
            this.LTicketPromedio.Location = new System.Drawing.Point(614, 156);
            this.LTicketPromedio.Name = "LTicketPromedio";
            this.LTicketPromedio.Size = new System.Drawing.Size(44, 16);
            this.LTicketPromedio.TabIndex = 5;
            this.LTicketPromedio.Text = "label2";
            // 
            // LTotalFacturas
            // 
            this.LTotalFacturas.AutoSize = true;
            this.LTotalFacturas.Location = new System.Drawing.Point(454, 156);
            this.LTotalFacturas.Name = "LTotalFacturas";
            this.LTotalFacturas.Size = new System.Drawing.Size(44, 16);
            this.LTotalFacturas.TabIndex = 6;
            this.LTotalFacturas.Text = "label3";
            // 
            // chartEvolucion
            // 
            chartArea2.Name = "ChartArea1";
            this.chartEvolucion.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chartEvolucion.Legends.Add(legend2);
            this.chartEvolucion.Location = new System.Drawing.Point(3, 214);
            this.chartEvolucion.Name = "chartEvolucion";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chartEvolucion.Series.Add(series2);
            this.chartEvolucion.Size = new System.Drawing.Size(638, 300);
            this.chartEvolucion.TabIndex = 7;
            this.chartEvolucion.Text = "chart1";
            // 
            // DGVFacturas
            // 
            this.DGVFacturas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVFacturas.Location = new System.Drawing.Point(757, 316);
            this.DGVFacturas.Name = "DGVFacturas";
            this.DGVFacturas.RowHeadersWidth = 51;
            this.DGVFacturas.RowTemplate.Height = 24;
            this.DGVFacturas.Size = new System.Drawing.Size(240, 150);
            this.DGVFacturas.TabIndex = 8;
            // 
            // CBTipoFiltro
            // 
            this.CBTipoFiltro.FormattingEnabled = true;
            this.CBTipoFiltro.Location = new System.Drawing.Point(38, 90);
            this.CBTipoFiltro.Name = "CBTipoFiltro";
            this.CBTipoFiltro.Size = new System.Drawing.Size(121, 24);
            this.CBTipoFiltro.TabIndex = 9;
            // 
            // CBMesHasta
            // 
            this.CBMesHasta.FormattingEnabled = true;
            this.CBMesHasta.Location = new System.Drawing.Point(338, 90);
            this.CBMesHasta.Name = "CBMesHasta";
            this.CBMesHasta.Size = new System.Drawing.Size(121, 24);
            this.CBMesHasta.TabIndex = 10;
            this.CBMesHasta.SelectedIndexChanged += new System.EventHandler(this.CBMesHasta_SelectedIndexChanged);
            // 
            // UC_Balance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.CBMesHasta);
            this.Controls.Add(this.CBTipoFiltro);
            this.Controls.Add(this.DGVFacturas);
            this.Controls.Add(this.chartEvolucion);
            this.Controls.Add(this.LTotalFacturas);
            this.Controls.Add(this.LTicketPromedio);
            this.Controls.Add(this.LTotalFacturado);
            this.Controls.Add(this.BtnAplicarFiltros);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.CBMesDesde);
            this.Name = "UC_Balance";
            this.Size = new System.Drawing.Size(1470, 730);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartEvolucion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGVFacturas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox CBMesDesde;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Button BtnAplicarFiltros;
        private System.Windows.Forms.Label LTotalFacturado;
        private System.Windows.Forms.Label LTicketPromedio;
        private System.Windows.Forms.Label LTotalFacturas;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartEvolucion;
        private System.Windows.Forms.DataGridView DGVFacturas;
        private System.Windows.Forms.ComboBox CBTipoFiltro;
        private System.Windows.Forms.ComboBox CBMesHasta;
    }
}
