using AppCurso_ASP.NET_MVC_Core.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppCurso_ASP.NET_MVC_Core.Data.Mappings
{
	public class ProdutoMapping : IEntityTypeConfiguration<Produto>
	{
		public void Configure(EntityTypeBuilder<Produto> builder)
		{
			builder.HasKey(p => p.Id);

			builder.Property(p => p.Nome)
				.IsRequired()
				.HasColumnType("varchar(75)");

			builder.Property(p => p.Descricao)
				.HasColumnType("varchar(200)");

			builder.Property(p => p.Ativo)
				.IsRequired()
				.HasDefaultValue(false);

			builder.Property(p => p.Valor)
				.IsRequired()
				.HasColumnType("decimal(6,2)");

			builder.Property(p => p.DataCadastro);

			builder.Property(p => p.Imagem)
				.HasColumnType("varchar(100)");

			builder.Property(p => p.FornecedorId)
				.IsRequired();

			builder.HasOne(p => p.Fornecedor)
				.WithMany(f => f.Produtos)
				.HasForeignKey(p => p.FornecedorId);
		}
	}
}
