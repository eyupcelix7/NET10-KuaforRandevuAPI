using KuaforRandevuAPI.Dtos.Barber;
using System;
using System.Collections.Generic;
using System.Text;

namespace KuaforRandevuAPI.Dtos.Services
{
    public class ResultServiceDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public string? Duration { get; set; }
    }
}
