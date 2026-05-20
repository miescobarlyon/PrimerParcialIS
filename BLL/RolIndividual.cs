using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class RolIndividual : PermisoBase<BE.RolIndividual>
    {
        public override string Descripcion(BE.RolIndividual rolIndividual)
        {
            return $"\r\n- {rolIndividual.Nombre}: {rolIndividual.Descripcion}" ?? "";
        }

        public override void Guardar(BE.RolIndividual permiso)
        {
            DAL.RolIndividualMapper mapper = new DAL.RolIndividualMapper();

            if (permiso.Id == 0)
            {
                mapper.Insertar(permiso);
            }
            else
            {
                mapper.Editar(permiso);
            }
        }

        public override void Eliminar(BE.RolIndividual permiso)
        {
            DAL.RolIndividualMapper mapper = new DAL.RolIndividualMapper();
            mapper.Borrar(permiso);
        }

        public override List<BE.RolIndividual> Listar()
        {
            DAL.RolIndividualMapper mapper = new DAL.RolIndividualMapper();
            return mapper.Listar();
        }

    }
}
