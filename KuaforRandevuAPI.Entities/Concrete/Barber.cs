using KuaforRandevuAPI.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace KuaforRandevuAPI.Entities.Concrete
{
    public class Barber: IEntity
    {
        public int Id { get; set; }
        public string? BarberName { get; set; }
        public TimeOnly JobStartTime { get; set; } // Mesai Başlangıç Saati
        public TimeOnly JobEndTime { get; set; } // Mesai Bitiş Saati
        public List<Reservation>? Reservations { get; set; }
        public List<BarberService>? BarberServices { get; set; }
    }
}
