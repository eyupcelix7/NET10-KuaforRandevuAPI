using KuaforRandevuAPI.Dtos.Barber;
using System;
using System.Collections.Generic;
using System.Text;

namespace KuaforRandevuAPI.Dtos.Services
{
    public class CreateServiceDto
    {
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public TimeSpan? Duration { get; set; }
        public int BarberId { get; set; }
    }
}
