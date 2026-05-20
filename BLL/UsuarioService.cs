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
                // Traer credenciales
                var credenciales = mapperUsuario.TraerPass(user);
                
                if (credenciales == null)
                {
                    errorManager.ManejarError("Usuario no encontrado.", BE.EnumError.Advertencia);
                    return false;
                }

                string hash = credenciales.Item1;
                string salt = credenciales.Item2;

                // Verificar contraseña
                if (!SecurityService.Verify(passwordPlano, salt, hash))
                {
                    // Traer usuario para manejar intentos
                    BE.Usuario usuario = mapperUsuario.TraerUsuario(user);
                    
                    if (usuario == null)
                    {
                        errorManager.ManejarError("Usuario no encontrado.", BE.EnumError.Advertencia);
                        return false;
                    }

                    // Verificar si ya está bloqueado
                    if (usuario.Bloqueado == 1)
                    {
                        errorManager.ManejarError("Usuario bloqueado. Contacte al administrador.", BE.EnumError.Advertencia);
                        return false;
                    }

                    // Agregar intento
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

                // Contraseña correcta, traer usuario completo
                BE.Usuario usuarioAutenticado = mapperUsuario.TraerUsuario(user);

                if (usuarioAutenticado == null)
                {
                    errorManager.ManejarError("Error al obtener datos del usuario.", BE.EnumError.Error);
                    return false;
                }

                // Verificar si está bloqueado
                if (usuarioAutenticado.Bloqueado == 1)
                {
                    errorManager.ManejarError("Usuario bloqueado. Contacte al administrador.", BE.EnumError.Advertencia);
                    return false;
                }

                // Reestablecer intentos
                mapperUsuario.ReestablecerIntentos(usuarioAutenticado);

                // Establecer sesión
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
    }
}
