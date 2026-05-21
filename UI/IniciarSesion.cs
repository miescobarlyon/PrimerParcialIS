using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI
{
    public partial class IniciarSesion : Form
    {
        private BLL.UsuarioService usuarioService;
        private BLL.ErrorManagerService errorManager;

        public IniciarSesion()
        {
            InitializeComponent();
            usuarioService = new BLL.UsuarioService();
            errorManager = BLL.ErrorManagerService.GetInstancia();
            
            // Suscribirse al evento de error
            errorManager.OnOcurrioError += MostrarError;

            // La contraseña se oculta por defecto
            textBoxContrasena.UseSystemPasswordChar = true;
        }

        private void MostrarError(object sender, BE.Error e)
        {
            MessageBoxIcon icono = MessageBoxIcon.Information;

            switch (e.Tipo)
            {
                case BE.EnumError.Info:
                    icono = MessageBoxIcon.Information;
                    break;
                case BE.EnumError.Advertencia:
                    icono = MessageBoxIcon.Warning;
                    break;
                case BE.EnumError.Error:
                    icono = MessageBoxIcon.Error;
                    break;
                case BE.EnumError.Critico:
                    icono = MessageBoxIcon.Stop;
                    break;
            }

            MessageBox.Show(e.Mensaje, "Información del Sistema", MessageBoxButtons.OK, icono);
        }

        private void checkBoxOcultar_CheckedChanged(object sender, EventArgs e)
        {
            textBoxContrasena.UseSystemPasswordChar = checkBoxOcultar.Checked;
        }

        private void buttonIniciarSesion_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxUsuario.Text))
            {
                errorManager.ManejarError("Ingrese un nombre de usuario.", BE.EnumError.Advertencia);
                textBoxUsuario.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(textBoxContrasena.Text))
            {
                errorManager.ManejarError("Ingrese una contraseña.", BE.EnumError.Advertencia);
                textBoxContrasena.Focus();
                return;
            }

            bool loginExitoso = usuarioService.Login(textBoxUsuario.Text, textBoxContrasena.Text);

            if (loginExitoso)
            {
                errorManager.ManejarError("Login exitoso.", BE.EnumError.Info);
                FormAdmin formPrincipal = new FormAdmin();
                formPrincipal.Show();
                this.Hide();
            }
        }

        private void buttonSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
