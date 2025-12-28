using KuaforRandevuAPI.Dtos.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace KuaforRandevuAPI.Dtos.Barber
{
    public class ResultBarberDto
    {
        public int Id { get; set; }
        public string? BarberName { get; set; }
        public TimeOnly JobStartTime { get; set; }
        public TimeOnly JobEndTime { get; set; }
        public List<ResultServiceDto>? Services { get; set; }
    }
}
