using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppCurso_ASP.NET_MVC_Core.Web.ViewModels
{
	public class EnderecoViewModel
	{
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = "O campo {0} é obrigatório!")]
		[StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres.", MinimumLength = 3)]
		public string Logradouro { get; set; }

		[Required(ErrorMessage = "O campo {0} é obrigatório!")]
		[StringLength(50, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres.", MinimumLength = 3)]
		public string Numero { get; set; }

		public string Complemento { get; set; }

		[Required(ErrorMessage = "O campo {0} é obrigatório!")]
		[StringLength(8, ErrorMessage = "O campo {0} precisa ter {1} caracteres.", MinimumLength = 8)]
		public string CEP { get; set; }

		[Required(ErrorMessage = "O campo {0} é obrigatório!")]
		[StringLength(50, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres.", MinimumLength = 3)]
		public string Bairro { get; set; }

		[Required(ErrorMessage = "O campo {0} é obrigatório!")]
		[StringLength(50, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres.", MinimumLength = 3)]
		public string Cidade { get; set; }

		[Required(ErrorMessage = "O campo {0} é obrigatório!")]
		[StringLength(2, ErrorMessage = "O campo {0} precisa ter {1} caracteres.", MinimumLength = 2)]
		public string Estado { get; set; }

		[HiddenInput]
		public int FornecedorId { get; set; }
		public FornecedorViewModel Fornecedor { get; set; }
	}
}
