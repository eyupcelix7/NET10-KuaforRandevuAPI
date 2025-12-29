using KuaforRandevuAPI.Dtos.Reservation;
using System;
using System.Collections.Generic;
using System.Text;

namespace KuaforRandevuAPI.Business.Abstract
{
    public interface IReservationService
    {
        Task<List<ResultReservationDto>> GetAllReservations();
        Task<ResultReservationDto> GetReservationById(int id);
        Task<List<ResultReservationDto>> GetReservationForToday();
        Task Create(CreateReservationDto dto);
        Task Update(UpdateReservationDto dto);
        Task Remove(int id);

        Task<bool> CheckReservationAvailabble(CreateReservationDto dto);
    }
}
