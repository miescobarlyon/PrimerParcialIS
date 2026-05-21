namespace UI
{
    partial class AdministrarOferta
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBoxSubasta = new System.Windows.Forms.GroupBox();
            this.buttonDesuscribirse = new System.Windows.Forms.Button();
            this.labelUnidad = new System.Windows.Forms.Label();
            this.comboBoxSubastas = new System.Windows.Forms.ComboBox();
            this.buttonSuscribirse = new System.Windows.Forms.Button();
            this.labelPrecioLabel = new System.Windows.Forms.Label();
            this.labelPrecioActual = new System.Windows.Forms.Label();
            this.groupBoxOfertar = new System.Windows.Forms.GroupBox();
            this.labelNombre = new System.Windows.Forms.Label();
            this.textBoxNombreOfertante = new System.Windows.Forms.TextBox();
            this.labelMonto = new System.Windows.Forms.Label();
            this.textBoxMonto = new System.Windows.Forms.TextBox();
            this.buttonOfertar = new System.Windows.Forms.Button();
            this.groupBoxNotificaciones = new System.Windows.Forms.GroupBox();
            this.listBoxNotificaciones = new System.Windows.Forms.ListBox();
            this.groupBoxSubasta.SuspendLayout();
            this.groupBoxOfertar.SuspendLayout();
            this.groupBoxNotificaciones.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxSubasta
            // 
            this.groupBoxSubasta.Controls.Add(this.buttonDesuscribirse);
            this.groupBoxSubasta.Controls.Add(this.labelUnidad);
            this.groupBoxSubasta.Controls.Add(this.comboBoxSubastas);
            this.groupBoxSubasta.Controls.Add(this.buttonSuscribirse);
            this.groupBoxSubasta.Controls.Add(this.labelPrecioLabel);
            this.groupBoxSubasta.Controls.Add(this.labelPrecioActual);
            this.groupBoxSubasta.Location = new System.Drawing.Point(14, 25);
            this.groupBoxSubasta.Name = "groupBoxSubasta";
            this.groupBoxSubasta.Size = new System.Drawing.Size(460, 150);
            this.groupBoxSubasta.TabIndex = 3;
            this.groupBoxSubasta.TabStop = false;
            this.groupBoxSubasta.Text = "Subasta";
            // 
            // buttonDesuscribirse
            // 
            this.buttonDesuscribirse.Location = new System.Drawing.Point(330, 90);
            this.buttonDesuscribirse.Name = "buttonDesuscribirse";
            this.buttonDesuscribirse.Size = new System.Drawing.Size(124, 32);
            this.buttonDesuscribirse.TabIndex = 4;
            this.buttonDesuscribirse.Text = "Desuscribirse";
            this.buttonDesuscribirse.UseVisualStyleBackColor = true;
            this.buttonDesuscribirse.Click += new System.EventHandler(this.buttonDesuscribirse_Click);
            // 
            // labelUnidad
            // 
            this.labelUnidad.AutoSize = true;
            this.labelUnidad.Location = new System.Drawing.Point(16, 32);
            this.labelUnidad.Name = "labelUnidad";
            this.labelUnidad.Size = new System.Drawing.Size(69, 20);
            this.labelUnidad.TabIndex = 0;
            this.labelUnidad.Text = "Subasta";
            // 
            // comboBoxSubastas
            // 
            this.comboBoxSubastas.FormattingEnabled = true;
            this.comboBoxSubastas.Location = new System.Drawing.Point(16, 55);
            this.comboBoxSubastas.Name = "comboBoxSubastas";
            this.comboBoxSubastas.Size = new System.Drawing.Size(300, 28);
            this.comboBoxSubastas.TabIndex = 0;
            // 
            // buttonSuscribirse
            // 
            this.buttonSuscribirse.Location = new System.Drawing.Point(330, 52);
            this.buttonSuscribirse.Name = "buttonSuscribirse";
            this.buttonSuscribirse.Size = new System.Drawing.Size(124, 32);
            this.buttonSuscribirse.TabIndex = 1;
            this.buttonSuscribirse.Text = "Suscribirse";
            this.buttonSuscribirse.UseVisualStyleBackColor = true;
            this.buttonSuscribirse.Click += new System.EventHandler(this.buttonSuscribirse_Click);
            // 
            // labelPrecioLabel
            // 
            this.labelPrecioLabel.AutoSize = true;
            this.labelPrecioLabel.Location = new System.Drawing.Point(16, 105);
            this.labelPrecioLabel.Name = "labelPrecioLabel";
            this.labelPrecioLabel.Size = new System.Drawing.Size(104, 20);
            this.labelPrecioLabel.TabIndex = 2;
            this.labelPrecioLabel.Text = "Precio actual:";
            // 
            // labelPrecioActual
            // 
            this.labelPrecioActual.AutoSize = true;
            this.labelPrecioActual.Location = new System.Drawing.Point(119, 105);
            this.labelPrecioActual.Name = "labelPrecioActual";
            this.labelPrecioActual.Size = new System.Drawing.Size(27, 20);
            this.labelPrecioActual.TabIndex = 3;
            this.labelPrecioActual.Text = "$0";
            // 
            // groupBoxOfertar
            // 
            this.groupBoxOfertar.Controls.Add(this.labelNombre);
            this.groupBoxOfertar.Controls.Add(this.textBoxNombreOfertante);
            this.groupBoxOfertar.Controls.Add(this.labelMonto);
            this.groupBoxOfertar.Controls.Add(this.textBoxMonto);
            this.groupBoxOfertar.Controls.Add(this.buttonOfertar);
            this.groupBoxOfertar.Location = new System.Drawing.Point(14, 188);
            this.groupBoxOfertar.Name = "groupBoxOfertar";
            this.groupBoxOfertar.Size = new System.Drawing.Size(460, 160);
            this.groupBoxOfertar.TabIndex = 4;
            this.groupBoxOfertar.TabStop = false;
            this.groupBoxOfertar.Text = "Realizar oferta";
            // 
            // labelNombre
            // 
            this.labelNombre.AutoSize = true;
            this.labelNombre.Location = new System.Drawing.Point(16, 32);
            this.labelNombre.Name = "labelNombre";
            this.labelNombre.Size = new System.Drawing.Size(138, 20);
            this.labelNombre.TabIndex = 0;
            this.labelNombre.Text = "Nombre ofertante:";
            // 
            // textBoxNombreOfertante
            // 
            this.textBoxNombreOfertante.Location = new System.Drawing.Point(16, 55);
            this.textBoxNombreOfertante.Name = "textBoxNombreOfertante";
            this.textBoxNombreOfertante.ReadOnly = true;
            this.textBoxNombreOfertante.Size = new System.Drawing.Size(200, 26);
            this.textBoxNombreOfertante.TabIndex = 0;
            // 
            // labelMonto
            // 
            this.labelMonto.AutoSize = true;
            this.labelMonto.Location = new System.Drawing.Point(16, 95);
            this.labelMonto.Name = "labelMonto";
            this.labelMonto.Size = new System.Drawing.Size(127, 20);
            this.labelMonto.TabIndex = 1;
            this.labelMonto.Text = "Monto oferta ($):";
            // 
            // textBoxMonto
            // 
            this.textBoxMonto.Location = new System.Drawing.Point(16, 115);
            this.textBoxMonto.Name = "textBoxMonto";
            this.textBoxMonto.Size = new System.Drawing.Size(200, 26);
            this.textBoxMonto.TabIndex = 1;
            // 
            // buttonOfertar
            // 
            this.buttonOfertar.Location = new System.Drawing.Point(240, 52);
            this.buttonOfertar.Name = "buttonOfertar";
            this.buttonOfertar.Size = new System.Drawing.Size(110, 32);
            this.buttonOfertar.TabIndex = 2;
            this.buttonOfertar.Text = "Ofertar";
            this.buttonOfertar.UseVisualStyleBackColor = true;
            this.buttonOfertar.Click += new System.EventHandler(this.buttonOfertar_Click);
            // 
            // groupBoxNotificaciones
            // 
            this.groupBoxNotificaciones.Controls.Add(this.listBoxNotificaciones);
            this.groupBoxNotificaciones.Location = new System.Drawing.Point(492, 25);
            this.groupBoxNotificaciones.Name = "groupBoxNotificaciones";
            this.groupBoxNotificaciones.Size = new System.Drawing.Size(380, 323);
            this.groupBoxNotificaciones.TabIndex = 5;
            this.groupBoxNotificaciones.TabStop = false;
            this.groupBoxNotificaciones.Text = "Notificaciones en tiempo real";
            // 
            // listBoxNotificaciones
            // 
            this.listBoxNotificaciones.FormattingEnabled = true;
            this.listBoxNotificaciones.ItemHeight = 20;
            this.listBoxNotificaciones.Location = new System.Drawing.Point(12, 25);
            this.listBoxNotificaciones.Name = "listBoxNotificaciones";
            this.listBoxNotificaciones.Size = new System.Drawing.Size(354, 284);
            this.listBoxNotificaciones.TabIndex = 0;
            // 
            // AdministrarOferta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(906, 450);
            this.Controls.Add(this.groupBoxSubasta);
            this.Controls.Add(this.groupBoxOfertar);
            this.Controls.Add(this.groupBoxNotificaciones);
            this.Name = "AdministrarOferta";
            this.Text = "AdministrarOferta";
            this.Load += new System.EventHandler(this.AdministrarOferta_Load);
            this.groupBoxSubasta.ResumeLayout(false);
            this.groupBoxSubasta.PerformLayout();
            this.groupBoxOfertar.ResumeLayout(false);
            this.groupBoxOfertar.PerformLayout();
            this.groupBoxNotificaciones.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxSubasta;
        private System.Windows.Forms.Label labelUnidad;
        private System.Windows.Forms.ComboBox comboBoxSubastas;
        private System.Windows.Forms.Button buttonSuscribirse;
        private System.Windows.Forms.Label labelPrecioLabel;
        private System.Windows.Forms.Label labelPrecioActual;
        private System.Windows.Forms.GroupBox groupBoxOfertar;
        private System.Windows.Forms.Label labelNombre;
        private System.Windows.Forms.TextBox textBoxNombreOfertante;
        private System.Windows.Forms.Label labelMonto;
        private System.Windows.Forms.TextBox textBoxMonto;
        private System.Windows.Forms.Button buttonOfertar;
        private System.Windows.Forms.GroupBox groupBoxNotificaciones;
        private System.Windows.Forms.ListBox listBoxNotificaciones;
        private System.Windows.Forms.Button buttonDesuscribirse;
    }
}