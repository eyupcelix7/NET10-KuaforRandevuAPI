using KuaforRandevuAPI.Entities.Concrete;
using KuaforRandevuAPI.Entities.Enums.Reservation;
using System;
using System.Collections.Generic;
using System.Text;

namespace KuaforRandevuAPI.DataAccess.Repositories.Abstract
{
    public interface IReservationRepository
    {
        Task<List<Reservation>> GetReservationsForToday();
        Task<List<Reservation>> GetReservationsByBarberId(ReservationStatus status, int barberId);
        Task<Reservation?> GetNextReservation(int barberId);
    }
}
