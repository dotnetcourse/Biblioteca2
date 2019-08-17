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

		[Required(ErrorMessage ="Camp obligatoriu")]
		[StringLength(50, ErrorMessage = "Numele Cartii trebuie sa fie intre 2 si 50 caracter", MinimumLength = 2)]
		[Display(Name="Numele Cartii")]
		public string NumeleCartii { get; set; }

		[Required]
		[Display(Name="Autor")]
		public string NumeAutor { get; set; }

		//[Display(Name = "Autor")]
		//public int AutorId { get; set; }

		[Display(Name = "Editura")]
		public string Editura { get; set; }

		[Display(Name = "Data Publicarii")]
		[DataType(DataType.Date)]
		public DateTime DataPublicarii { get; set; }

		[Range(20, 20000)]
		[Display(Name = "Numar Pagini")]
		public int NumarPagini { get; set; }

		[StringLength(50, ErrorMessage = "Introduceti un Abstract de minim 50 de caractere")]
		//[DataType(DataType)]
		public string Abstract { get; set; }
	}
}
