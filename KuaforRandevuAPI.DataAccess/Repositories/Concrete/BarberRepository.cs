using KuaforRandevuAPI.DataAccess.Context;
using KuaforRandevuAPI.DataAccess.Repositories.Abstract;
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
    }
}
