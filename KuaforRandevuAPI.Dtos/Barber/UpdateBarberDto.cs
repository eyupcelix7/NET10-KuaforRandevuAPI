using System;
using System.Collections.Generic;
using System.Text;

namespace KuaforRandevuAPI.Dtos.Barber
{
    public class UpdateBarberDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
    }
}
