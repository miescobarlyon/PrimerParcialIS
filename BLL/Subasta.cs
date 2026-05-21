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

            // Persist if we have a real user identity
            BE.Usuario usuario = SessionManager.GetInstancia().GetUsuario();
            if (usuario != null && subasta != null && subasta.Id > 0)
            {
                DAL.NotificacionMapper mapper = new DAL.NotificacionMapper();
                mapper.InsertarSuscripcion(usuario.Id, subasta.Id);
            }
        }

        public void Desuscribir(BE.Subasta subasta, IObserver observer)
        {
            if (observadores.Contains(observer))
            {
                observadores.Remove(observer);
            }

            BE.Usuario usuario = SessionManager.GetInstancia().GetUsuario();
            if (usuario != null && subasta != null && subasta.Id > 0)
            {
                DAL.NotificacionMapper mapper = new DAL.NotificacionMapper();
                mapper.EliminarSuscripcion(usuario.Id, subasta.Id);
                mapper.EliminarNotificacionesDeSubasta(usuario.Id, subasta.Id);
            }
        }

        public void Notificar(BE.Subasta subasta, string evento)
        {
            // In-memory delivery to active observers
            foreach (IObserver observer in observadores)
            {
                observer.Actualizar(subasta, evento);
            }

            // Persistent delivery: write to DB for every user subscribed to this subasta
            if (subasta != null && subasta.Id > 0)
            {
                DAL.NotificacionMapper mapper = new DAL.NotificacionMapper();

                // Get all user IDs subscribed to this subasta from DB
                List<int> usuarioIds = mapper.ObtenerUsuariosSuscritos(subasta.Id);

                foreach (int uid in usuarioIds)
                {
                    mapper.InsertarNotificacion(uid, subasta.Id, evento);
                }
            }
        }

        /// <summary>
        /// Restore in-memory subscriptions for subastas still open
        /// </summary>
        public void RestaurarSuscripciones(IObserver observer)
        {
            BE.Usuario usuario = SessionManager.GetInstancia().GetUsuario();
            if (usuario == null) return;

            DAL.NotificacionMapper mapper = new DAL.NotificacionMapper();
            List<int> subastaIds = mapper.ObtenerSubastasDeUsuario(usuario.Id);
            List<BE.Subasta> abiertas = ListarAbiertas();

            foreach (int idSubasta in subastaIds)
            {
                BE.Subasta subasta = abiertas.FirstOrDefault(s => s.Id == idSubasta);
                if (subasta != null)
                {
                    // Re-add to in-memory observers without re-persisting (already in DB)
                    if (!observadores.Contains(observer))
                        observadores.Add(observer);
                }
            }
        }

        /// <summary>
        /// Get stored notifications for the current user
        /// </summary>
        public List<BE.Notificacion> ObtenerNotificacionesGuardadas()
        {
            BE.Usuario usuario = SessionManager.GetInstancia().GetUsuario();
            if (usuario == null) return new List<BE.Notificacion>();

            DAL.NotificacionMapper mapper = new DAL.NotificacionMapper();
            return mapper.ObtenerNotificaciones(usuario.Id);
        }

        /// <summary>
        /// Mark all notifications for current user as read
        /// </summary>
        public void MarcarNotificacionesLeidas()
        {
            BE.Usuario usuario = SessionManager.GetInstancia().GetUsuario();
            if (usuario == null) return;

            DAL.NotificacionMapper mapper = new DAL.NotificacionMapper();
            mapper.MarcarLeidas(usuario.Id);
        }

        /// <summary>
        /// Get the list of subasta IDs the current user is subscribed to in the database
        /// </summary>
        public List<int> ObtenerSubastasPersistadasDeUsuario()
        {
            BE.Usuario usuario = SessionManager.GetInstancia().GetUsuario();
            if (usuario == null) return new List<int>();

            DAL.NotificacionMapper mapper = new DAL.NotificacionMapper();
            return mapper.ObtenerSubastasDeUsuario(usuario.Id);
        }

        /// <summary>
        /// Count unread notifications for current user
        /// </summary>
        public int ContarNoLeidas()
        {
            return ObtenerNotificacionesGuardadas().Count(n => !n.Leida);
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

                subasta.Ganador = ObtenerGanador(subasta);
                mapper.CerrarSubasta(subasta);

                // Create detailed closure notification with winner information
                string resultado;
                if (subasta.Ganador != null)
                {
                    resultado = $"SUBASTA CERRADA: {subasta.Articulo.Nombre} | " +
                                $"Ganador: {subasta.Ganador.Nombre} | " +
                                $"Precio final: ${subasta.PrecioActual}";
                }
                else
                {
                    resultado = $"SUBASTA CERRADA: {subasta.Articulo.Nombre} | Sin ofertas válidas";
                }

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

            // Get current user (admin)
            BE.Usuario usuarioActual = SessionManager.GetInstancia().GetUsuario();

            // ✅ NEW: Admin auto-subscribes to their own auction to receive notifications
            if (usuarioActual != null && subasta.Id > 0)
            {
                DAL.NotificacionMapper notificacionMapper = new DAL.NotificacionMapper();
                notificacionMapper.InsertarSuscripcion(usuarioActual.Id, subasta.Id);
            }

            // Notify all users that a new auction has opened
            string mensajeApertura = $"Nueva subasta abierta: {articulo.Nombre} | Precio base: ${precioBase}";

            // Send to all active observers first
            foreach (IObserver observer in observadores)
            {
                observer.Actualizar(subasta, mensajeApertura);
            }

            // Persist notification for ALL users (so they see it even if not logged in)
            if (subasta != null && subasta.Id > 0 && usuarioActual != null)
            {
                DAL.NotificacionMapper notificacionMapper = new DAL.NotificacionMapper();

                // Get all users in the system to notify them of new auction
                DAL.UsuarioMapper usuarioMapper = new DAL.UsuarioMapper();
                List<BE.Usuario> todosLosUsuarios = usuarioMapper.Listar();

                foreach (BE.Usuario usuario in todosLosUsuarios)
                {
                    // Include everyone (including admin) so they know about all auctions
                    notificacionMapper.InsertarNotificacion(usuario.Id, subasta.Id, mensajeApertura);
                }
            }

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

        public BE.Usuario ObtenerGanador(BE.Subasta subasta)
        {
            DAL.SubastaMapper mapper = new DAL.SubastaMapper();
            List<BE.Oferta> ofertas = mapper.ListarOfertas(subasta);

            BE.Usuario ganador = ofertas
                .OrderByDescending(o => o.Monto)
                .ThenByDescending(o => o.FechaHora)
                .Select(o => o.Ofertante)
                .FirstOrDefault();
            return ganador;

        }


    }

}
