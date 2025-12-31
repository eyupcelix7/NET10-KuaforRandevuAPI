using KuaforRandevuAPI.Business.Abstract;
using KuaforRandevuAPI.Dtos.Reservation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KuaforRandevuAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }
        [HttpGet("GetAllReservations")]
        public async Task<IActionResult> GetAllReservations()
        {
            var reservations = await _reservationService.GetAllReservations();
            return Ok(reservations);
        }
        [HttpGet("GetReservationsForToday")]
        public async Task<IActionResult> GetReservationsForToday()
        {
            var reservations = await _reservationService.GetReservationForToday();
            return Ok(reservations);
        }

        [HttpGet("GetReservationById{id}")]
        public async Task<IActionResult> GetReservationById(int id)
        {
            var reservation = await _reservationService.GetReservationById(id);
            return Ok(reservation);
        }
        [HttpPost("CreateReservation")]
        public async Task<IActionResult> CreateReservation(CreateReservationDto dto)
        {
            var result = await _reservationService.Create(dto);
            return Ok(result);
        }
        [HttpPut("UpdateReservation")]
        public async Task<IActionResult> UpdateReservation(UpdateReservationDto dto)
        {
            var result = await _reservationService.Update(dto);
            return Ok(result);
        }
        [HttpDelete("RemoveReservation/{id}")]
        public async Task<IActionResult> RemoveReservation(int id)
        {
            var result = await _reservationService.Remove(id);
            return Ok(result);
        }
    }
}
