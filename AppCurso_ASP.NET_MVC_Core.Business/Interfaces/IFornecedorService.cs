using AppCurso_ASP.NET_MVC_Core.Business.Models;
using System.Threading.Tasks;

namespace AppCurso_ASP.NET_MVC_Core.Business.Interfaces
{
	public interface IFornecedorService
	{
		Task Adicionar(Fornecedor fornecedor);
		Task Atualizar(Fornecedor fornecedor);
		Task Remover(int id);
		Task AtualizarEndereco(Endereco endereco);
	}
}
