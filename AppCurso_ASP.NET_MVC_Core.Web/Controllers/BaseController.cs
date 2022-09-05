using AppCurso_ASP.NET_MVC_Core.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppCurso_ASP.NET_MVC_Core.Web.Controllers
{
	public abstract class BaseController : Controller
	{
		private readonly INotificador _notificador;

		protected BaseController(INotificador notificador)
		{
			_notificador = notificador;
		}

		protected bool OperacaoValida()
		{
			return _notificador.ExisteNotificacao();
		}
	}
}
