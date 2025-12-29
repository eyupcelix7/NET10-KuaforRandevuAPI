using KuaforRandevuAPI.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace KuaforRandevuAPI.DataAccess.Repositories.Abstract
{
    public interface IReservationRepository
    {
        Task<List<Reservation>> GetReservationsForToday();
    }
}
