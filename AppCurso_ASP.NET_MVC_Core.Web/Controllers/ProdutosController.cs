using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AppCurso_ASP.NET_MVC_Core.Web.ViewModels;
using AppCurso_ASP.NET_MVC_Core.Business.Interfaces;
using AutoMapper;
using AppCurso_ASP.NET_MVC_Core.Business.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using System;
using Microsoft.AspNetCore.Authorization;
using AppCurso_ASP.NET_MVC_Core.Web.Extensions;

namespace AppCurso_ASP.NET_MVC_Core.Web.Controllers
{
	[Authorize]
	[Route("produtos")]

	public class ProdutosController : BaseController
	{
		private readonly IProdutoRepository _produtoRepository;
		private readonly IFornecedorRepository _fornecedorRepository;
		private readonly IProdutoService _produtoService;
		private readonly IMapper _mapper;

		public ProdutosController(IProdutoRepository produtoRepository,
								  IFornecedorRepository fornecedorRepository,
								  IMapper mapper, IProdutoService produtoService,
								  INotificador notificador) : base(notificador)
		{
			_produtoRepository = produtoRepository;
			_fornecedorRepository = fornecedorRepository;
			_mapper = mapper;
			_produtoService = produtoService;
		}

		[AllowAnonymous]
		[HttpGet("lista")]
		public async Task<IActionResult> Index()
		{
			return View(_mapper.Map<IEnumerable<ProdutoViewModel>>(await _produtoRepository.GetProdutosFornecedor()));
		}

		[AllowAnonymous]
		[HttpGet("detalhes/{id:int}")]
		public async Task<IActionResult> Details(int id)
		{
			var produtoViewModel = _mapper.Map<ProdutoViewModel>(await _produtoRepository.GetProdutoFornecedor(id));

			if (produtoViewModel == null)
			{
				return NotFound("Produto não encontrado!");
			}

			return View(produtoViewModel);
		}

		[ClaimsAuthorize("Produto", "Adicionar")]
		[Route("novo")]
		public async Task<IActionResult> Create()
		{
			var produtoViewModel = new ProdutoViewModel();
			produtoViewModel.Fornecedores = _mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepository.GetAll());

			return View(produtoViewModel);
		}

		[ClaimsAuthorize("Produto", "Adicionar")]
		[HttpPost("novo")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(ProdutoViewModel produtoViewModel)
		{
			if (!ModelState.IsValid)
			{
				return View(produtoViewModel);
			}

			produtoViewModel.DataCadastro = DateTime.Now;
			string prefix = string.Concat(Guid.NewGuid(), "_");
			if(!await UploadImagem(produtoViewModel.ImagemUpload, prefix))
			{
				return View(produtoViewModel);
			}

			produtoViewModel.Imagem = string.Concat(prefix, produtoViewModel.ImagemUpload.FileName);
			await _produtoService.Adicionar(_mapper.Map<Produto>(produtoViewModel));

			if (!OperacaoValida()) return View(produtoViewModel);

			return RedirectToAction(nameof(Index));
		}

		[ClaimsAuthorize("Produto", "Editar")]
		[Route("editar/{id:int}")]
		public async Task<IActionResult> Edit(int id)
		{
			var produtoViewModel = await _produtoRepository.GetProdutoFornecedor(id);

			if (produtoViewModel == null)
			{
				return NotFound();
			}

			return View(_mapper.Map<ProdutoViewModel>(produtoViewModel));
		}

		[ClaimsAuthorize("Produto", "Editar")]
		[HttpPost("editar/{id:int}")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, ProdutoViewModel produtoViewModel)
		{
			if(id != produtoViewModel.Id)
			{
				return NotFound("Produto não encontrado");
			}

			if (!ModelState.IsValid)
			{
				return View(produtoViewModel);
			}

			await _produtoService.Atualizar(_mapper.Map<Produto>(produtoViewModel));

			if (!OperacaoValida()) return View(produtoViewModel);

			return RedirectToAction(nameof(Index));
		}

		[ClaimsAuthorize("Produto", "Excluir")]
		[Route("deletar/{id:int}")]
		public async Task<IActionResult> Delete(int id)
		{
			var produto = await _produtoRepository.GetProdutoFornecedor(id);

			if (produto == null)
			{
				return NotFound("Produto não encontrado!");
			}

			return View(_mapper.Map<ProdutoViewModel>(produto));
		}

		[ClaimsAuthorize("Produto", "Excluir")]
		[HttpPost("deletar/{id:int}"), ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var produto = await _produtoRepository.GetProdutoFornecedor(id);

			if (produto == null)
			{
				return NotFound("Produto não encontrado!");
			}

			await _produtoService.Remover(id);

			if (!OperacaoValida()) return View(_mapper.Map<ProdutoViewModel>(produto));

			return RedirectToAction(nameof(Index));
		}

		private async Task<bool> UploadImagem(IFormFile imagem, string prefixPath)
		{
			if (imagem.Length == 0)
				return false;

			string pathToSave = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imgs", prefixPath + imagem.FileName);

			using(FileStream fs = System.IO.File.Create(pathToSave))
			{
				await imagem.CopyToAsync(fs);
			}

			return true;
		}
	}
}
