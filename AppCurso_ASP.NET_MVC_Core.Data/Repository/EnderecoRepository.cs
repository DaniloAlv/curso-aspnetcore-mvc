using AppCurso_ASP.NET_MVC_Core.Business.Interfaces;
using AppCurso_ASP.NET_MVC_Core.Business.Models;
using AppCurso_ASP.NET_MVC_Core.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppCurso_ASP.NET_MVC_Core.Data.Repository
{
	public class EnderecoRepository : BaseRepository<Endereco>, IEnderecoRepository
	{
		public EnderecoRepository(AppCursoDbContext context) : base(context){}

		public async Task<Endereco> GetEnderecoByFornecedor(int idFornecedor)
		{
			return await GetByFilter(e => e.FornecedorId.Equals(idFornecedor));
		}
	}
}
