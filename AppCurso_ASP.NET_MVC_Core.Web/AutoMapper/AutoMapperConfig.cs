using AppCurso_ASP.NET_MVC_Core.Business.Models;
using AppCurso_ASP.NET_MVC_Core.Web.ViewModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppCurso_ASP.NET_MVC_Core.Web.AutoMapper
{
	public class AutoMapperConfig : Profile
	{
		public AutoMapperConfig()
		{
			CreateMap<Fornecedor, FornecedorViewModel>().ReverseMap();

			CreateMap<Produto, ProdutoViewModel>().ReverseMap();

			CreateMap<Endereco, EnderecoViewModel>().ReverseMap();
		}
	}
}
