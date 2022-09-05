using AppCurso_ASP.NET_MVC_Core.Business.Interfaces;
using AppCurso_ASP.NET_MVC_Core.Business.Models;
using AppCurso_ASP.NET_MVC_Core.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCurso_ASP.NET_MVC_Core.Data.Repository
{
	public class ProdutoRepository : BaseRepository<Produto>, IProdutoRepository
	{
		public ProdutoRepository(AppCursoDbContext context) : base(context) {}

		public async Task<Produto> GetProdutoFornecedor(int idProduto)
		{
			return await _context.Produtos.AsNoTracking()
				.Include(p => p.Fornecedor)
				.FirstOrDefaultAsync(p => p.Id.Equals(idProduto));
		}

		public async Task<List<Produto>> GetProdutosByFornecedor(int idFornecedor)
		{
			return await GetListByFilter(p => p.FornecedorId.Equals(idFornecedor));
		}

		public async Task<List<Produto>> GetProdutosFornecedor()
		{
			return await _context.Produtos.AsNoTracking()
				.Include(p => p.Fornecedor)
				.OrderBy(p => p.Nome)
				.ToListAsync();
		}
	}
}
