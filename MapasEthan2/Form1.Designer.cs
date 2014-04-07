namespace MapasEthan2
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.gmap = new GMap.NET.WindowsForms.GMapControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.modoPoligonoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.dividirPoligonoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.divisiónConRectaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.numeroDePuntosMarkersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.borrarPolígonoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.numeroOverlaysToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_Area = new System.Windows.Forms.Label();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gmap
            // 
            this.gmap.Bearing = 0F;
            this.gmap.CanDragMap = true;
            this.gmap.ContextMenuStrip = this.contextMenuStrip1;
            this.gmap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gmap.EmptyTileColor = System.Drawing.Color.Navy;
            this.gmap.GrayScaleMode = false;
            this.gmap.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gmap.LevelsKeepInMemmory = 5;
            this.gmap.Location = new System.Drawing.Point(0, 0);
            this.gmap.Margin = new System.Windows.Forms.Padding(4);
            this.gmap.MarkersEnabled = true;
            this.gmap.MaxZoom = 18;
            this.gmap.MinZoom = 2;
            this.gmap.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.gmap.Name = "gmap";
            this.gmap.NegativeMode = false;
            this.gmap.PolygonsEnabled = true;
            this.gmap.RetryLoadTile = 0;
            this.gmap.RoutesEnabled = true;
            this.gmap.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.gmap.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.gmap.ShowTileGridLines = false;
            this.gmap.Size = new System.Drawing.Size(1482, 1045);
            this.gmap.TabIndex = 0;
            this.gmap.Zoom = 3D;
            this.gmap.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gmap_KeyDown);
            this.gmap.MouseClick += new System.Windows.Forms.MouseEventHandler(this.gmap_MouseClick);
            this.gmap.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gmap_MouseDown);
            this.gmap.MouseMove += new System.Windows.Forms.MouseEventHandler(this.gmap_MouseMove);
            this.gmap.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gmap_MouseUp);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.AccessibleRole = System.Windows.Forms.AccessibleRole.Cursor;
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modoPoligonoToolStripMenuItem,
            this.toolStripSeparator1,
            this.dividirPoligonoToolStripMenuItem,
            this.toolStripSeparator2,
            this.numeroDePuntosMarkersToolStripMenuItem,
            this.borrarPolígonoToolStripMenuItem,
            this.numeroOverlaysToolStripMenuItem,
            this.toolStripSeparator3,
            this.toolStripMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(255, 188);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // modoPoligonoToolStripMenuItem
            // 
            this.modoPoligonoToolStripMenuItem.Name = "modoPoligonoToolStripMenuItem";
            this.modoPoligonoToolStripMenuItem.Size = new System.Drawing.Size(254, 24);
            this.modoPoligonoToolStripMenuItem.Text = "Modo Poligono";
            this.modoPoligonoToolStripMenuItem.Click += new System.EventHandler(this.modoPoligonoToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(251, 6);
            // 
            // dividirPoligonoToolStripMenuItem
            // 
            this.dividirPoligonoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.divisiónConRectaToolStripMenuItem});
            this.dividirPoligonoToolStripMenuItem.Enabled = false;
            this.dividirPoligonoToolStripMenuItem.Name = "dividirPoligonoToolStripMenuItem";
            this.dividirPoligonoToolStripMenuItem.Size = new System.Drawing.Size(254, 24);
            this.dividirPoligonoToolStripMenuItem.Text = "Dividir Poligono";
            this.dividirPoligonoToolStripMenuItem.Click += new System.EventHandler(this.dividirPoligonoToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(200, 24);
            this.toolStripMenuItem2.Text = "División estándar";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // divisiónConRectaToolStripMenuItem
            // 
            this.divisiónConRectaToolStripMenuItem.Name = "divisiónConRectaToolStripMenuItem";
            this.divisiónConRectaToolStripMenuItem.Size = new System.Drawing.Size(200, 24);
            this.divisiónConRectaToolStripMenuItem.Text = "División con Recta";
            this.divisiónConRectaToolStripMenuItem.Click += new System.EventHandler(this.divisiónConRectaToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(251, 6);
            // 
            // numeroDePuntosMarkersToolStripMenuItem
            // 
            this.numeroDePuntosMarkersToolStripMenuItem.Name = "numeroDePuntosMarkersToolStripMenuItem";
            this.numeroDePuntosMarkersToolStripMenuItem.Size = new System.Drawing.Size(254, 24);
            this.numeroDePuntosMarkersToolStripMenuItem.Text = "Numero de puntosMarkers";
            this.numeroDePuntosMarkersToolStripMenuItem.Click += new System.EventHandler(this.numeroDePuntosMarkersToolStripMenuItem_Click);
            // 
            // borrarPolígonoToolStripMenuItem
            // 
            this.borrarPolígonoToolStripMenuItem.Name = "borrarPolígonoToolStripMenuItem";
            this.borrarPolígonoToolStripMenuItem.Size = new System.Drawing.Size(254, 24);
            this.borrarPolígonoToolStripMenuItem.Text = "Borrar Polígono";
            this.borrarPolígonoToolStripMenuItem.Click += new System.EventHandler(this.borrarPolígonoToolStripMenuItem_Click);
            // 
            // numeroOverlaysToolStripMenuItem
            // 
            this.numeroOverlaysToolStripMenuItem.Name = "numeroOverlaysToolStripMenuItem";
            this.numeroOverlaysToolStripMenuItem.Size = new System.Drawing.Size(254, 24);
            this.numeroOverlaysToolStripMenuItem.Text = "NumeroOverlays";
            this.numeroOverlaysToolStripMenuItem.Click += new System.EventHandler(this.numeroOverlaysToolStripMenuItem_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(12, 981);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(84, 23);
            this.checkBox1.TabIndex = 1;
            this.checkBox1.Text = "Markers";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Location = new System.Drawing.Point(12, 1010);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(89, 23);
            this.checkBox2.TabIndex = 2;
            this.checkBox2.Text = "Polygons";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 959);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 19);
            this.label1.TabIndex = 3;
            this.label1.Text = "Area:";
            // 
            // lbl_Area
            // 
            this.lbl_Area.AutoSize = true;
            this.lbl_Area.Location = new System.Drawing.Point(58, 959);
            this.lbl_Area.Name = "lbl_Area";
            this.lbl_Area.Size = new System.Drawing.Size(13, 19);
            this.lbl_Area.TabIndex = 4;
            this.lbl_Area.Text = "-";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(251, 6);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(254, 24);
            this.toolStripMenuItem1.Text = "RESET";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1482, 1045);
            this.Controls.Add(this.lbl_Area);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.gmap);
            this.Font = new System.Drawing.Font("Ubuntu Light", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "MapasEthan V2";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GMap.NET.WindowsForms.GMapControl gmap;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem modoPoligonoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem numeroDePuntosMarkersToolStripMenuItem;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_Area;
        private System.Windows.Forms.ToolStripMenuItem borrarPolígonoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem numeroOverlaysToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dividirPoligonoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem divisiónConRectaToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
    }
}

