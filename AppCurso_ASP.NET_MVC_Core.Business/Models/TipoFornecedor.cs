using System.ComponentModel;

namespace AppCurso_ASP.NET_MVC_Core.Business.Models
{
	public enum TipoFornecedor
	{
		[Description("Pessoa Física")]
		PessoaFisica = 1,

		[Description("Pessoa Jurídica")]
		PessoaJuridica = 2
	}
}
