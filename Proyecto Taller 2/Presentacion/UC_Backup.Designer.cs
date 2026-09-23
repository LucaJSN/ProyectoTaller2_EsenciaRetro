namespace Proyecto_Taller_2
{
    partial class UC_BackUp
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblTituloPrincipal = new System.Windows.Forms.Label();
            this.panelResumen = new System.Windows.Forms.Panel();
            this.lblEstado = new System.Windows.Forms.Label();
            this.lblUltimaCopia = new System.Windows.Forms.Label();
            this.lblDatosIncluir = new System.Windows.Forms.Label();
            this.panelDestino = new System.Windows.Forms.Panel();
            this.txtDestino = new System.Windows.Forms.TextBox();
            this.btnSeleccionarCarpeta = new System.Windows.Forms.Button();
            this.btnGenerarBackup = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.colFecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHora = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDestino = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTamanio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAccion = new System.Windows.Forms.DataGridViewButtonColumn();
            this.lblTituloHistorial = new System.Windows.Forms.Label();
            this.btnExportarNube = new System.Windows.Forms.Button();
            this.panelResumen.SuspendLayout();
            this.panelDestino.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTituloPrincipal
            // 
            this.lblTituloPrincipal.AutoSize = true;
            this.lblTituloPrincipal.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTituloPrincipal.Location = new System.Drawing.Point(283, 13);
            this.lblTituloPrincipal.Name = "lblTituloPrincipal";
            this.lblTituloPrincipal.Size = new System.Drawing.Size(415, 32);
            this.lblTituloPrincipal.TabIndex = 0;
            this.lblTituloPrincipal.Text = "Generar Nueva Copia de Seguridad";
            this.lblTituloPrincipal.Click += new System.EventHandler(this.lblTituloPrincipal_Click);
            // 
            // panelResumen
            // 
            this.panelResumen.BackColor = System.Drawing.Color.Lavender;
            this.panelResumen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelResumen.Controls.Add(this.lblEstado);
            this.panelResumen.Controls.Add(this.lblUltimaCopia);
            this.panelResumen.Controls.Add(this.lblDatosIncluir);
            this.panelResumen.Location = new System.Drawing.Point(63, 60);
            this.panelResumen.Name = "panelResumen";
            this.panelResumen.Size = new System.Drawing.Size(430, 95);
            this.panelResumen.TabIndex = 1;
            // 
            // lblEstado
            // 
            this.lblEstado.AutoSize = true;
            this.lblEstado.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblEstado.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblEstado.Location = new System.Drawing.Point(15, 62);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(126, 23);
            this.lblEstado.TabIndex = 2;
            this.lblEstado.Text = "✔ Estado: OK";
            // 
            // lblUltimaCopia
            // 
            this.lblUltimaCopia.AutoSize = true;
            this.lblUltimaCopia.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblUltimaCopia.ForeColor = System.Drawing.Color.Black;
            this.lblUltimaCopia.Location = new System.Drawing.Point(15, 37);
            this.lblUltimaCopia.Name = "lblUltimaCopia";
            this.lblUltimaCopia.Size = new System.Drawing.Size(260, 21);
            this.lblUltimaCopia.TabIndex = 1;
            this.lblUltimaCopia.Text = "🕒 Última Copia: 20/09/2026 15:30";
            // 
            // lblDatosIncluir
            // 
            this.lblDatosIncluir.AutoSize = true;
            this.lblDatosIncluir.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblDatosIncluir.ForeColor = System.Drawing.Color.Black;
            this.lblDatosIncluir.Location = new System.Drawing.Point(15, 12);
            this.lblDatosIncluir.Name = "lblDatosIncluir";
            this.lblDatosIncluir.Size = new System.Drawing.Size(348, 21);
            this.lblDatosIncluir.TabIndex = 0;
            this.lblDatosIncluir.Text = "👥 Datos a Incluir: Todos (Ventas, Stock, Clientes)";
            // 
            // panelDestino
            // 
            this.panelDestino.BackColor = System.Drawing.Color.Lavender;
            this.panelDestino.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelDestino.Controls.Add(this.txtDestino);
            this.panelDestino.Controls.Add(this.btnSeleccionarCarpeta);
            this.panelDestino.Location = new System.Drawing.Point(503, 60);
            this.panelDestino.Name = "panelDestino";
            this.panelDestino.Size = new System.Drawing.Size(360, 95);
            this.panelDestino.TabIndex = 2;
            // 
            // txtDestino
            // 
            this.txtDestino.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDestino.Location = new System.Drawing.Point(15, 12);
            this.txtDestino.Name = "txtDestino";
            this.txtDestino.Size = new System.Drawing.Size(330, 30);
            this.txtDestino.TabIndex = 0;
            this.txtDestino.Text = "C:\\CopiasBase\\RetroCamisetas\\";
            // 
            // btnSeleccionarCarpeta
            // 
            this.btnSeleccionarCarpeta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.btnSeleccionarCarpeta.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnSeleccionarCarpeta.ForeColor = System.Drawing.Color.White;
            this.btnSeleccionarCarpeta.Location = new System.Drawing.Point(15, 48);
            this.btnSeleccionarCarpeta.Name = "btnSeleccionarCarpeta";
            this.btnSeleccionarCarpeta.Size = new System.Drawing.Size(330, 38);
            this.btnSeleccionarCarpeta.TabIndex = 1;
            this.btnSeleccionarCarpeta.Text = "📁 Seleccionar Carpeta";
            this.btnSeleccionarCarpeta.UseVisualStyleBackColor = false;
            // 
            // btnGenerarBackup
            // 
            this.btnGenerarBackup.BackColor = System.Drawing.Color.ForestGreen;
            this.btnGenerarBackup.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnGenerarBackup.ForeColor = System.Drawing.Color.White;
            this.btnGenerarBackup.Location = new System.Drawing.Point(932, 107);
            this.btnGenerarBackup.Name = "btnGenerarBackup";
            this.btnGenerarBackup.Size = new System.Drawing.Size(260, 95);
            this.btnGenerarBackup.TabIndex = 3;
            this.btnGenerarBackup.Text = "➕ Generar BACK-UP Completo";
            this.btnGenerarBackup.UseVisualStyleBackColor = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(70)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colFecha,
            this.colHora,
            this.colTipo,
            this.colDestino,
            this.colTamanio,
            this.colAccion});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new System.Drawing.Point(63, 218);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.Size = new System.Drawing.Size(800, 397);
            this.dataGridView1.TabIndex = 4;
            // 
            // colFecha
            // 
            this.colFecha.HeaderText = "Fecha";
            this.colFecha.MinimumWidth = 6;
            this.colFecha.Name = "colFecha";
            this.colFecha.Width = 125;
            // 
            // colHora
            // 
            this.colHora.HeaderText = "Hora";
            this.colHora.MinimumWidth = 6;
            this.colHora.Name = "colHora";
            this.colHora.Width = 125;
            // 
            // colTipo
            // 
            this.colTipo.HeaderText = "Tipo";
            this.colTipo.MinimumWidth = 6;
            this.colTipo.Name = "colTipo";
            this.colTipo.Width = 125;
            // 
            // colDestino
            // 
            this.colDestino.HeaderText = "Destino";
            this.colDestino.MinimumWidth = 6;
            this.colDestino.Name = "colDestino";
            this.colDestino.Width = 125;
            // 
            // colTamanio
            // 
            this.colTamanio.HeaderText = "Tamaño";
            this.colTamanio.MinimumWidth = 6;
            this.colTamanio.Name = "colTamanio";
            this.colTamanio.Width = 125;
            // 
            // colAccion
            // 
            this.colAccion.HeaderText = "Acciones";
            this.colAccion.MinimumWidth = 6;
            this.colAccion.Name = "colAccion";
            this.colAccion.Width = 125;
            // 
            // lblTituloHistorial
            // 
            this.lblTituloHistorial.AutoSize = true;
            this.lblTituloHistorial.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTituloHistorial.Location = new System.Drawing.Point(284, 174);
            this.lblTituloHistorial.Name = "lblTituloHistorial";
            this.lblTituloHistorial.Size = new System.Drawing.Size(442, 28);
            this.lblTituloHistorial.TabIndex = 5;
            this.lblTituloHistorial.Text = "Historial de Copias Realizadas y Restauración";
            // 
            // btnExportarNube
            // 
            this.btnExportarNube.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.btnExportarNube.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnExportarNube.ForeColor = System.Drawing.Color.White;
            this.btnExportarNube.Location = new System.Drawing.Point(932, 426);
            this.btnExportarNube.Name = "btnExportarNube";
            this.btnExportarNube.Size = new System.Drawing.Size(260, 86);
            this.btnExportarNube.TabIndex = 7;
            this.btnExportarNube.Text = "☁️ Exportar Copias a Nube";
            this.btnExportarNube.UseVisualStyleBackColor = false;
            this.btnExportarNube.Click += new System.EventHandler(this.btnExportarNube_Click);
            // 
            // UC_BackUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.btnExportarNube);
            this.Controls.Add(this.lblTituloHistorial);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnGenerarBackup);
            this.Controls.Add(this.panelDestino);
            this.Controls.Add(this.panelResumen);
            this.Controls.Add(this.lblTituloPrincipal);
            this.Name = "UC_BackUp";
            this.Size = new System.Drawing.Size(1317, 719);
            this.panelResumen.ResumeLayout(false);
            this.panelResumen.PerformLayout();
            this.panelDestino.ResumeLayout(false);
            this.panelDestino.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label lblTituloPrincipal;
        private System.Windows.Forms.Panel panelResumen;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.Label lblUltimaCopia;
        private System.Windows.Forms.Label lblDatosIncluir;
        private System.Windows.Forms.Panel panelDestino;
        private System.Windows.Forms.TextBox txtDestino;
        private System.Windows.Forms.Button btnSeleccionarCarpeta;
        private System.Windows.Forms.Button btnGenerarBackup;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHora;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTipo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDestino;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTamanio;
        private System.Windows.Forms.DataGridViewButtonColumn colAccion;
        private System.Windows.Forms.Label lblTituloHistorial;
        private System.Windows.Forms.Button btnExportarNube;
    }
}