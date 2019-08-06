using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca2.Models
{
	public class Carte
	{
		public int Id { get; set; }

		[Display(Description="Descrierea Numele Cartii", Name="Numele Cartii")]
		public string NumeleCartii { get; set; }

		public string NumeAutor { get; set; }

		public string Editura { get; set; }

		public DateTime DataPublicarii { get; set; }

		public int NumarPagini { get; set; }
	}
}
