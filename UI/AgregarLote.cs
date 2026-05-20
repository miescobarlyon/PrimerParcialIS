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
    public partial class AgregarLote : Form
    {
        private BLL.Lote loteService;

        public AgregarLote()
        {
            InitializeComponent();
            loteService = new BLL.Lote();
            
        }

        private void buttonAgregarLote_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxNombre.Text))
            {
                MessageBox.Show("Por favor, ingrese un nombre para el lote.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxNombre.Focus();
                return;
            }

            try
            {
                BE.Lote nuevoLote = new BE.Lote();
                nuevoLote.Nombre = textBoxNombre.Text.Trim();
                nuevoLote.Articulos = new List<BE.UnidadDeVenta>();

                loteService.Gruardar(nuevoLote);

                MessageBox.Show("Lote agregado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar el lote: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
