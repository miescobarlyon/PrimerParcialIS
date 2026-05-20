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
    public partial class FormSubasta : Form, BLL.IObserver
    {
        public static event EventHandler<SubastaAbiertaEventArgs> SubastaAbierta;

        private BE.Subasta _subastaActual;
        private BLL.Subasta _subastaManager;

        public FormSubasta() 
        {
            InitializeComponent();
            AdministrarOferta oferta = new AdministrarOferta();
            oferta.Show();
            oferta = new AdministrarOferta();
            oferta.Show();
        }

        private void FormSubasta_Load(object sender, EventArgs e)
        {
            _subastaManager = BLL.Subasta.GetInstance();
            BLL.Lote loteBLL = new BLL.Lote();
            comboBoxUnidades.DataSource = loteBLL.ListarCompleto();
            comboBoxUnidades.DisplayMember = "Nombre";

            SuscribirseASubastasAbiertas();
        }

        private void SuscribirseASubastasAbiertas()
        {
            List<BE.Subasta> subastasAbiertas = _subastaManager.ListarAbiertas();
            foreach (BE.Subasta subasta in subastasAbiertas)
            {
                _subastaManager.Suscribir(subasta, this);
                listBoxNotificaciones.Items.Add($"[{DateTime.Now:HH:mm:ss}] Suscrito a subasta existente: {subasta.Articulo.Nombre}");
            }
        }

        private void buttonAbrirSubasta_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxUnidades.SelectedItem == null)
                    throw new InvalidOperationException("Selecciona una unidad de venta.");

                BE.UnidadDeVenta unidad = (BE.UnidadDeVenta)comboBoxUnidades.SelectedItem;

                if (_subastaManager.TieneSubastaAbierta(unidad))
                    throw new InvalidOperationException($"Ya existe una subasta abierta para '{unidad.Nombre}'.");

                float precioBase = unidad is BE.Lote l
                    ? new BLL.Lote().PrecioBase(l)
                    : new BLL.Articulo().PrecioBase((BE.Articulo)unidad);

                _subastaActual = _subastaManager.Abrir(unidad, precioBase);

                _subastaManager.Suscribir(_subastaActual, this);

                labelPrecioActual.Text = $"${_subastaActual.PrecioActual}";
                listBoxNotificaciones.Items.Clear();
                listBoxNotificaciones.Items.Add("Subasta abierta.");

                OnSubastaAbierta(_subastaActual);
            }
            catch (Exception ex) 
            { 
                MessageBox.Show(ex.Message, "Error al abrir subasta"); 
            }
        }

        private void buttonCerrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxUnidades.SelectedItem == null) return;
                _subastaActual = comboBoxUnidades.SelectedItem as BE.Subasta;
                _subastaManager.Cerrar(_subastaActual);
                _subastaManager.Desuscribir(_subastaActual, this);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        public void Actualizar(BE.Subasta subasta, string evento)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => Actualizar(subasta, evento)));
                return;
            }
            labelPrecioActual.Text = $"${subasta.PrecioActual}";
            listBoxNotificaciones.Items.Add($"[{DateTime.Now:HH:mm:ss}] {evento}");
        }

        protected virtual void OnSubastaAbierta(BE.Subasta subasta)
        {
            SubastaAbierta?.Invoke(this, new SubastaAbiertaEventArgs(subasta));
        }

        private void comboBoxUnidades_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBoxUnidades.SelectedItem != null)
            {
                _subastaActual = comboBoxUnidades.SelectedItem as BE.Subasta;
            }
        }
    }

    public class SubastaAbiertaEventArgs : EventArgs
    {
        public BE.Subasta SubastaAbierta { get; }

        public SubastaAbiertaEventArgs(BE.Subasta subasta)
        {
            SubastaAbierta = subasta;
        }
    }
}
