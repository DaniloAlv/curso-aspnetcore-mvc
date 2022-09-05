using System;
using System.Collections.Generic;
using System.Text;

namespace AppCurso_ASP.NET_MVC_Core.Business.Models
{
	public class Produto : Entity
	{
		public int FornecedorId { get; set; }
		public string Nome { get; set; }
		public string Descricao { get; set; }
		public decimal Valor { get; set; }
		public bool Ativo { get; set; }
		public DateTime DataCadastro { get; set; }
		public string Imagem { get; set; }

		public Fornecedor Fornecedor { get; set; }
	}
}
