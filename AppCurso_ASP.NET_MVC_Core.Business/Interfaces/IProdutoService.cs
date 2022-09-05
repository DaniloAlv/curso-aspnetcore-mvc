using AppCurso_ASP.NET_MVC_Core.Business.Models;
using System.Threading.Tasks;

namespace AppCurso_ASP.NET_MVC_Core.Business.Interfaces
{
	public interface IProdutoService
	{
		Task Adicionar(Produto produto);
		Task Atualizar(Produto produto);
		Task Remover(int id);
	}
}
