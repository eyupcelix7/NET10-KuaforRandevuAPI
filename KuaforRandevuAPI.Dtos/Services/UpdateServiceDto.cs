using System;
using System.Collections.Generic;
using System.Text;

namespace KuaforRandevuAPI.Dtos.Services
{
    public class UpdateServiceDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public TimeSpan? Duration { get; set; }
    }
}
