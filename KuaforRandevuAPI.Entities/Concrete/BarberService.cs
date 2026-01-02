using KuaforRandevuAPI.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace KuaforRandevuAPI.Entities.Concrete
{
    public class BarberService:IEntity
    {
        public int Id { get; set; }
        public int BarberId { get; set; }
        public int ServiceId { get; set; }
        public bool IsActive { get; set; }
        public Barber? Barber { get; set; }
        public Service? Service { get; set; }
    }
}
