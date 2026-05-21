using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class PermisoService
    {
        public List<BE.Permiso> Listar()
        {
            DAL.PermisoMapper mapper = new DAL.PermisoMapper();
            return mapper.Listar();
        }

        public void Guardar(BE.Permiso permiso)
        {
            DAL.PermisoMapper mapper = new DAL.PermisoMapper();

            if (permiso.Id == 0)
            {
                mapper.Insertar(permiso);
            }
            else
            {
                mapper.Editar(permiso);
            }
        }

        public void Eliminar(BE.Permiso permiso)
        {
            DAL.PermisoMapper mapper = new DAL.PermisoMapper();
            mapper.Borrar(permiso);
        }
    }
}
