using Microsoft.EntityFrameworkCore;
using Personas.Domain.Interfaces;
using Personas.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personas.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
           return await _dbSet.FindAsync(id) ?? throw new KeyNotFoundException($"Entity with id {id} not found.");
        }


        public async Task AddAsync(T entity)
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            //_dbSet.Update(entity);
            //await _context.SaveChangesAsync();
            var key = _context.Model.FindEntityType(typeof(T))?.FindPrimaryKey()?.Properties.FirstOrDefault();
            if (key == null)
                throw new InvalidOperationException("No se pudo determinar la clave primaria");

            var keyValue = entity.GetType().GetProperty(key.Name)?.GetValue(entity);
            if (keyValue == null)
                throw new InvalidOperationException("La clave primaria es nula");

            var existingEntity = await _dbSet.FindAsync(keyValue);

            if (existingEntity != null)
            {
                _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            }
            else
            {
                _dbSet.Update(entity);
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"Entity with id {id} not found.");
            }
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

    }
}
