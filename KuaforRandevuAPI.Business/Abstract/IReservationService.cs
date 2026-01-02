using KuaforRandevuAPI.Common.Responses;
using KuaforRandevuAPI.Dtos.Reservation;
using KuaforRandevuAPI.Entities.Enums.Reservation;
using System;
using System.Collections.Generic;
using System.Text;

namespace KuaforRandevuAPI.Business.Abstract
{
    public interface IReservationService
    {
        Task<ApiResponse<List<ResultReservationDto>>> GetAllReservations();
        Task<ApiResponse<ResultReservationDto>> GetReservationById(int id);
        Task<ApiResponse<List<ResultReservationDto>>> GetReservationForToday();
        Task<ApiResponse<List<ResultReservationDto>>> GetReservationByBarberId(ReservationStatus status, int barberId);
        Task<ResultNextReservationDto> GetNextReservation(int barberId);
        Task<ApiResponse<CreateReservationDto>> Create(CreateReservationDto dto);
        Task<ApiResponse<UpdateReservationDto>> Update(UpdateReservationDto dto);
        Task<ApiResponse<int>> Remove(int id);
        Task<ApiResponse<UpdateReservationStatusDto>> UpdateReservationStatus(UpdateReservationStatusDto dto);
        Task<List<string>> GetReservationSuggestions(ResultReservationDto dto);
        Task<bool> CheckHourAvailable(ResultReservationDto dto);
        Task<bool> CheckReservationAvailable(ResultReservationDto dto);
    }
}
