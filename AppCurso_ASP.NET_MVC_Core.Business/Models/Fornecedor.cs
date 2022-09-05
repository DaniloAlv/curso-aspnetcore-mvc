using System;
using System.Collections.Generic;
using System.Text;

namespace AppCurso_ASP.NET_MVC_Core.Business.Models
{
	public class Fornecedor : Entity
	{
		public string Nome { get; set; }
		public string Documento { get; set; }
		public bool Ativo { get; set; }
		public Endereco Endereco { get; set; }
		public TipoFornecedor TipoFornecedor { get; set; }

		public IEnumerable<Produto> Produtos { get; set; }
	}
}
