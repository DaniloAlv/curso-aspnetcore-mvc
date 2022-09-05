using AppCurso_ASP.NET_MVC_Core.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppCurso_ASP.NET_MVC_Core.Business.Interfaces
{
	public interface IFornecedorRepository : IBaseRepository<Fornecedor>
	{
		Task<Fornecedor> GetFornecedorByEndereco(int idEndereco);
		Task<Fornecedor> GetFornecedorByProdutosEndereco(int id);
	}
}
