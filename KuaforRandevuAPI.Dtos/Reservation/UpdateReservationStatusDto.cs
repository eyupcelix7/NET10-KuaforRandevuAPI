using KuaforRandevuAPI.Entities.Enums.Reservation;
using System;
using System.Collections.Generic;
using System.Text;

namespace KuaforRandevuAPI.Dtos.Reservation
{
    public class UpdateReservationStatusDto
    {
        public int Id { get; set; }
        public ReservationStatus? Status { get; set; }
        public decimal Price { get; set; }
    }
}
