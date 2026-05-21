using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class UsuarioPerfilService
    {
        public event BE.delEnviarError EnviarError;

        public List<BE.Usuario> ListarUsuarios()
        {
            return BLL.UsuarioService.Listar();
        }

        public List<BE.Perfil> ListarPerfiles()
        {
            DAL.PerfilMapper mapper = new DAL.PerfilMapper();
            return mapper.Listar();
        }

        public List<BE.Perfil> ListarPerfilesDeUsuario(BE.Usuario usuario)
        {
            DAL.PerfilMapper mapper = new DAL.PerfilMapper();
            return mapper.ObtenerPerfilesDeUsuario(usuario.Id);
        }

        public bool AsignarPerfil(BE.Usuario usuario, BE.Perfil perfil)
        {
            // Verifica si este usuario específico ya tiene asignado este perfil específico
            List<BE.Perfil> perfilesActuales = ListarPerfilesDeUsuario(usuario);

            if (perfilesActuales.Any(p => p.Id == perfil.Id))
            {
                OnEnviarError("Este perfil ya está asignado a este usuario.");
                return false;
            }

            // Varios usuarios PUEDEN tener el mismo perfil asignado, esto está permitido
            DAL.PerfilMapper mapper = new DAL.PerfilMapper();
            int resultado = mapper.AsignarPerfilAUsuario(usuario.Id, perfil.Id);

            return resultado > 0;
        }

        public void RemoverPerfil(BE.Usuario usuario, BE.Perfil perfil)
        {
            DAL.PerfilMapper mapper = new DAL.PerfilMapper();
            mapper.RemoverPerfilDeUsuario(usuario.Id, perfil.Id);
        }

        protected virtual void OnEnviarError(string mensaje)
        {
            EnviarError?.Invoke(mensaje);
        }
    }
}
