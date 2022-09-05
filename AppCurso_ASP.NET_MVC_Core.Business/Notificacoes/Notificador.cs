using AppCurso_ASP.NET_MVC_Core.Business.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace AppCurso_ASP.NET_MVC_Core.Business.Notificacoes
{
	public class Notificador : INotificador
	{
		private List<Notificacao> _notificacoes;

		public Notificador()
		{
			_notificacoes = new List<Notificacao>();
		}

		public bool ExisteNotificacao()
		{
			return _notificacoes.Any();
		}

		public void Handle(Notificacao notificao)
		{
			_notificacoes.Add(notificao);
		}

		public List<Notificacao> ObterNotificacoes()
		{
			return _notificacoes;
		}
	}
}
