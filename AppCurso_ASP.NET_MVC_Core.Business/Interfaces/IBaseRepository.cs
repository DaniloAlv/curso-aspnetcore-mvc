using AppCurso_ASP.NET_MVC_Core.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AppCurso_ASP.NET_MVC_Core.Business.Interfaces
{
	public interface IBaseRepository<TEntity> : IDisposable where TEntity : Entity
	{
		Task<List<TEntity>> GetAll();
		Task<TEntity> GetById(int id);
		Task<List<TEntity>> GetListByFilter(Expression<Func<TEntity, bool>> filter);
		Task<TEntity> GetByFilter(Expression<Func<TEntity, bool>> filter);
		Task<TEntity> Add(TEntity entity);
		Task<TEntity> Update(TEntity entity);
		Task Remove(int id);
		Task<int> SaveChanges();
	}
}
