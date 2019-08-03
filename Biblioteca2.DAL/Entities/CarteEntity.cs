using System;
using System.Collections.Generic;
using System.Text;

namespace Biblioteca2.DAL.Entities
{
	public class CarteEntity
	{
		public int Id { get; set; }

		public string NumeleCartii { get; set; }

		public string NumeAutor { get; set; }

		public string Editura { get; set; }

		public DateTime DataPublicarii { get; set; }

		public int NumarPagini { get; set; }
	}
}
