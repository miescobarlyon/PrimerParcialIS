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
        private HashSet<int> subastasSuscritas = new HashSet<int>();

        public FormSubasta() 
        {
            InitializeComponent();
        }

        private void FormSubasta_Load(object sender, EventArgs e)
        {
            _subastaManager = BLL.Subasta.GetInstance();
            BLL.Lote loteBLL = new BLL.Lote();
            comboBoxUnidades.DataSource = loteBLL.ListarCompleto();
            comboBoxUnidades.DisplayMember = "Nombre";

            SuscribirseASubastasAbiertas();
            CargarNotificacionesGuardadas();
        }

        private void SuscribirseASubastasAbiertas()
        {
            List<BE.Subasta> subastasAbiertas = _subastaManager.ListarAbiertas();
            foreach (BE.Subasta subasta in subastasAbiertas)
            {
                _subastaManager.Suscribir(subasta, this);
                subastasSuscritas.Add(subasta.Id);
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
                subastasSuscritas.Add(_subastaActual.Id);

                labelPrecioActual.Text = $"${_subastaActual.PrecioActual}";
                listBoxNotificaciones.Items.Add($"[{DateTime.Now:HH:mm:ss}] Subasta abierta exitosamente para {unidad.Nombre}");

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
                BE.UnidadDeVenta unidad = (BE.UnidadDeVenta)comboBoxUnidades.SelectedItem;                

                _subastaActual = (from s in _subastaManager.ListarAbiertas()
                                   where s.Articulo.Id == unidad.Id
                                   select s).FirstOrDefault();

                if (_subastaActual == null)
                {
                    MessageBox.Show($"No hay una subasta abierta para '{unidad.Nombre}'.", "Error al cerrar subasta");
                    return;
                }

                _subastaManager.Cerrar(_subastaActual);
                
                if (_subastaActual != null)
                    subastasSuscritas.Remove(_subastaActual.Id);
                    
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

            if (!subastasSuscritas.Contains(subasta.Id))
                return;

            labelPrecioActual.Text = $"${subasta.PrecioActual}";
            listBoxNotificaciones.Items.Add($"[{DateTime.Now:HH:mm:ss}] {evento}");

            if (listBoxNotificaciones.Items.Count > 0)
                listBoxNotificaciones.TopIndex = listBoxNotificaciones.Items.Count - 1;
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
                if (_subastaActual != null)
                {
                    labelPrecioActual.Text = $"${_subastaActual.PrecioActual}";
                }
            }
        }

        private void CargarNotificacionesGuardadas()
        {
            BLL.Subasta _subastaManager = BLL.Subasta.GetInstance();
            List<BE.Notificacion> guardadas = _subastaManager.ObtenerNotificacionesGuardadas();

            listBoxNotificaciones.Items.Clear();

            foreach (BE.Notificacion n in guardadas)
            {
                string prefijo = n.Leida ? "[LEÍDA]" : "[NUEVA]";
                listBoxNotificaciones.Items.Add(
                    $"{prefijo} [{n.Fecha:dd/MM/yyyy HH:mm:ss}] {n.Mensaje}");
            }

            _subastaManager.MarcarNotificacionesLeidas();

            if (listBoxNotificaciones.Items.Count > 0)
                listBoxNotificaciones.TopIndex = listBoxNotificaciones.Items.Count - 1;
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
