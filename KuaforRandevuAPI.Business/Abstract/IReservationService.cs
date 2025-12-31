using KuaforRandevuAPI.Dtos.Reservation;
using KuaforRandevuAPI.Entities.Enums.Reservation;
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
        Task<List<ResultReservationDto>> GetReservationByBarberId(ReservationStatus status, int barberId);

        Task Create(CreateReservationDto dto);
        Task Update(UpdateReservationDto dto);
        Task Remove(int id);

        Task<bool> CheckHourAvailable(CreateReservationDto dto);
        Task<List<string>> GetReservationSuggestions(CreateReservationDto dto);
        Task<bool> CheckReservationAvailable(CreateReservationDto dto);
    }
}
