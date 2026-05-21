using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class NotificacionMapper
    {
        private ACCESO acceso;

        public NotificacionMapper()
        {
            acceso = new ACCESO();
        }

        /// <summary>
        /// Persist a subscription for a user+subasta pair
        /// </summary>
        public int InsertarSuscripcion(int idUsuario, int idSubasta)
        {
            acceso = new ACCESO();
            try
            {
                acceso.Abrir();

                List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();
                parametros.Add(acceso.CrearParametro("@id_usuario", idUsuario));
                parametros.Add(acceso.CrearParametro("@id_subasta", idSubasta));
                parametros.Add(acceso.CrearParametro("@fecha", DateTime.Now));

                int resultado = acceso.Escribir("InsertarSuscripcion", parametros);
                return resultado;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en InsertarSuscripcion: {ex.Message}");
                return -1;
            }
            finally
            {
                acceso.Cerrar();
            }
        }

        /// <summary>
        /// Remove a subscription
        /// </summary>
        public int EliminarSuscripcion(int idUsuario, int idSubasta)
        {
            acceso = new ACCESO();
            try
            {
                acceso.Abrir();

                List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();
                parametros.Add(acceso.CrearParametro("@id_usuario", idUsuario));
                parametros.Add(acceso.CrearParametro("@id_subasta", idSubasta));

                int resultado = acceso.Escribir("EliminarSuscripcion", parametros);
                return resultado;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EliminarSuscripcion: {ex.Message}");
                return -1;
            }
            finally
            {
                acceso.Cerrar();
            }
        }

        /// <summary>
        /// Get all subasta IDs a user is subscribed to
        /// </summary>
        public List<int> ObtenerSubastasDeUsuario(int idUsuario)
        {
            acceso = new ACCESO();
            try
            {
                acceso.Abrir();

                List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();
                parametros.Add(acceso.CrearParametro("@id_usuario", idUsuario));

                DataTable tabla = acceso.Leer("ObtenerSuscripcionesDeUsuario", parametros);

                List<int> subastaIds = new List<int>();
                foreach (DataRow fila in tabla.Rows)
                {
                    subastaIds.Add((int)fila["SUBASTA_ID"]);
                }

                return subastaIds;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en ObtenerSubastasDeUsuario: {ex.Message}");
                return new List<int>();
            }
            finally
            {
                acceso.Cerrar();
            }
        }

        /// <summary>
        /// Get all user IDs subscribed to a specific subasta
        /// </summary>
        public List<int> ObtenerUsuariosSuscritos(int idSubasta)
        {
            acceso = new ACCESO();
            try
            {
                acceso.Abrir();

                List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();
                parametros.Add(acceso.CrearParametro("@id_subasta", idSubasta));

                DataTable tabla = acceso.Leer("ObtenerUsuariosSuscritos", parametros);

                List<int> usuarioIds = new List<int>();
                foreach (DataRow fila in tabla.Rows)
                {
                    usuarioIds.Add((int)fila["USUARIO_ID"]);
                }

                return usuarioIds;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en ObtenerUsuariosSuscritos: {ex.Message}");
                return new List<int>();
            }
            finally
            {
                acceso.Cerrar();
            }
        }

        /// <summary>
        /// Store a notification for one user
        /// </summary>
        public int InsertarNotificacion(int idUsuario, int idSubasta, string mensaje)
        {
            acceso = new ACCESO();
            try
            {
                acceso.Abrir();

                List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();
                parametros.Add(acceso.CrearParametro("@id_usuario", idUsuario));
                parametros.Add(acceso.CrearParametro("@id_subasta", idSubasta));
                parametros.Add(acceso.CrearParametro("@mensaje", mensaje));
                parametros.Add(acceso.CrearParametro("@fecha", DateTime.Now));

                int resultado = acceso.Escribir("InsertarNotificacion", parametros);
                return resultado;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en InsertarNotificacion: {ex.Message}");
                return -1;
            }
            finally
            {
                acceso.Cerrar();
            }
        }

        /// <summary>
        /// Get all stored notifications for a user (read + unread)
        /// </summary>
        public List<BE.Notificacion> ObtenerNotificaciones(int idUsuario)
        {
            acceso = new ACCESO();
            try
            {
                acceso.Abrir();

                List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();
                parametros.Add(acceso.CrearParametro("@id_usuario", idUsuario));

                DataTable tabla = acceso.Leer("ObtenerNotificacionesDeUsuario", parametros);

                List<BE.Notificacion> notificaciones = new List<BE.Notificacion>();
                foreach (DataRow fila in tabla.Rows)
                {
                    BE.Notificacion notificacion = new BE.Notificacion();
                    notificacion.Id = (int)fila["NOTIFICACION_ID"];
                    notificacion.SubastaId = (int)fila["SUBASTA_ID"];
                    notificacion.Mensaje = (string)fila["MENSAJE"];
                    notificacion.Fecha = (DateTime)fila["FECHA"];
                    notificacion.Leida = Convert.ToBoolean(fila["LEIDA"]);

                    notificaciones.Add(notificacion);
                }

                return notificaciones;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en ObtenerNotificaciones: {ex.Message}");
                return new List<BE.Notificacion>();
            }
            finally
            {
                acceso.Cerrar();
            }
        }

        /// <summary>
        /// Mark all notifications for a user as read
        /// </summary>
        public int MarcarLeidas(int idUsuario)
        {
            acceso = new ACCESO();
            try
            {
                acceso.Abrir();

                List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();
                parametros.Add(acceso.CrearParametro("@id_usuario", idUsuario));

                int resultado = acceso.Escribir("MarcarNotificacionesLeidas", parametros);
                return resultado;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en MarcarLeidas: {ex.Message}");
                return -1;
            }
            finally
            {
                acceso.Cerrar();
            }
        }

        /// <summary>
        /// Remove all stored notifications for a user+subasta (called on unsubscribe)
        /// </summary>
        public int EliminarNotificacionesDeSubasta(int idUsuario, int idSubasta)
        {
            acceso = new ACCESO();
            try
            {
                acceso.Abrir();

                List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();
                parametros.Add(acceso.CrearParametro("@id_usuario", idUsuario));
                parametros.Add(acceso.CrearParametro("@id_subasta", idSubasta));

                int resultado = acceso.Escribir("EliminarNotificacionesDeSubasta", parametros);
                return resultado;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EliminarNotificacionesDeSubasta: {ex.Message}");
                return -1;
            }
            finally
            {
                acceso.Cerrar();
            }
        }
    }
}
