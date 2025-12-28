using System;
using System.Collections.Generic;
using System.Text;

namespace KuaforRandevuAPI.Dtos.Barber
{
    public class CreateBarberDto
    {
        public string? BarberName { get; set; }
        public TimeOnly JobStartTime { get; set; }
        public TimeOnly JobEndTime { get; set; }
    }
}
