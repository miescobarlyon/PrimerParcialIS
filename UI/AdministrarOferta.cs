using BE;
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
    public partial class AdministrarOferta : Form, BLL.IObserver
    {
        private BLL.Subasta _subastaManager;
        private BE.Usuario interesado;
        private HashSet<int> subastasSuscritas = new HashSet<int>();

        public AdministrarOferta()
        {
            InitializeComponent();
            _subastaManager = BLL.Subasta.GetInstance();
            interesado = new BE.Usuario { Id = 1, Nombre = "Interesado" };
            FormSubasta.SubastaAbierta += FormSubasta_SubastaAbierta;
        }

        public void Actualizar(BE.Subasta subasta, string evento)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => Actualizar(subasta, evento)));
                return;
            }

            if (!subastasSuscritas.Contains(subasta.Id))
                return;

            labelPrecioActual.Text = $"${subasta.PrecioActual}";
            listBoxNotificaciones.Items.Add($"[{DateTime.Now:HH:mm:ss}] {evento}");
        }

        private void AdministrarOferta_Load(object sender, EventArgs e)
        {
            _subastaManager = BLL.Subasta.GetInstance();
            comboBoxSubastas.DataSource = null;
            comboBoxSubastas.DataSource = _subastaManager.ListarAbiertas();
        }

        private void FormSubasta_SubastaAbierta(object sender, SubastaAbiertaEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => FormSubasta_SubastaAbierta(sender, e)));
                return;
            }

            comboBoxSubastas.DataSource = null;
            comboBoxSubastas.DataSource = _subastaManager.ListarAbiertas();
        }

        private void buttonOfertar_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxSubastas.SelectedItem == null)
                    throw new InvalidOperationException("Primero selecciona una subasta.");

                BE.Subasta subastaSeleccionada = (BE.Subasta)comboBoxSubastas.SelectedItem;

                if (!subastasSuscritas.Contains(subastaSeleccionada.Id))
                    throw new InvalidOperationException($"Debes suscribirte a la subasta antes de ofertar.");

                if (string.IsNullOrWhiteSpace(textBoxMonto.Text))
                    throw new InvalidOperationException("Ingresa un monto válido.");

                float monto = float.Parse(textBoxMonto.Text);
                
                if (monto <= 0)
                    throw new InvalidOperationException("El monto debe ser mayor a 0.");

                _subastaManager.Ofertar(subastaSeleccionada, interesado, monto);
                textBoxMonto.Clear();
            }
            catch (FormatException)
            {
                MessageBox.Show("El monto debe ser un número válido.", "Error de Formato");
            }
            catch (Exception ex) 
            { 
                MessageBox.Show(ex.Message, "Error"); 
            }
        }

        private void buttonSuscribirse_Click(object sender, EventArgs e)
        {
            try
            {
                BE.Subasta subastaSeleccionada = (BE.Subasta)comboBoxSubastas.SelectedItem;

                if (subastaSeleccionada == null)
                {
                    MessageBox.Show("Selecciona una subasta primero.", "Error");
                    return;
                }

                if (subastasSuscritas.Contains(subastaSeleccionada.Id))
                {
                    MessageBox.Show("Ya estás suscrito a esta subasta.", "Aviso");
                    return;
                }

                subastasSuscritas.Add(subastaSeleccionada.Id);

                _subastaManager.Suscribir(subastaSeleccionada, this);
                listBoxNotificaciones.Items.Add($"[{DateTime.Now:HH:mm:ss}] Te has suscrito a la subasta de {subastaSeleccionada.Articulo.Nombre}.");
            }
            catch (Exception ex) 
            { 
                MessageBox.Show(ex.Message, "Error"); 
            }
        }

        private void buttonDesuscribirse_Click(object sender, EventArgs e)
        {
            try
            {
                BE.Subasta subastaSeleccionada = (BE.Subasta)comboBoxSubastas.SelectedItem;

                if (subastaSeleccionada == null)
                {
                    MessageBox.Show("Selecciona una subasta primero.", "Error");
                    return;
                }

                if (!subastasSuscritas.Contains(subastaSeleccionada.Id))
                {
                    MessageBox.Show("No estás suscrito a esta subasta.", "Aviso");
                    return;
                }

                subastasSuscritas.Remove(subastaSeleccionada.Id);

                _subastaManager.Desuscribir(subastaSeleccionada, this);
                listBoxNotificaciones.Items.Add($"[{DateTime.Now:HH:mm:ss}] Te has desuscrito de la subasta de {subastaSeleccionada.Articulo.Nombre}.");
            }
            catch (Exception ex) 
            { 
                MessageBox.Show(ex.Message, "Error"); 
            }
        }
    }
}
