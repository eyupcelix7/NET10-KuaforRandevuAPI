using KuaforRandevuAPI.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace KuaforRandevuAPI.Entities.Concrete
{
    public class Service:IEntity
    {
        public int Id { get; set; }
        public string? ServiceName { get; set; }
        public decimal ServicePrice { get; set; }
        public string? ServiceDuration { get; set; } // Hizmetin Süresi
        public Barber? Barber { get; set; }
        public int BarberId { get; set; }
    }
}
