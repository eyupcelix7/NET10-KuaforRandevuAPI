using KuaforRandevuAPI.DataAccess.Context;
using KuaforRandevuAPI.DataAccess.Repositories.Abstract;
using KuaforRandevuAPI.Entities.Concrete;
using KuaforRandevuAPI.Entities.Enums.Reservation;
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
        public async Task ChangeReservationStatus(Reservation reservation)
        {
            var updatedReservation = await _context.Reservations.Where(x=> x.Id == reservation.Id).FirstOrDefaultAsync();
            if(updatedReservation != null)
            {
                updatedReservation.Price = reservation.Price;
                updatedReservation.Status = reservation.Status;
                _context.Reservations.Update(updatedReservation);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<Reservation?> GetNextReservation(int barberId)
        {
            var currentDate = DateOnly.FromDateTime(DateTime.Now);
            var currentTime = TimeOnly.FromDateTime(DateTime.Now);

            var nextReservation = await _context.Reservations
                .Include(x=> x.Barber)
                .Include(x => x.Service)
                .Where(x=> x.BarberId == barberId && x.Status == ReservationStatus.Pending && (x.Date > currentDate || (x.Date == currentDate && x.Time > currentTime))) // Tarih bugünden sonra ise veya (tarih bugün ise ve saat şuandan sonra ise)
                .OrderBy(x => x.Date)
                .ThenBy(x => x.Time)
                .FirstOrDefaultAsync();

            return nextReservation;
        }

        public async Task<List<Reservation>> GetReservationsByBarberId(ReservationStatus status, int barberId)
        {
            return await _context.Reservations
                .Where(x => x.BarberId == barberId && x.Status == status)
                .Include(x => x.Barber)
                .Include(x => x.Service)
                .ToListAsync();
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
