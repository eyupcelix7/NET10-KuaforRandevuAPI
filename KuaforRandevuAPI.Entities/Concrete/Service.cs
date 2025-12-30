using KuaforRandevuAPI.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace KuaforRandevuAPI.Entities.Concrete
{
    public class Service:IEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public string? Duration { get; set; } // Hizmetin Süresi
        public List<Reservation>? Reservations { get; set; }
        public List<BarberService>? BarberServices { get; set; }
    }
}
