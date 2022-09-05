using AppCurso_ASP.NET_MVC_Core.Business.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AppCurso_ASP.NET_MVC_Core.Data.Context
{
	public class AppCursoDbContext : DbContext
	{
		public AppCursoDbContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<Produto> Produtos { get; set; }
		public DbSet<Fornecedor> Fornecedores { get; set; }
		public DbSet<Endereco> Enderecos { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// caso tenha esquecido de mapear algum campo do tipo string, ele se encarrega de setar o tipo corretamente
			foreach (var property in modelBuilder.Model.GetEntityTypes()
				.SelectMany(ent => ent.GetProperties()
				.Where(p => p.ClrType == typeof(string))))
			{
				property.SetColumnType("varchar(100)");
			}

			modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppCursoDbContext).Assembly);

			// Caso uma entidade mãe seja excluida, esta configuração se encarrega de não excluir os registros que possuem relação
			foreach (var relation in modelBuilder.Model.GetEntityTypes().SelectMany(r => r.GetForeignKeys()))
			{
				relation.DeleteBehavior = DeleteBehavior.ClientSetNull;
			}

			base.OnModelCreating(modelBuilder);
		}
	}
}
