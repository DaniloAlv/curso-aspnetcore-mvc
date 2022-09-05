using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AppCurso_ASP.NET_MVC_Core.Web.ViewModels;
using AppCurso_ASP.NET_MVC_Core.Business.Interfaces;
using AutoMapper;
using AppCurso_ASP.NET_MVC_Core.Business.Models;
using Microsoft.AspNetCore.Authorization;
using AppCurso_ASP.NET_MVC_Core.Web.Extensions;

namespace AppCurso_ASP.NET_MVC_Core.Web.Controllers
{
	[Authorize]
	[Route("fornecedores")]

	public class FornecedoresController : BaseController
	{
		private readonly IFornecedorRepository _fornecedorRepository;
		private readonly IFornecedorService _fornecedorService;
		private readonly IMapper _mapper;

		public FornecedoresController(IFornecedorRepository fornecedorRepository,
									  IMapper mapper,
									  IFornecedorService fornecedorService,
									  INotificador notificador) : base(notificador)
		{
			_fornecedorRepository = fornecedorRepository;
			_mapper = mapper;
			_fornecedorService = fornecedorService;
		}

		[AllowAnonymous]
		[HttpGet("lista")]
		public async Task<IActionResult> Index()
		{
			var fornecedores = await _fornecedorRepository.GetAll();
			return View(_mapper.Map<IEnumerable<FornecedorViewModel>>(fornecedores));
		}

		[AllowAnonymous]
		[HttpGet("detalhes/{id:int}")]
		public async Task<IActionResult> Details(int id)
		{
			var fornecedorViewModel = await _fornecedorRepository.GetFornecedorByEndereco(id);

			if (fornecedorViewModel == null)
			{
				return NotFound("Fornecedor não encontrado");
			}

			return View(_mapper.Map<FornecedorViewModel>(fornecedorViewModel));
		}

		[ClaimsAuthorize("Fornecedor", "Adicionar")]
		[Route("novo")]
		public IActionResult Create()
		{
			return View();
		}

		[ClaimsAuthorize("Fornecedor", "Adicionar")]
		[HttpPost("novo")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(FornecedorViewModel fornecedorViewModel)
		{
			if (!ModelState.IsValid)
			{
				return View(fornecedorViewModel);
			}

			Fornecedor fornecedor = _mapper.Map<Fornecedor>(fornecedorViewModel);
			await _fornecedorService.Adicionar(fornecedor);

			if (!OperacaoValida()) return View(fornecedorViewModel);

			return RedirectToAction(nameof(Index));
		}

		[ClaimsAuthorize("Fornecedor", "Editar")]
		[Route("editar/{id:int}")]
		public async Task<IActionResult> Edit(int id)
		{
			var fornecedorViewModel = await _fornecedorRepository.GetFornecedorByProdutosEndereco(id);

			if (fornecedorViewModel == null)
			{
				return NotFound("Fornecedor não encontrado!");
			}

			return View(_mapper.Map<FornecedorViewModel>(fornecedorViewModel));
		}

		[ClaimsAuthorize("Fornecedor", "Editar")]
		[HttpPost("editar/{id:int}")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, FornecedorViewModel fornecedorViewModel)
		{
			if (id != fornecedorViewModel.Id)
			{
				return NotFound();
			}

			if (!ModelState.IsValid)
			{
				return View(fornecedorViewModel);
			}

			Fornecedor fornecedor = _mapper.Map<Fornecedor>(fornecedorViewModel);
			await _fornecedorService.Atualizar(fornecedor);

			if (!OperacaoValida()) return View(fornecedorViewModel);

			return RedirectToAction(nameof(Index));
		}

		[ClaimsAuthorize("Fornecedor", "Excluir")]
		[Route("excluir/{id:int}")]
		public async Task<IActionResult> Delete(int id)
		{
			Fornecedor fornecedor = await _fornecedorRepository.GetFornecedorByEndereco(id);

			if (fornecedor == null)
			{
				return NotFound("Fornecedor não encontrado!");
			}

			FornecedorViewModel fornecedorViewModel = _mapper.Map<FornecedorViewModel>(fornecedor);

			return View(fornecedorViewModel);
		}

		[ClaimsAuthorize("Fornecedor", "Excluir")]
		[HttpPost("excluir/{id:int}"), ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			Fornecedor fornecedor = await _fornecedorRepository.GetFornecedorByEndereco(id);

			if(fornecedor == null)
			{
				return NotFound("Fornecedor não encontrado!");
			}

			await _fornecedorService.Remover(id);

			if (!OperacaoValida()) return View(_mapper.Map<FornecedorViewModel>(fornecedor));

			return RedirectToAction(nameof(Index));
		}

		[ClaimsAuthorize("Fornecedor", "Editar")]
		[Route("atualizar-endereco/{fornecedorId:int}")]
		public async Task<IActionResult> AtualizarEndereco(int fornecedorId)
		{
			var fornecedor = await _fornecedorRepository.GetFornecedorByEndereco(fornecedorId);

			if (fornecedor == null)
				return NotFound("Fornecedor não encontrado!");

			return PartialView("_AtualizarEndereco", new FornecedorViewModel { Endereco = _mapper.Map<EnderecoViewModel>(fornecedor.Endereco) });
		}

		[ClaimsAuthorize("Fornecedor", "Editar")]
		[HttpPost("atualizar-endereco/{fornecedorId:int}")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> AtualizarEndereco(FornecedorViewModel fornecedor)
		{
			ModelState.Remove("Nome");
			ModelState.Remove("Documento");

			if (!ModelState.IsValid) return PartialView("_AtualizarEndereco", fornecedor);

			await _fornecedorService.AtualizarEndereco(_mapper.Map<Endereco>(fornecedor.Endereco));

			var url = Url.Action("ObterEndereco", "Fornecedores", new { id = fornecedor.Id });
			return Json(new { success = true, url });
		}

		[AllowAnonymous]
		[Route("obter-endereco/{id:int}")]
		public async Task<IActionResult> ObterEndereco(int id)
		{
			var fornecedor = await _fornecedorRepository.GetFornecedorByEndereco(id);

			if (fornecedor == null) return NotFound("Fornecedor não encontrado!");

			return PartialView("_DetalhesEndereco", fornecedor);
		}
	}
}
