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
    public partial class AgregarArticulo : Form
    {
        private BLL.Articulo articuloService;

        public AgregarArticulo()
        {
            InitializeComponent();
            articuloService = new BLL.Articulo();
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void buttonAgregarArticulo_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxNombre.Text))
            {
                MessageBox.Show("Por favor, ingrese un nombre para el artículo.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxNombre.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(textBoxDesc.Text))
            {
                MessageBox.Show("Por favor, ingrese una descripción para el artículo.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxDesc.Focus();
                return;
            }

            if (numericUpDownPrecio.Value <= 0)
            {
                MessageBox.Show("Por favor, ingrese un precio válido (mayor a 0).", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numericUpDownPrecio.Focus();
                return;
            }

            try
            {
                BE.Articulo nuevoArticulo = new BE.Articulo();
                nuevoArticulo.Nombre = textBoxNombre.Text.Trim();
                nuevoArticulo.Descripcion = textBoxDesc.Text.Trim();
                nuevoArticulo.PrecioBase = (float)numericUpDownPrecio.Value;

                articuloService.Gruardar(nuevoArticulo);

                MessageBox.Show("Artículo agregado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar el artículo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
