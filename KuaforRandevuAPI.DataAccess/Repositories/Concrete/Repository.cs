using KuaforRandevuAPI.DataAccess.Context;
using KuaforRandevuAPI.DataAccess.Repositories.Abstract;
using KuaforRandevuAPI.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KuaforRandevuAPI.DataAccess.Repositories.Concrete
{
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly BarberContext _context;
        public Repository(BarberContext context)
        {
            _context = context;
        }
        public async Task Add(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
        }
        public async Task<List<T>> GetAll()
        {
            var values = await _context.Set<T>().ToListAsync();
            return values;
        }
        public async Task<T?> GetById(int id)
        {
            var value = await _context.Set<T>().FindAsync(id);
            return value;
        }
        public async Task Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }
        public async Task Update(T entity)
        {
            var value = GetById(entity.Id);
            if (value != null)
            {
                var updatedEntity = _context.Set<T>().Update(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
