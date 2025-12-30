using KuaforRandevuAPI.DataAccess.Context;
using KuaforRandevuAPI.DataAccess.Repositories.Abstract;
using KuaforRandevuAPI.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace KuaforRandevuAPI.DataAccess.Repositories.Concrete
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly BarberContext _context;
        public ReservationRepository(BarberContext context)
        {
            _context = context;
        }
        public async Task<List<Reservation>> GetReservationsForToday()
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            return await _context.Reservations
                .Include(r => r.Service)
                .Include(x => x.Barber)
                .Where(r => r.Date == today)
                .ToListAsync();
        }
    }
}
