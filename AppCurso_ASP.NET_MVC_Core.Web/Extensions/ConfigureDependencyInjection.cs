using AppCurso_ASP.NET_MVC_Core.Business.Interfaces;
using AppCurso_ASP.NET_MVC_Core.Business.Notificacoes;
using AppCurso_ASP.NET_MVC_Core.Business.Services;
using AppCurso_ASP.NET_MVC_Core.Data.Repository;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;

namespace AppCurso_ASP.NET_MVC_Core.Web.Extensions
{
	public static class ConfigureDependencyInjection
	{
		public static void DependencyInjection(this IServiceCollection services)
		{
			services.AddScoped<IProdutoRepository, ProdutoRepository>();
			services.AddScoped<IFornecedorRepository, FornecedorRepository>();
			services.AddScoped<IEnderecoRepository, EnderecoRepository>();
			services.AddSingleton<IValidationAttributeAdapterProvider, MoedaValidationAttributeAdapterProvider>();

			services.AddScoped<INotificador, Notificador>();
			services.AddScoped<IProdutoService, ProdutoService>();
			services.AddScoped<IFornecedorService, FornecedorService>();
		}
	}
}
