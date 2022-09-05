using AppCurso_ASP.NET_MVC_Core.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AppCurso_ASP.NET_MVC_Core.Web.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[Route("erro/{id:length(3,3)}")]
		public IActionResult Errors(int id)
        {
			var modalError = new ErrorViewModel();

			if(id.Equals(403))
            {
				modalError.ErrorCode = id;
				modalError.Titulo = "Acesso Negado";
				modalError.Mensagem = "Você não possui permissão para acessar esta página!";
            }
			else if (id.Equals(404))
			{
				modalError.ErrorCode = id;
				modalError.Titulo = "Página não encontrada!";
				modalError.Mensagem = "A página que está tentando acessar não existe.";
			}
			else if (id.Equals(500))
			{
				modalError.ErrorCode = id;
				modalError.Titulo = "Ocorreu um erro";
				modalError.Mensagem = "Ocorreu um erro interno no servidor.<br/> Por favor, tente novamente mais tarde ou contate nosso suporte.";
			}
            else
            {
				return StatusCode(400);
            }

			return View("Error", modalError);
		}
	}
}
