using KuaforRandevuAPI.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace KuaforRandevuAPI.Entities.Concrete
{
    public class Reservation: IEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public Barber? Barber { get; set; }
        public int BarberId { get; set; }
    }
}
