using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class SessionManager
    {
        private static SessionManager instancia;
        private static object lockObject = new object();
        private BE.Usuario usuarioActual;

        private SessionManager()
        {
            usuarioActual = null;
        }

        public static SessionManager GetInstancia()
        {
            if (instancia == null)
            {
                lock (lockObject)
                {
                    if (instancia == null)
                    {
                        instancia = new SessionManager();
                    }
                }
            }
            return instancia;
        }

        public void Login(BE.Usuario usuario)
        {
            usuarioActual = usuario;
        }

        public void Logout()
        {
            usuarioActual = null;
        }

        public BE.Usuario GetUsuario()
        {
            return usuarioActual;
        }

        public bool EstaAutenticado()
        {
            return usuarioActual != null;
        }
    }
}
