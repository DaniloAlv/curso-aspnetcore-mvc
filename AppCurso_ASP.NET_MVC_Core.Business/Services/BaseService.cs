using AppCurso_ASP.NET_MVC_Core.Business.Interfaces;
using AppCurso_ASP.NET_MVC_Core.Business.Models;
using AppCurso_ASP.NET_MVC_Core.Business.Notificacoes;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppCurso_ASP.NET_MVC_Core.Business.Services
{
	public abstract class BaseService
	{
		private readonly INotificador _notificador;

		protected BaseService(INotificador notificador)
		{
			_notificador = notificador;
		}

		protected void Notificar(ValidationResult result)
		{
			foreach (var error in result.Errors)
			{
				Notificar(error.ErrorMessage);
			}
		}

		protected void Notificar(string message)
		{
			_notificador.Handle(new Notificacao(message));
		}

		protected bool ExecutarValidacao<TValidator, TEntity>(TValidator validacao, TEntity entity) where TValidator : AbstractValidator<TEntity> where TEntity : Entity
		{
			var validator = validacao.Validate(entity);

			if (validator.IsValid) return true;

			Notificar(validator);

			return false;
		}
	}
}
