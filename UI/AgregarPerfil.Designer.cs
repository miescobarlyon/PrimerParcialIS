namespace UI
{
    partial class AgregarPerfil
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxNombre = new System.Windows.Forms.TextBox();
            this.buttonAgregarPerfil = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nombre";
            // 
            // textBoxNombre
            // 
            this.textBoxNombre.Location = new System.Drawing.Point(31, 61);
            this.textBoxNombre.Name = "textBoxNombre";
            this.textBoxNombre.Size = new System.Drawing.Size(299, 26);
            this.textBoxNombre.TabIndex = 1;
            // 
            // buttonAgregarPerfil
            // 
            this.buttonAgregarPerfil.Location = new System.Drawing.Point(31, 116);
            this.buttonAgregarPerfil.Name = "buttonAgregarPerfil";
            this.buttonAgregarPerfil.Size = new System.Drawing.Size(299, 36);
            this.buttonAgregarPerfil.TabIndex = 9;
            this.buttonAgregarPerfil.Text = "Agregar";
            this.buttonAgregarPerfil.UseVisualStyleBackColor = true;
            this.buttonAgregarPerfil.Click += new System.EventHandler(this.buttonAgregarPerfil_Click);
            // 
            // AgregarPerfil
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(366, 175);
            this.Controls.Add(this.buttonAgregarPerfil);
            this.Controls.Add(this.textBoxNombre);
            this.Controls.Add(this.label1);
            this.Name = "AgregarPerfil";
            this.Text = "Nuevo Perfil";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxNombre;
        private System.Windows.Forms.Button buttonAgregarPerfil;
    }
}
