using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class RolCompuesto : PermisoBase<BE.RolCompuesto>
    {
        private RolIndividual rolIndividualBLL = new RolIndividual();

        public override string Descripcion(BE.RolCompuesto rolCompuesto)
        {
            if (rolCompuesto?.Permisos == null || rolCompuesto.Permisos.Count == 0)
                return string.Empty;

            List<string> descripciones = rolCompuesto.Permisos
                .Select(p =>
                {
                    if (p is BE.RolIndividual)
                        return rolIndividualBLL.Descripcion((BE.RolIndividual)p);
                    else if (p is BE.RolCompuesto)
                        return this.Descripcion((BE.RolCompuesto)p);
                    return string.Empty;
                })
                .Where(d => !string.IsNullOrEmpty(d))
                .ToList();

            return string.Join("\n", descripciones);
        }

        public bool AgregarPermiso(BE.RolCompuesto rolCompuesto, BE.Permiso permisoAAgregar)
        {
            bool ok = true;
            if (!rolCompuesto.Permisos.Contains(permisoAAgregar))
            {
                if (permisoAAgregar is BE.RolCompuesto rolCompuestoPorAgregar)
                {
                    if (VerificarReferenciaCircular(rolCompuesto.Id, rolCompuestoPorAgregar.Id))
                    {
                        OnEnviarError("No se puede agregar este rol compuesto porque ya contiene al rol actual. Evite referencias circulares.");
                        ok = false;
                    }
                    else
                    {
                        rolCompuesto.Permisos.Add(rolCompuestoPorAgregar);
                    }
                }
                else
                {
                    rolCompuesto.Permisos.Add(permisoAAgregar);
                }
            }

            return ok;
        }

        private bool VerificarReferenciaCircular(int idRolCompuestoPadre, int idRolCompuestoHijo)
        {
            DAL.RolCompuestoMapper rolCompuestoMapper = new DAL.RolCompuestoMapper();
            
            List<BE.Permiso> hijosDelRolAAgregar = rolCompuestoMapper.ObtenerHijos(idRolCompuestoHijo);

            return ContieneRolCompuesto(hijosDelRolAAgregar, idRolCompuestoPadre);
        }

        private bool ContieneRolCompuesto(List<BE.Permiso> permisos, int idRolCompuestoBuscado)
        {
            foreach (BE.Permiso permiso in permisos)
            {
                if (permiso is BE.RolCompuesto rolCompuesto)
                {
                    if (rolCompuesto.Id == idRolCompuestoBuscado)
                        return true;

                    DAL.RolCompuestoMapper rolCompuestoMapper = new DAL.RolCompuestoMapper();
                    List<BE.Permiso> hijosDelRol = rolCompuestoMapper.ObtenerHijos(rolCompuesto.Id);
                    
                    if (ContieneRolCompuesto(hijosDelRol, idRolCompuestoBuscado))
                        return true;
                }
            }

            return false;
        }

        public void EliminarPermiso(BE.RolCompuesto rolCompuesto, BE.Permiso permisoAAgregar)
        {
            if (rolCompuesto.Permisos.Contains(permisoAAgregar))
            {
                rolCompuesto.Permisos.Remove(permisoAAgregar);
            }
        }

        public override void Guardar(BE.RolCompuesto permiso)
        {
            DAL.RolCompuestoMapper mapper = new DAL.RolCompuestoMapper();

            if (permiso.Id == 0)
            {
                mapper.Insertar(permiso);
            }
            else
            {
                mapper.Editar(permiso);
            }
        }

        public override void Eliminar(BE.RolCompuesto permiso)
        {
            DAL.RolCompuestoMapper mapper = new DAL.RolCompuestoMapper();
            mapper.Borrar(permiso);
        }

        public override List<BE.RolCompuesto> Listar()
        {
            DAL.RolCompuestoMapper mapper = new DAL.RolCompuestoMapper();
            return mapper.Listar();
        }
    }
}
