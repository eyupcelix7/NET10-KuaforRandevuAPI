using KuaforRandevuAPI.Dtos.Barber;
using System;
using System.Collections.Generic;
using System.Text;

namespace KuaforRandevuAPI.Dtos.Services
{
    public class CreateServiceDto
    {
        public string? ServiceName { get; set; }
        public decimal ServicePrice { get; set; }
        public string? ServiceDuration { get; set; }
        public int BarberId { get; set; }
    }
}
