namespace UI
{
    partial class IniciarSesion
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
            this.labelUsuario = new System.Windows.Forms.Label();
            this.labelContrasena = new System.Windows.Forms.Label();
            this.textBoxUsuario = new System.Windows.Forms.TextBox();
            this.textBoxContrasena = new System.Windows.Forms.TextBox();
            this.checkBoxOcultar = new System.Windows.Forms.CheckBox();
            this.buttonIniciarSesion = new System.Windows.Forms.Button();
            this.buttonSalir = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelUsuario
            // 
            this.labelUsuario.AutoSize = true;
            this.labelUsuario.Location = new System.Drawing.Point(38, 30);
            this.labelUsuario.Name = "labelUsuario";
            this.labelUsuario.Size = new System.Drawing.Size(43, 13);
            this.labelUsuario.TabIndex = 0;
            this.labelUsuario.Text = "Usuario";
            // 
            // labelContrasena
            // 
            this.labelContrasena.AutoSize = true;
            this.labelContrasena.Location = new System.Drawing.Point(38, 70);
            this.labelContrasena.Name = "labelContrasena";
            this.labelContrasena.Size = new System.Drawing.Size(61, 13);
            this.labelContrasena.TabIndex = 1;
            this.labelContrasena.Text = "Contraseña";
            // 
            // textBoxUsuario
            // 
            this.textBoxUsuario.Location = new System.Drawing.Point(120, 27);
            this.textBoxUsuario.Name = "textBoxUsuario";
            this.textBoxUsuario.Size = new System.Drawing.Size(150, 20);
            this.textBoxUsuario.TabIndex = 2;
            // 
            // textBoxContrasena
            // 
            this.textBoxContrasena.Location = new System.Drawing.Point(120, 67);
            this.textBoxContrasena.Name = "textBoxContrasena";
            this.textBoxContrasena.Size = new System.Drawing.Size(150, 20);
            this.textBoxContrasena.TabIndex = 3;
            // 
            // checkBoxOcultar
            // 
            this.checkBoxOcultar.AutoSize = true;
            this.checkBoxOcultar.Checked = true;
            this.checkBoxOcultar.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxOcultar.Location = new System.Drawing.Point(120, 93);
            this.checkBoxOcultar.Name = "checkBoxOcultar";
            this.checkBoxOcultar.Size = new System.Drawing.Size(84, 17);
            this.checkBoxOcultar.TabIndex = 4;
            this.checkBoxOcultar.Text = "Ocultar texto";
            this.checkBoxOcultar.UseVisualStyleBackColor = true;
            this.checkBoxOcultar.CheckedChanged += new System.EventHandler(this.checkBoxOcultar_CheckedChanged);
            // 
            // buttonIniciarSesion
            // 
            this.buttonIniciarSesion.Location = new System.Drawing.Point(56, 130);
            this.buttonIniciarSesion.Name = "buttonIniciarSesion";
            this.buttonIniciarSesion.Size = new System.Drawing.Size(100, 30);
            this.buttonIniciarSesion.TabIndex = 5;
            this.buttonIniciarSesion.Text = "Iniciar Sesión";
            this.buttonIniciarSesion.UseVisualStyleBackColor = true;
            this.buttonIniciarSesion.Click += new System.EventHandler(this.buttonIniciarSesion_Click);
            // 
            // buttonSalir
            // 
            this.buttonSalir.Location = new System.Drawing.Point(170, 130);
            this.buttonSalir.Name = "buttonSalir";
            this.buttonSalir.Size = new System.Drawing.Size(100, 30);
            this.buttonSalir.TabIndex = 6;
            this.buttonSalir.Text = "Salir";
            this.buttonSalir.UseVisualStyleBackColor = true;
            this.buttonSalir.Click += new System.EventHandler(this.buttonSalir_Click);
            // 
            // IniciarSesion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 180);
            this.Controls.Add(this.buttonSalir);
            this.Controls.Add(this.buttonIniciarSesion);
            this.Controls.Add(this.checkBoxOcultar);
            this.Controls.Add(this.textBoxContrasena);
            this.Controls.Add(this.textBoxUsuario);
            this.Controls.Add(this.labelContrasena);
            this.Controls.Add(this.labelUsuario);
            this.Name = "IniciarSesion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Iniciar Sesión";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelUsuario;
        private System.Windows.Forms.Label labelContrasena;
        private System.Windows.Forms.TextBox textBoxUsuario;
        private System.Windows.Forms.TextBox textBoxContrasena;
        private System.Windows.Forms.CheckBox checkBoxOcultar;
        private System.Windows.Forms.Button buttonIniciarSesion;
        private System.Windows.Forms.Button buttonSalir;
    }
}
