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
	public class FornecedorRepository : BaseRepository<Fornecedor>, IFornecedorRepository
	{
		public FornecedorRepository(AppCursoDbContext context) : base(context) {}

		public async Task<Fornecedor> GetFornecedorByEndereco(int idFornecedor)
		{
			return await _context.Fornecedores.AsNoTracking()
				.Include(f => f.Endereco)
				.FirstOrDefaultAsync(f => f.Id.Equals(idFornecedor));
		}

		public async Task<Fornecedor> GetFornecedorByProdutosEndereco(int id)
		{
			return await _context.Fornecedores.AsNoTracking()
				.Include(f => f.Endereco)
				.Include(f => f.Produtos)
				.FirstOrDefaultAsync(f => f.Id.Equals(id));
		}
	}
}
