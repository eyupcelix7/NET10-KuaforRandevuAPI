using System;
using System.Collections.Generic;
using System.Text;

namespace KuaforRandevuAPI.Dtos.Services
{
    public class UpdateServiceDto
    {
        public int Id { get; set; }
        public string? ServiceName { get; set; }
        public decimal ServicePrice { get; set; }
        public string? ServiceDuration { get; set; }
    }
}
