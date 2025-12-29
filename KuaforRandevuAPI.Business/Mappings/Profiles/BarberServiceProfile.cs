using AutoMapper;
using KuaforRandevuAPI.Dtos.BarberService;
using KuaforRandevuAPI.Dtos.Services;
using KuaforRandevuAPI.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace KuaforRandevuAPI.Business.Mappings.Profiles
{
    public class BarberServiceProfile : Profile
    {
        public BarberServiceProfile()
        {
            CreateMap<BarberService, ResultBarberServiceDto>();
            CreateMap<BarberService, ResultServiceDto>();
        }
    }
}
