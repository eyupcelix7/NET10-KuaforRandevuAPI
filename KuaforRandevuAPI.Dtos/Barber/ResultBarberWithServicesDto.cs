using KuaforRandevuAPI.Dtos.BarberService;
using System;
using System.Collections.Generic;
using System.Text;

namespace KuaforRandevuAPI.Dtos.Barber
{
    public class ResultBarberWithServicesDto
    {
        public int Id { get; set; }
        public string? BarberName { get; set; }
        public TimeOnly JobStartTime { get; set; }
        public TimeOnly JobEndTime { get; set; }
        public List<ResultBarberServiceDto>? BarberServices { get; set; }
    }
}
