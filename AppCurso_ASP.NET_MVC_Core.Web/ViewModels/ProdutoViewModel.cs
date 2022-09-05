using AppCurso_ASP.NET_MVC_Core.Web.Extensions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppCurso_ASP.NET_MVC_Core.Web.ViewModels
{
	public class ProdutoViewModel
	{
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = "O campo {0} é obrigatório!")]
		[StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres.", MinimumLength = 3)]
		public string Nome { get; set; }

		[DisplayName("Descrição")]
		[Required(ErrorMessage = "O campo {0} é obrigatório!")]
		[StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres.", MinimumLength = 3)]
		public string Descricao { get; set; }

		[Moeda]
		[Required(ErrorMessage = "O campo {0} é obrigatório!")]
		public decimal Valor { get; set; }

		[DisplayName("Ativo ?")]
		public bool Ativo { get; set; }

		[ScaffoldColumn(false)]
		[DisplayName("Data de Cadastro")]
		public DateTime DataCadastro { get; set; }

		[DisplayName("Imagem")]
		public IFormFile ImagemUpload { get; set; }
		public string Imagem { get; set; }

		[DisplayName("Fornecedor")]
		[Required(ErrorMessage = "O campo {0} é obrigatório!")]
		public int FornecedorId { get; set; }
		public IEnumerable<FornecedorViewModel> Fornecedores { get; set; }
		public FornecedorViewModel Fornecedor { get; set; }
	}
}
