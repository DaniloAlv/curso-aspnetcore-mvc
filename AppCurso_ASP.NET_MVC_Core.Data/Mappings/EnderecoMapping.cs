using AppCurso_ASP.NET_MVC_Core.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppCurso_ASP.NET_MVC_Core.Data.Mappings
{
	public class EnderecoMapping : IEntityTypeConfiguration<Endereco>
	{
		public void Configure(EntityTypeBuilder<Endereco> builder)
		{
			builder.HasKey(p => p.Id);

			builder.Property(p => p.Logradouro)
				.IsRequired()
				.HasColumnType("varchar(100)");

			builder.Property(p => p.Numero)
				.IsRequired()
				.HasColumnType("varchar(50)");

			builder.Property(p => p.CEP)
				.IsRequired()
				.HasColumnType("varchar(8)")
				.IsFixedLength();

			builder.Property(p => p.Complemento)
				.IsRequired()
				.HasColumnType("varchar(100)");

			builder.Property(p => p.Bairro)
				.IsRequired()
				.HasColumnType("varchar(50)");

			builder.Property(p => p.Cidade)
				.IsRequired()
				.HasColumnType("varchar(50)");

			builder.Property(p => p.Estado)
				.IsRequired()
				.IsFixedLength()
				.HasColumnType("varchar(2)");

			builder.Property(p => p.FornecedorId)
				.IsRequired();

			builder.HasOne(e => e.Fornecedor)
				.WithOne(f => f.Endereco);
		}
	}
}
