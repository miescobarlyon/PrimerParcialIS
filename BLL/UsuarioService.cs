using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class UsuarioService
    {
        private DAL.UsuarioMapper mapperUsuario;
        private ErrorManagerService errorManager;
        private SessionManager sessionManager;
        private const int MAX_INTENTOS = 3;

        public UsuarioService()
        {
            mapperUsuario = new DAL.UsuarioMapper();
            errorManager = ErrorManagerService.GetInstancia();
            sessionManager = SessionManager.GetInstancia();
        }

        public bool Login(string user, string passwordPlano)
        {
            try
            {
                var credenciales = mapperUsuario.TraerPass(user);
                
                if (credenciales == null)
                {
                    errorManager.ManejarError("Usuario no encontrado.", BE.EnumError.Advertencia);
                    return false;
                }

                string hash = credenciales.Item1;
                string salt = credenciales.Item2;

                if (!SecurityService.Verify(passwordPlano, salt, hash))
                {
                    BE.Usuario usuario = mapperUsuario.TraerUsuario(user);
                    
                    if (usuario == null)
                    {
                        errorManager.ManejarError("Usuario no encontrado.", BE.EnumError.Advertencia);
                        return false;
                    }

                    if (usuario.Bloqueado == 1)
                    {
                        errorManager.ManejarError("Usuario bloqueado. Contacte al administrador.", BE.EnumError.Advertencia);
                        return false;
                    }

                    mapperUsuario.AgregarIntento(usuario);
                    int intentosActuales = mapperUsuario.TraerIntentos(usuario);

                    if (intentosActuales >= MAX_INTENTOS)
                    {
                        mapperUsuario.BloquearUsuario(usuario);
                        errorManager.ManejarError("Se han excedido los intentos de login. Usuario bloqueado.", BE.EnumError.Critico);
                        return false;
                    }

                    int intentosRestantes = MAX_INTENTOS - intentosActuales;
                    errorManager.ManejarError($"Contraseña incorrecta. Le quedan {intentosRestantes} intentos.", BE.EnumError.Advertencia);
                    return false;
                }

                BE.Usuario usuarioAutenticado = mapperUsuario.TraerUsuario(user);

                if (usuarioAutenticado == null)
                {
                    errorManager.ManejarError("Error al obtener datos del usuario.", BE.EnumError.Error);
                    return false;
                }

                if (usuarioAutenticado.Bloqueado == 1)
                {
                    errorManager.ManejarError("Usuario bloqueado. Contacte al administrador.", BE.EnumError.Advertencia);
                    return false;
                }

                mapperUsuario.ReestablecerIntentos(usuarioAutenticado);

                sessionManager.Login(usuarioAutenticado);

                return true;
            }
            catch (Exception ex)
            {
                errorManager.ManejarError(ex, BE.EnumError.Critico);
                return false;
            }
        }

        public static List<BE.Usuario> Listar()
        {
            DAL.UsuarioMapper mapper = new DAL.UsuarioMapper();
            return mapper.Listar();
        }

        public static void Bloquear(BE.Usuario usuario)
        {
            DAL.UsuarioMapper mapper = new DAL.UsuarioMapper();
            mapper.BloquearUsuario(usuario);
        }

        public static bool TienePermiso(BE.Usuario usuario, string nombrePermiso)
        {
            if (usuario == null || usuario.Perfiles == null || usuario.Perfiles.Count == 0)
                return false;

            foreach (BE.Perfil perfil in usuario.Perfiles)
            {
                if (perfil.Permisos != null)
                {
                    foreach (BE.Permiso permiso in perfil.Permisos)
                    {
                        if (permiso.Nombre.Equals(nombrePermiso, StringComparison.OrdinalIgnoreCase))
                            return true;

                    }
                }
            }

            return false;
        }

        public static bool TieneRol(BE.Usuario usuario, string nombreRol)
        {
            if (usuario == null || usuario.Perfiles == null || usuario.Perfiles.Count == 0)
                return false;

            foreach (BE.Perfil perfil in usuario.Perfiles)
            {
                if (perfil.Nombre.Equals(nombreRol, StringComparison.OrdinalIgnoreCase))
                    return true;
            }

            return false;
        }
    }
}
