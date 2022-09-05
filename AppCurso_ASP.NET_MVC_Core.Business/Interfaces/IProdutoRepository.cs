using AppCurso_ASP.NET_MVC_Core.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppCurso_ASP.NET_MVC_Core.Business.Interfaces
{
	public interface IProdutoRepository : IBaseRepository<Produto>
	{
		Task<List<Produto>> GetProdutosByFornecedor(int idFornecedor);
		Task<List<Produto>> GetProdutosFornecedor();
		Task<Produto> GetProdutoFornecedor(int idProduto);
	}
}
