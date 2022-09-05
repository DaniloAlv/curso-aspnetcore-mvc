using AppCurso_ASP.NET_MVC_Core.Business.Models;
using Microsoft.AspNetCore.Mvc.Razor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AppCurso_ASP.NET_MVC_Core.Web.Extensions
{
	public static class RazorExtensions
	{
		public static string FormataDocumento(this RazorPage page, int tipoPessoa, string documento)
		{
			return tipoPessoa == (int)TipoFornecedor.PessoaFisica ? Convert.ToUInt64(documento).ToString(@"000\.000\.000\-00") : Convert.ToUInt64(documento).ToString(@"00\.000\.000\/0000\-00");
		}

		public static string RetornarTipoForncedor(this RazorPage page, int tipoPessoa)
		{
			var tipo = (TipoFornecedor)tipoPessoa;
			return tipo.GetDescription();
		}

		public static string ProdutoAtivo(this RazorPage page, bool ativo)
		{
			return ativo ? "Sim" : "Não";
		}

		public static string FormataCEP(this RazorPage page, string cep)
		{
			return Convert.ToUInt32(cep).ToString(@"00000-000");
		}

		private static string GetDescription(this Enum value)
		{
			FieldInfo info = value.GetType().GetField(value.ToString());
			DescriptionAttribute[] attributes = (DescriptionAttribute[])info.GetCustomAttributes(typeof(DescriptionAttribute), false);

			return attributes[0].Description;
		}
	}
}
