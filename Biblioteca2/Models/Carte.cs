using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca2.Models
{
	public class Carte
	{
		public int Id { get; set; }

		public string NumeleCartii { get; set; }

		public string NumeAutor { get; set; }

		public string Editura { get; set; }

		public DateTime DataPublicarii { get; set; }

		public int NumarPagini { get; set; }
	}
}
