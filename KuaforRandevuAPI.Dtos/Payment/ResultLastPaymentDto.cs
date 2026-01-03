using KuaforRandevuAPI.Dtos.Barber;
using KuaforRandevuAPI.Dtos.Reservation;
using KuaforRandevuAPI.Entities.Enums.PaymentMethods;
using System;
using System.Collections.Generic;
using System.Text;

namespace KuaforRandevuAPI.Dtos.Payment
{
    public class ResultLastPaymentDto
    {
        public int Id { get; set; }
        public int BarberId { get; set; }
        public int ReservationId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public PaymentMethods PaymentMethod { get; set; }
        public ResultBarberDto? Barber { get; set; }
        public ResultReservationDto? Reservation { get; set; }
    }
}
