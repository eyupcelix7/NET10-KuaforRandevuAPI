using System;
using System.Collections.Generic;
using System.Text;

namespace KuaforRandevuAPI.Dtos.Barber
{
    public class CreateBarberDto
    {
        public string? Name { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
    }
}
