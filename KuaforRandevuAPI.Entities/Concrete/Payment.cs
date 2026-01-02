using KuaforRandevuAPI.Entities.Enums.PaymentMethods;
using System;
using System.Collections.Generic;
using System.Text;

namespace KuaforRandevuAPI.Entities.Concrete
{
    public class Payment
    {
        public int Id { get; set; }
        public Barber? Barber { get; set; }
        public int BarberId { get; set; }
        public Reservation? Reservation { get; set; }
        public int ReservationId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public PaymentMethods PaymentMethod { get; set; }
    }
}
