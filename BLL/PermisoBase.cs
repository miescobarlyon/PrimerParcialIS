using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public abstract class PermisoBase<T> where T : BE.Permiso
    {
        public event BE.delEnviarError EnviarError;
        public abstract string Descripcion(T permiso);
        public abstract void Guardar(T permiso);
        public abstract void Eliminar(T permiso);
        public virtual List<T> Listar() 
        {
            return new List<T>();
        }

        public List<BE.Permiso> ListarCompleto()
        {
            List<BE.Permiso> resultado = new List<BE.Permiso>();

            BLL.RolCompuesto rolCompuestoService = new RolCompuesto();

            List<BE.RolCompuesto> rolesCompuestos = rolCompuestoService.Listar();
            HashSet<int> permisosEnRolesCompuestos = new HashSet<int>();

            foreach (BE.RolCompuesto rolCompuesto in rolesCompuestos)
            {
                resultado.Add(rolCompuesto);
                ObtenerPermisosEnRolCompuesto(rolCompuesto, permisosEnRolesCompuestos);
            }

            RolIndividual rolIndividualService = new RolIndividual();
            List<BE.RolIndividual> rolesIndividuales = rolIndividualService.Listar();

            foreach (BE.RolIndividual rolIndividual in rolesIndividuales)
            {
                if (!permisosEnRolesCompuestos.Contains(rolIndividual.Id))
                {
                    resultado.Add(rolIndividual);
                }
            }

            return resultado;
        }

        private void ObtenerPermisosEnRolCompuesto(BE.RolCompuesto rolCompuesto, HashSet<int> permisosEnRolesCompuestos)
        {
            if (rolCompuesto.Permisos == null || rolCompuesto.Permisos.Count == 0)
                return;

            foreach (BE.Permiso permiso in rolCompuesto.Permisos)
            {
                if (permiso is BE.RolIndividual rolIndividual)
                {
                    permisosEnRolesCompuestos.Add(rolIndividual.Id);
                }
                else if (permiso is BE.RolCompuesto rolCompuestoAnidado)
                {
                    ObtenerPermisosEnRolCompuesto(rolCompuestoAnidado, permisosEnRolesCompuestos);
                }
            }
        }

        protected void OnEnviarError(string mensaje)
        {
            EnviarError.Invoke(mensaje);
        }
    }
}
