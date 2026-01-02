using KuaforRandevuAPI.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace KuaforRandevuAPI.Entities.Concrete
{
    public class Barber: IEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public TimeOnly StartTime { get; set; } // Mesai Başlangıç Saati
        public TimeOnly EndTime { get; set; } // Mesai Bitiş Saati
        public List<Reservation>? Reservations { get; set; }
        public List<BarberService>? BarberServices { get; set; }
        public List<Payment>? Payments { get; set; }
    }
}
