namespace Proyecto_Taller_2
{
    partial class UC_Productos
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
            this.components = new System.ComponentModel.Container();
            this.DGVProductos = new System.Windows.Forms.DataGridView();
            this.CID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CEdicion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CDescripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CPrecio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CTalle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CCantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CCategoria = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CImagen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CEliminar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CActivo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BtnAgregar = new System.Windows.Forms.Button();
            this.TBNombre = new System.Windows.Forms.TextBox();
            this.LNombre = new System.Windows.Forms.Label();
            this.LDescripcion = new System.Windows.Forms.Label();
            this.LVenta = new System.Windows.Forms.Label();
            this.LCantidad = new System.Windows.Forms.Label();
            this.TBDescripcion = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.LCosto = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.BtnCancelar = new System.Windows.Forms.Button();
            this.BtnImagen = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.NUDCosto = new System.Windows.Forms.NumericUpDown();
            this.NUDVenta = new System.Windows.Forms.NumericUpDown();
            this.NUDCantidad = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.DGVProductos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUDCosto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUDVenta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUDCantidad)).BeginInit();
            this.SuspendLayout();
            // 
            // DGVProductos
            // 
            this.DGVProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVProductos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CID,
            this.CNombre,
            this.CEdicion,
            this.CDescripcion,
            this.CPrecio,
            this.CTalle,
            this.CCantidad,
            this.CCategoria,
            this.CImagen,
            this.CEliminar,
            this.CActivo});
            this.DGVProductos.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.DGVProductos.Location = new System.Drawing.Point(0, 515);
            this.DGVProductos.Name = "DGVProductos";
            this.DGVProductos.RowHeadersWidth = 51;
            this.DGVProductos.RowTemplate.Height = 24;
            this.DGVProductos.Size = new System.Drawing.Size(1358, 310);
            this.DGVProductos.TabIndex = 0;
            this.DGVProductos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // CID
            // 
            this.CID.HeaderText = "ID";
            this.CID.MinimumWidth = 6;
            this.CID.Name = "CID";
            this.CID.Width = 125;
            // 
            // CNombre
            // 
            this.CNombre.HeaderText = "Nombre";
            this.CNombre.MinimumWidth = 6;
            this.CNombre.Name = "CNombre";
            this.CNombre.Width = 125;
            // 
            // CEdicion
            // 
            this.CEdicion.HeaderText = "Edicion";
            this.CEdicion.MinimumWidth = 6;
            this.CEdicion.Name = "CEdicion";
            this.CEdicion.Width = 125;
            // 
            // CDescripcion
            // 
            this.CDescripcion.HeaderText = "Descripcion";
            this.CDescripcion.MinimumWidth = 6;
            this.CDescripcion.Name = "CDescripcion";
            this.CDescripcion.Width = 125;
            // 
            // CPrecio
            // 
            this.CPrecio.HeaderText = "Precio($)";
            this.CPrecio.MinimumWidth = 6;
            this.CPrecio.Name = "CPrecio";
            this.CPrecio.Width = 125;
            // 
            // CTalle
            // 
            this.CTalle.HeaderText = "Talle";
            this.CTalle.MinimumWidth = 6;
            this.CTalle.Name = "CTalle";
            this.CTalle.Width = 125;
            // 
            // CCantidad
            // 
            this.CCantidad.HeaderText = "Cantidad";
            this.CCantidad.MinimumWidth = 6;
            this.CCantidad.Name = "CCantidad";
            this.CCantidad.Width = 125;
            // 
            // CCategoria
            // 
            this.CCategoria.HeaderText = "Categoria";
            this.CCategoria.MinimumWidth = 6;
            this.CCategoria.Name = "CCategoria";
            this.CCategoria.Width = 125;
            // 
            // CImagen
            // 
            this.CImagen.HeaderText = "Imagen";
            this.CImagen.MinimumWidth = 6;
            this.CImagen.Name = "CImagen";
            this.CImagen.Width = 125;
            // 
            // CEliminar
            // 
            this.CEliminar.HeaderText = "Eliminar";
            this.CEliminar.MinimumWidth = 6;
            this.CEliminar.Name = "CEliminar";
            this.CEliminar.Width = 125;
            // 
            // CActivo
            // 
            this.CActivo.HeaderText = "Activo (¿?)";
            this.CActivo.MinimumWidth = 6;
            this.CActivo.Name = "CActivo";
            this.CActivo.Width = 125;
            // 
            // BtnAgregar
            // 
            this.BtnAgregar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnAgregar.BackColor = System.Drawing.Color.Green;
            this.BtnAgregar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnAgregar.Font = new System.Drawing.Font("Microsoft YaHei", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAgregar.Location = new System.Drawing.Point(32, 388);
            this.BtnAgregar.Name = "BtnAgregar";
            this.BtnAgregar.Size = new System.Drawing.Size(145, 76);
            this.BtnAgregar.TabIndex = 1;
            this.BtnAgregar.Text = "Agregar";
            this.BtnAgregar.UseVisualStyleBackColor = false;
            this.BtnAgregar.Click += new System.EventHandler(this.button1_Click);
            // 
            // TBNombre
            // 
            this.TBNombre.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TBNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TBNombre.Location = new System.Drawing.Point(201, 60);
            this.TBNombre.Name = "TBNombre";
            this.TBNombre.Size = new System.Drawing.Size(222, 28);
            this.TBNombre.TabIndex = 2;
            this.TBNombre.TextChanged += new System.EventHandler(this.TBNombre_TextChanged);
            this.TBNombre.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TBNombre_KeyPress);
            // 
            // LNombre
            // 
            this.LNombre.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LNombre.AutoSize = true;
            this.LNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LNombre.Location = new System.Drawing.Point(27, 63);
            this.LNombre.Name = "LNombre";
            this.LNombre.Size = new System.Drawing.Size(81, 25);
            this.LNombre.TabIndex = 6;
            this.LNombre.Text = "Nombre";
            // 
            // LDescripcion
            // 
            this.LDescripcion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LDescripcion.AutoSize = true;
            this.LDescripcion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LDescripcion.Location = new System.Drawing.Point(27, 120);
            this.LDescripcion.Name = "LDescripcion";
            this.LDescripcion.Size = new System.Drawing.Size(114, 25);
            this.LDescripcion.TabIndex = 7;
            this.LDescripcion.Text = "Descripcion";
            // 
            // LVenta
            // 
            this.LVenta.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LVenta.AutoSize = true;
            this.LVenta.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LVenta.Location = new System.Drawing.Point(27, 237);
            this.LVenta.Name = "LVenta";
            this.LVenta.Size = new System.Drawing.Size(154, 25);
            this.LVenta.TabIndex = 8;
            this.LVenta.Text = "Precio Venta ($)";
            this.LVenta.Click += new System.EventHandler(this.LPrecio_Click);
            // 
            // LCantidad
            // 
            this.LCantidad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LCantidad.AutoSize = true;
            this.LCantidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LCantidad.Location = new System.Drawing.Point(27, 289);
            this.LCantidad.Name = "LCantidad";
            this.LCantidad.Size = new System.Drawing.Size(91, 25);
            this.LCantidad.TabIndex = 9;
            this.LCantidad.Text = "Cantidad";
            // 
            // TBDescripcion
            // 
            this.TBDescripcion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TBDescripcion.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TBDescripcion.Location = new System.Drawing.Point(201, 110);
            this.TBDescripcion.Multiline = true;
            this.TBDescripcion.Name = "TBDescripcion";
            this.TBDescripcion.Size = new System.Drawing.Size(222, 67);
            this.TBDescripcion.TabIndex = 10;
            this.TBDescripcion.TextChanged += new System.EventHandler(this.TBDescripcion_TextChanged);
            this.TBDescripcion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TBDescripcion_KeyPress);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // LCosto
            // 
            this.LCosto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LCosto.AutoSize = true;
            this.LCosto.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LCosto.Location = new System.Drawing.Point(27, 189);
            this.LCosto.Name = "LCosto";
            this.LCosto.Size = new System.Drawing.Size(154, 25);
            this.LCosto.TabIndex = 14;
            this.LCosto.Text = "Precio Costo ($)";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.DarkGray;
            this.pictureBox1.BackgroundImage = global::Proyecto_Taller_2.Properties.Resources.images1;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(739, 37);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(317, 314);
            this.pictureBox1.TabIndex = 16;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // BtnCancelar
            // 
            this.BtnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnCancelar.BackColor = System.Drawing.Color.Red;
            this.BtnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnCancelar.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancelar.ForeColor = System.Drawing.SystemColors.ControlText;
            this.BtnCancelar.Location = new System.Drawing.Point(278, 388);
            this.BtnCancelar.Name = "BtnCancelar";
            this.BtnCancelar.Size = new System.Drawing.Size(145, 76);
            this.BtnCancelar.TabIndex = 17;
            this.BtnCancelar.Text = "Cancelar";
            this.BtnCancelar.UseVisualStyleBackColor = false;
            this.BtnCancelar.Click += new System.EventHandler(this.BCancelar_Click);
            // 
            // BtnImagen
            // 
            this.BtnImagen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnImagen.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnImagen.Location = new System.Drawing.Point(32, 340);
            this.BtnImagen.Name = "BtnImagen";
            this.BtnImagen.Size = new System.Drawing.Size(391, 30);
            this.BtnImagen.TabIndex = 18;
            this.BtnImagen.Text = "Imagen";
            this.BtnImagen.UseVisualStyleBackColor = true;
            this.BtnImagen.Click += new System.EventHandler(this.BtnImagen_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // NUDCosto
            // 
            this.NUDCosto.DecimalPlaces = 2;
            this.NUDCosto.Location = new System.Drawing.Point(201, 194);
            this.NUDCosto.Maximum = new decimal(new int[] {
            -1981284353,
            -1966660860,
            0,
            0});
            this.NUDCosto.Name = "NUDCosto";
            this.NUDCosto.Size = new System.Drawing.Size(120, 22);
            this.NUDCosto.TabIndex = 20;
            // 
            // NUDVenta
            // 
            this.NUDVenta.DecimalPlaces = 2;
            this.NUDVenta.Location = new System.Drawing.Point(201, 242);
            this.NUDVenta.Maximum = new decimal(new int[] {
            -1981284353,
            -1966660860,
            0,
            0});
            this.NUDVenta.Name = "NUDVenta";
            this.NUDVenta.Size = new System.Drawing.Size(120, 22);
            this.NUDVenta.TabIndex = 22;
            // 
            // NUDCantidad
            // 
            this.NUDCantidad.Location = new System.Drawing.Point(201, 294);
            this.NUDCantidad.Maximum = new decimal(new int[] {
            -727379969,
            232,
            0,
            0});
            this.NUDCantidad.Name = "NUDCantidad";
            this.NUDCantidad.Size = new System.Drawing.Size(120, 22);
            this.NUDCantidad.TabIndex = 23;
            // 
            // UC_Productos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.Controls.Add(this.NUDCantidad);
            this.Controls.Add(this.NUDVenta);
            this.Controls.Add(this.NUDCosto);
            this.Controls.Add(this.BtnImagen);
            this.Controls.Add(this.BtnCancelar);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.LCosto);
            this.Controls.Add(this.TBDescripcion);
            this.Controls.Add(this.LCantidad);
            this.Controls.Add(this.LVenta);
            this.Controls.Add(this.LDescripcion);
            this.Controls.Add(this.LNombre);
            this.Controls.Add(this.TBNombre);
            this.Controls.Add(this.BtnAgregar);
            this.Controls.Add(this.DGVProductos);
            this.MinimumSize = new System.Drawing.Size(400, 300);
            this.Name = "UC_Productos";
            this.Size = new System.Drawing.Size(1358, 825);
            this.Load += new System.EventHandler(this.UC_Productos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGVProductos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUDCosto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUDVenta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUDCantidad)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DGVProductos;
        private System.Windows.Forms.Button BtnAgregar;
        private System.Windows.Forms.TextBox TBNombre;
        private System.Windows.Forms.Label LNombre;
        private System.Windows.Forms.Label LDescripcion;
        private System.Windows.Forms.Label LVenta;
        private System.Windows.Forms.Label LCantidad;
        private System.Windows.Forms.TextBox TBDescripcion;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Label LCosto;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button BtnCancelar;
        private System.Windows.Forms.Button BtnImagen;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.NumericUpDown NUDCosto;
        private System.Windows.Forms.NumericUpDown NUDVenta;
        private System.Windows.Forms.NumericUpDown NUDCantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn CID;
        private System.Windows.Forms.DataGridViewTextBoxColumn CNombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn CEdicion;
        private System.Windows.Forms.DataGridViewTextBoxColumn CDescripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn CPrecio;
        private System.Windows.Forms.DataGridViewTextBoxColumn CTalle;
        private System.Windows.Forms.DataGridViewTextBoxColumn CCantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn CCategoria;
        private System.Windows.Forms.DataGridViewTextBoxColumn CImagen;
        private System.Windows.Forms.DataGridViewTextBoxColumn CEliminar;
        private System.Windows.Forms.DataGridViewTextBoxColumn CActivo;
    }
}
