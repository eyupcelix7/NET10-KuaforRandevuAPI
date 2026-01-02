using KuaforRandevuAPI.Dtos.Barber;
using KuaforRandevuAPI.Dtos.Services;
using KuaforRandevuAPI.Entities.Enums.Reservation;
using System;
using System.Collections.Generic;
using System.Text;

namespace KuaforRandevuAPI.Dtos.Reservation
{
    public class ResultNextReservationDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public ResultBarberDto? Barber { get; set; }
        public int BarberId { get; set; }
        public ResultServiceDto? Service { get; set; }
        public int ServiceId { get; set; }
        public ReservationStatus Status { get; set; }

    }
}
