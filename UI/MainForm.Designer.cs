namespace UI
{
    partial class MainForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.subastasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rolesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.administrarRolesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.asignarRolesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ofertasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cerrarSesiónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelContenido = new System.Windows.Forms.Panel();
            this.unidadesDeVentaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.subastasToolStripMenuItem,
            this.rolesToolStripMenuItem,
            this.ofertasToolStripMenuItem,
            this.unidadesDeVentaToolStripMenuItem,
            this.cerrarSesiónToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1200, 33);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // subastasToolStripMenuItem
            // 
            this.subastasToolStripMenuItem.Name = "subastasToolStripMenuItem";
            this.subastasToolStripMenuItem.Size = new System.Drawing.Size(99, 29);
            this.subastasToolStripMenuItem.Text = "Subastas";
            this.subastasToolStripMenuItem.Click += new System.EventHandler(this.subastasToolStripMenuItem_Click);
            // 
            // rolesToolStripMenuItem
            // 
            this.rolesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.administrarRolesToolStripMenuItem,
            this.asignarRolesToolStripMenuItem});
            this.rolesToolStripMenuItem.Name = "rolesToolStripMenuItem";
            this.rolesToolStripMenuItem.Size = new System.Drawing.Size(70, 29);
            this.rolesToolStripMenuItem.Text = "Roles";
            // 
            // administrarRolesToolStripMenuItem
            // 
            this.administrarRolesToolStripMenuItem.Name = "administrarRolesToolStripMenuItem";
            this.administrarRolesToolStripMenuItem.Size = new System.Drawing.Size(206, 34);
            this.administrarRolesToolStripMenuItem.Text = "Administrar";
            this.administrarRolesToolStripMenuItem.Click += new System.EventHandler(this.administrarRolesToolStripMenuItem_Click);
            // 
            // asignarRolesToolStripMenuItem
            // 
            this.asignarRolesToolStripMenuItem.Name = "asignarRolesToolStripMenuItem";
            this.asignarRolesToolStripMenuItem.Size = new System.Drawing.Size(206, 34);
            this.asignarRolesToolStripMenuItem.Text = "Asignar";
            this.asignarRolesToolStripMenuItem.Click += new System.EventHandler(this.asignarRolesToolStripMenuItem_Click);
            // 
            // ofertasToolStripMenuItem
            // 
            this.ofertasToolStripMenuItem.Name = "ofertasToolStripMenuItem";
            this.ofertasToolStripMenuItem.Size = new System.Drawing.Size(86, 29);
            this.ofertasToolStripMenuItem.Text = "Ofertas";
            this.ofertasToolStripMenuItem.Click += new System.EventHandler(this.ofertasToolStripMenuItem_Click);
            // 
            // cerrarSesiónToolStripMenuItem
            // 
            this.cerrarSesiónToolStripMenuItem.Name = "cerrarSesiónToolStripMenuItem";
            this.cerrarSesiónToolStripMenuItem.Size = new System.Drawing.Size(132, 29);
            this.cerrarSesiónToolStripMenuItem.Text = "Cerrar Sesión";
            this.cerrarSesiónToolStripMenuItem.Click += new System.EventHandler(this.cerrarSesiónToolStripMenuItem_Click);
            // 
            // panelContenido
            // 
            this.panelContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenido.Location = new System.Drawing.Point(0, 33);
            this.panelContenido.Name = "panelContenido";
            this.panelContenido.Size = new System.Drawing.Size(1200, 667);
            this.panelContenido.TabIndex = 1;
            // 
            // unidadesDeVentaToolStripMenuItem
            // 
            this.unidadesDeVentaToolStripMenuItem.Name = "unidadesDeVentaToolStripMenuItem";
            this.unidadesDeVentaToolStripMenuItem.Size = new System.Drawing.Size(178, 29);
            this.unidadesDeVentaToolStripMenuItem.Text = "Unidades De Venta";
            this.unidadesDeVentaToolStripMenuItem.Click += new System.EventHandler(this.unidadesDeVentaToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.panelContenido);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Panel Principal";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem subastasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rolesToolStripMenuItem;
        private System.Windows.Forms.Panel panelContenido;
        private System.Windows.Forms.ToolStripMenuItem cerrarSesiónToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem administrarRolesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem asignarRolesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ofertasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unidadesDeVentaToolStripMenuItem;
    }
}
