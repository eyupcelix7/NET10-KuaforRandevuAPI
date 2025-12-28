using KuaforRandevuAPI.DataAccess.Context;
using KuaforRandevuAPI.DataAccess.Repositories.Abstract;
using KuaforRandevuAPI.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace KuaforRandevuAPI.DataAccess.Repositories.Concrete
{
    public class BarberRepository : IBarberRepository
    {
        private readonly BarberContext _context;
        public BarberRepository(BarberContext context)
        {
            _context = context;
        }
        public bool checkNameExists(string barberName)
        {
            return _context.Barbers.Any(x=> x.BarberName!.Equals(barberName));
        }

        public async Task<Barber?> GetBarberByIdWithServices(int id)
        {
            return await _context.Barbers.Include(x => x.Services).Where(y => y.Id == id).FirstOrDefaultAsync();
        }

        public Task<List<Barber?>> GetBarbersWithServices()
        {
            throw new NotImplementedException();
        }
    }
}
