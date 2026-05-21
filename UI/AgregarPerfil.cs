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
    public partial class AgregarPerfil : Form
    {
        private BLL.PerfilService perfilService;

        public AgregarPerfil()
        {
            InitializeComponent();
            perfilService = new BLL.PerfilService();
        }

        private void buttonAgregarPerfil_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxNombre.Text))
            {
                MessageBox.Show("Por favor, ingrese un nombre para el perfil.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxNombre.Focus();
                return;
            }

            try
            {
                BE.Perfil nuevoPerfil = new BE.Perfil();
                nuevoPerfil.Nombre = textBoxNombre.Text.Trim();

                perfilService.Guardar(nuevoPerfil);

                MessageBox.Show("Perfil agregado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar el perfil: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
