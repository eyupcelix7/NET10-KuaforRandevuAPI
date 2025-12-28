using KuaforRandevuAPI.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace KuaforRandevuAPI.DataAccess.Repositories.Abstract
{
    public interface IRepository<T> where T : class, IEntity
    {
        Task<T?> GetById(int id);
        Task<List<T>> GetAll();
        Task Add(T entity);
        Task Update(T entity);
        Task Remove(T entity);
    }
}
