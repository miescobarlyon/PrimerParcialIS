using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Oferta
    {
		private int id;

		public int Id
		{
			get { return id; }
			set { id = value; }
		}

		private Usuario ofertante;

		public Usuario Ofertante
        {
			get { return ofertante; }
			set { ofertante = value; }
		}

		private float monto;

		public float Monto
		{
			get { return monto; }
			set { monto = value; }
		}

		private DateTime fechaHora;

		public DateTime FechaHora
		{
			get { return fechaHora; }
			set { fechaHora = value; }
		}

	}
}
