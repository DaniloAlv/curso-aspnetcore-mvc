using AppCurso_ASP.NET_MVC_Core.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppCurso_ASP.NET_MVC_Core.Business.Interfaces
{
	public interface IEnderecoRepository : IBaseRepository<Endereco>
	{
		Task<Endereco> GetEnderecoByFornecedor(int idFornecedor);
	}
}
