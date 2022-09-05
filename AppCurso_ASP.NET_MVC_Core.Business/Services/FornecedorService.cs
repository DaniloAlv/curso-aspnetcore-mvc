using AppCurso_ASP.NET_MVC_Core.Business.Interfaces;
using AppCurso_ASP.NET_MVC_Core.Business.Models;
using AppCurso_ASP.NET_MVC_Core.Business.Models.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCurso_ASP.NET_MVC_Core.Business.Services
{
	public class FornecedorService : BaseService, IFornecedorService
	{
		private readonly IFornecedorRepository _fornecedorRepository;
		private readonly IEnderecoRepository _enderecoRepository;

		public FornecedorService(IFornecedorRepository fornecedorRepository,
								 IEnderecoRepository enderecoRepository,
								 INotificador notificador) : base(notificador)
		{
			_fornecedorRepository = fornecedorRepository;
			_enderecoRepository = enderecoRepository;
		}

		public async Task Adicionar(Fornecedor fornecedor)
		{
			if (!ExecutarValidacao(new FornecedorValidation(), fornecedor) || 
				!ExecutarValidacao(new EnderecoValidation(), fornecedor.Endereco)) return;

			if((await _fornecedorRepository.GetListByFilter(f => f.Documento == fornecedor.Documento)).Any())
			{
				Notificar("Já existe um fornecedor com este documento.");
				return;
			}

			await _fornecedorRepository.Add(fornecedor);
		}

		public async Task Atualizar(Fornecedor fornecedor)
		{
			if (!ExecutarValidacao(new FornecedorValidation(), fornecedor)) return;

			if ((await _fornecedorRepository.GetListByFilter(f => f.Documento == fornecedor.Documento && f.Id != fornecedor.Id)).Any())
			{
				Notificar("Já existe um fornecedor com este documento.");
				return;
			}

			await _fornecedorRepository.Update(fornecedor);
		}

		public async Task AtualizarEndereco(Endereco endereco)
		{
			if (!ExecutarValidacao(new EnderecoValidation(), endereco)) return;

			await _enderecoRepository.Update(endereco);
		}

		public async Task Remover(int id)
		{
			if((await _fornecedorRepository.GetFornecedorByProdutosEndereco(id)).Produtos.Any())
			{
				Notificar("O fornecedor ainda possui produtos vinculados");
				return;
			}

			await _fornecedorRepository.Remove(id);
		}
	}
}
