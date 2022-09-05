using AppCurso_ASP.NET_MVC_Core.Business.Interfaces;
using AppCurso_ASP.NET_MVC_Core.Business.Models;
using AppCurso_ASP.NET_MVC_Core.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AppCurso_ASP.NET_MVC_Core.Data.Repository
{
	public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : Entity
	{
		protected readonly AppCursoDbContext _context;

		protected BaseRepository(AppCursoDbContext context)
		{
			_context = context;
		}

		public virtual async Task<List<TEntity>> GetAll()
		{
			return await _context.Set<TEntity>()
				.AsNoTracking()
				.ToListAsync();
		}

		public async Task<TEntity> GetByFilter(Expression<Func<TEntity, bool>> filter)
		{
			return await _context.Set<TEntity>()
				.AsNoTracking()
				.FirstOrDefaultAsync(filter);
		}

		public virtual async Task<TEntity> GetById(int id)
		{
			return await _context.Set<TEntity>()
				.FindAsync(id);
		}

		public async Task<List<TEntity>> GetListByFilter(Expression<Func<TEntity, bool>> filter)
		{
			return await _context.Set<TEntity>()
				.AsNoTracking()
				.Where(filter)
				.ToListAsync();
		}

		public virtual async Task<TEntity> Add(TEntity entity)
		{
			await _context.Set<TEntity>().AddAsync(entity);
			await SaveChanges();

			return entity;
		}

		public virtual async Task<TEntity> Update(TEntity entity)
		{
			_context.Set<TEntity>().Update(entity);
			await SaveChanges();

			return entity;
		}

		public virtual async Task Remove(int id)
		{
			var entity = await GetById(id);
			_context.Remove(entity);
		}

		public async Task<int> SaveChanges()
		{
			return await _context.SaveChangesAsync();
		}

		public async void Dispose()
		{
			await _context.DisposeAsync();
		}
	}
}
