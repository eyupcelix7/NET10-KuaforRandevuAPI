using KuaforRandevuAPI.DataAccess.Context;
using KuaforRandevuAPI.DataAccess.Repositories.Abstract;
using KuaforRandevuAPI.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace KuaforRandevuAPI.DataAccess.Repositories.Concrete
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly BarberContext _context;
        public ServiceRepository(BarberContext context)
        {
            _context = context;
        }
        public async Task<List<Service>> GetServicesByBarberId(int id)
        {
            return await _context.BarberServices
                .Include(x => x.Barber)
                .Include(y => y.Service)
                .Where(z=> z.BarberId == id)
                .Select(w=> new Service
                {
                    Id = w.ServiceId,
                    Name = w.Service!.Name,
                    Duration = w.Service!.Duration,
                })
                .ToListAsync();
        }
    }
}
