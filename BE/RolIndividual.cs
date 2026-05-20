using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class RolIndividual : Permiso
    {
        private string descripcion;

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        public override string ToString()
        {
            return $"[ROL] {nombre}";
        }

    }
}
