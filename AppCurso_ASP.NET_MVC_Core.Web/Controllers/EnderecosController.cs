using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AppCurso_ASP.NET_MVC_Core.Web.ViewModels;
using AppCurso_ASP.NET_MVC_Core.Business.Interfaces;
using AutoMapper;
using AppCurso_ASP.NET_MVC_Core.Business.Models;

namespace AppCurso_ASP.NET_MVC_Core.Web.Controllers
{
	public class EnderecosController : Controller
	{
		private readonly IEnderecoRepository _enderecoRepository;
		private readonly IMapper _mapper;

		public EnderecosController(IEnderecoRepository enderecoRepository,
								  IMapper mapper)
		{
			_enderecoRepository = enderecoRepository;
			_mapper = mapper;
		}

		public async Task<IActionResult> Index()
		{
			return View(_mapper.Map<IEnumerable<EnderecoViewModel>>(await _enderecoRepository.GetAll()));
		}

		public async Task<IActionResult> Details(int id)
		{
			var endereco = await _enderecoRepository.GetById(id);

			if (endereco == null)
			{
				return NotFound("Endereço não encontrado!");
			}

			return View(_mapper.Map<EnderecoViewModel>(endereco));
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(EnderecoViewModel enderecoViewModel)
		{
			if (!ModelState.IsValid)
			{
				return View(enderecoViewModel);
			}

			await _enderecoRepository.Add(_mapper.Map<Endereco>(enderecoViewModel));

			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> Edit(int id)
		{
			var endereco = await _enderecoRepository.GetById(id);

			if (endereco == null)
			{
				return NotFound("Endereço não encontrado!");
			}

			return View(_mapper.Map<EnderecoViewModel>(endereco));
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, EnderecoViewModel enderecoViewModel)
		{
			if (id != enderecoViewModel.Id)
			{
				return NotFound("Endereço não encontrado!");
			}

			if (!ModelState.IsValid)
			{
				return View(enderecoViewModel);
			}

			await _enderecoRepository.Update(_mapper.Map<Endereco>(enderecoViewModel));

			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> Delete(int id)
		{
			var endereco = await _enderecoRepository.GetById(id);

			if (endereco== null)
			{
				return NotFound("Endereço não encontrado!");
			}

			return View(_mapper.Map<EnderecoViewModel>(endereco));
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var endereco = await _enderecoRepository.GetById(id);

			if(endereco == null)
			{
				return NotFound("Endereço não encontrado!");
			}

			await _enderecoRepository.Remove(id);

			return RedirectToAction(nameof(Index));
		}
	}
}
