using AppCurso_ASP.NET_MVC_Core.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppCurso_ASP.NET_MVC_Core.Data.Mappings
{
	public class FornecedorMapping : IEntityTypeConfiguration<Fornecedor>
	{
		public void Configure(EntityTypeBuilder<Fornecedor> builder)
		{
			builder.HasKey(f => f.Id);

			builder.Property(f => f.Nome)
				.IsRequired()
				.HasColumnType("varchar(150)");

			builder.Property(f => f.TipoFornecedor)
				.IsRequired()
				.HasConversion<int>();

			builder.Property(f => f.Documento)
				.IsRequired()
				.HasColumnType("varchar(14)");

			builder.Property(f => f.Ativo)
				.HasDefaultValue(false);

			builder.HasOne(f => f.Endereco)
				.WithOne(e => e.Fornecedor);

			builder.HasMany(f => f.Produtos)
				.WithOne(p => p.Fornecedor)
				.HasForeignKey(p => p.FornecedorId);
		}
	}
}
