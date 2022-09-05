using AppCurso_ASP.NET_MVC_Core.Business.Notificacoes;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppCurso_ASP.NET_MVC_Core.Business.Interfaces
{
	public interface INotificador
	{
		bool ExisteNotificacao();
		List<Notificacao> ObterNotificacoes();
		void Handle(Notificacao notificao);
	}
}
