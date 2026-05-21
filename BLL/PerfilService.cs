using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class PerfilService
    {
        public event BE.delEnviarError EnviarError;

        public void Guardar(BE.Perfil perfil)
        {
            DAL.PerfilMapper mapper = new DAL.PerfilMapper();

            if (perfil.Id == 0)
            {
                mapper.Insertar(perfil);
            }
            else
            {
                mapper.Editar(perfil);
            }
        }

        public void Eliminar(BE.Perfil perfil)
        {
            DAL.PerfilMapper mapper = new DAL.PerfilMapper();
            mapper.Borrar(perfil);
        }

        public List<BE.Perfil> Listar()
        {
            DAL.PerfilMapper mapper = new DAL.PerfilMapper();
            return mapper.Listar();
        }

        public bool AgregarPermiso(BE.Perfil perfil, BE.Permiso permiso)
        {
            if (perfil.Permisos != null)
            {
                foreach (BE.Permiso p in perfil.Permisos)
                {
                    if (p.Id == permiso.Id)
                    {
                        OnEnviarError("Este permiso ya está asignado al perfil.");
                        return false;
                    }
                }
            }

            perfil.Permisos.Add(permiso);
            return true;
        }

        public void QuitarPermiso(BE.Perfil perfil, BE.Permiso permiso)
        {
            if (perfil.Permisos != null && perfil.Permisos.Contains(permiso))
            {
                perfil.Permisos.Remove(permiso);
            }
        }

        protected virtual void OnEnviarError(string mensaje)
        {
            EnviarError?.Invoke(mensaje);
        }
    }
}
