using System;
using System.Collections.Generic;
using System.Text;

namespace KuaforRandevuAPI.Dtos.BarberService
{
    public class ResultBarberServiceDto
    {
        public int BarberId { get; set; }
        public int ServiceId { get; set; }
        public bool IsActive { get; set; }
    }
}
