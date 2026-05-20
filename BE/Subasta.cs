using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Subasta
    {

		public Subasta()
		{
			suscriptores = new List<Usuario>();
			historial = new List<Oferta>();

		}

		private int id;

		public int Id
		{
			get { return id; }
			set { id = value; }
		}

		private UnidadDeVenta articulo;

		public UnidadDeVenta Articulo
		{
			get { return articulo; }
			set { articulo = value; }
		}

		private float precioActual;

		public float PrecioActual
		{
			get { return precioActual; }
			set { precioActual = value; }
		}

		private Usuario ganador;

		public Usuario Ganador
		{
			get { return ganador; }
			set { ganador = value; }
		}

		private EstadoSubasta estado;

		public EstadoSubasta Estado
		{
			get { return estado; }
			set { estado = value; }
		}

		private List<Usuario> suscriptores;

		public List<Usuario> Suscriptores
        {
			get { return suscriptores; }
			set { suscriptores = value; }
		}

		private List<Oferta> historial;

		public List<Oferta> Historial
		{
			get { return historial; }
			set { historial = value; }
		}

        public override string ToString()
        {
            return articulo.Nombre;
        }

	}
}
