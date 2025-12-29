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
            return await _context.Reservations.Where(x=> x.Date == DateOnly.FromDateTime(DateTime.Now)).ToListAsync();
        }
    }
}
