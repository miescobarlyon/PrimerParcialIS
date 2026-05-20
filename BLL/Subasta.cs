using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Subasta : ISubject
    {
        private static Subasta _instancia;
        private static readonly object _lock = new object();
        private List<IObserver> observadores = new List<IObserver>();

        private Subasta() { }

        public static Subasta GetInstance()
        {
            lock (_lock)
            {
                if (_instancia == null)
                {
                    _instancia = new Subasta();
                }
                return _instancia;
            }
        }

        public void Suscribir(BE.Subasta subasta, IObserver observer)
        {
            if (!observadores.Contains(observer))
            {
                observadores.Add(observer);
            }
        }

        public void Desuscribir(BE.Subasta subasta, IObserver observer)
        {
            if (observadores.Contains(observer))
            {
                observadores.Remove(observer);
            }
        }

        public void Notificar(BE.Subasta subasta, string evento)
        {
            foreach (IObserver observer in observadores)
            {
                observer.Actualizar(subasta, evento);
            }
        }

        public bool Ofertar(BE.Subasta subasta, BE.Usuario usuario, float monto)
        {
            return BLL.Oferta.GetInstance().ProcesarOferta(subasta, usuario, monto, this);
        }

        public void Cerrar(BE.Subasta subasta)
        {
            lock (this)
            {
                if (subasta.Estado == BE.EstadoSubasta.Cerrada)
                    throw new InvalidOperationException("La subasta ya estaba cerrada.");

                subasta.Estado = BE.EstadoSubasta.Cerrada;

                DAL.SubastaMapper mapper = new DAL.SubastaMapper();
                mapper.CerrarSubasta(subasta);

                string resultado = subasta.Ganador != null
                    ? $"Ganador: {subasta.Ganador.Nombre} | Precio final: ${subasta.PrecioActual}"
                    : "Subasta cerrada sin ofertas.";

                Notificar(subasta, resultado);
            }
        }

        public BE.Subasta Abrir(BE.UnidadDeVenta articulo, float precioBase)
        {
            BE.Subasta subasta = new BE.Subasta
            {
                Articulo = articulo,
                PrecioActual = precioBase,
                Estado = BE.EstadoSubasta.Abierta
            };

            DAL.SubastaMapper mapper = new DAL.SubastaMapper();
            subasta.Id = mapper.Insertar(subasta);

            return subasta;
        }

        public List<BE.Subasta> Listar()
        {
            DAL.SubastaMapper mapper = new DAL.SubastaMapper();
            return mapper.Listar();
        }

        public List<BE.Subasta> ListarAbiertas()
        {
            DAL.SubastaMapper mapper = new DAL.SubastaMapper();
            return mapper.Listar().Where(s => s.Estado == BE.EstadoSubasta.Abierta).ToList();
        }

        public bool TieneSubastaAbierta(BE.UnidadDeVenta unidad)
        {
            return ListarAbiertas().Any(s => s.Articulo.Id == unidad.Id);
        }
    }
}
