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
        public bool checkNameExists(string barberName, int? barberId = null)
        {
            if(barberId == null)
            {
                return _context.Barbers.Any(x=> x.Name!.Equals(barberName));
            }
            else
            {
                return _context.Barbers.Any(x => x.Name!.Equals(barberName) && x.Id != barberId);
            }
        }

        public async Task<Barber?> GetBarberByIdWithServices(int id)
        {
            var values = await _context.Barbers.Include(x => x.BarberServices).Where(y => y.Id == id).FirstOrDefaultAsync();
            return values;
        }

        public Task<List<Barber?>> GetBarbersWithServices()
        {
            throw new NotImplementedException();
        }
    }
}
