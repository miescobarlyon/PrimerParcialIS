using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Oferta
    {
        private static Oferta _instancia;
        private static readonly object _lock = new object();

        private Oferta() { }

        public static Oferta GetInstance()
        {
            lock (_lock)
            {
                if (_instancia == null)
                {
                    _instancia = new Oferta();
                }
                return _instancia;
            }
        }

        public bool ProcesarOferta(BE.Subasta subasta, BE.Usuario usuario, float monto, Subasta subastaManager)
        {
            lock (_lock)
            {
                if (subasta.Estado == BE.EstadoSubasta.Cerrada)
                    throw new InvalidOperationException("La subasta ya está cerrada.");

                if (monto <= subasta.PrecioActual)
                    throw new ArgumentException(
                        $"La oferta debe superar el precio actual (${subasta.PrecioActual}).");

                subasta.PrecioActual = monto;
                subasta.Ganador = usuario;

                BE.Oferta oferta = new BE.Oferta
                {
                    Ofertante = usuario,
                    Monto = monto,
                    FechaHora = DateTime.Now
                };
                subasta.Historial.Add(oferta);

                DAL.SubastaMapper mapper = new DAL.SubastaMapper();
                mapper.RegistrarOferta(subasta, oferta);

                subastaManager.Notificar(subasta, $"Nueva oferta de {usuario.Nombre}: ${monto}");
                return true;
            }
        }

    }
}
