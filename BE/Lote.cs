using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Lote : UnidadDeVenta
    {
		private List<UnidadDeVenta> articulos;

		public List<UnidadDeVenta> Articulos
		{
			get { return articulos; }
			set { articulos = value; }
		}

        public override string ToString()
        {
            return $"[LOTE] {nombre}";
        }

    }
}
