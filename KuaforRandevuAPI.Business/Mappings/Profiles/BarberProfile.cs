using AutoMapper;
using KuaforRandevuAPI.Dtos.Barber;
using KuaforRandevuAPI.Dtos.Services;
using KuaforRandevuAPI.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace KuaforRandevuAPI.Business.Mappings.Profiles
{
    public class BarberProfile: Profile
    {
        public BarberProfile()
        {
            CreateMap<Barber, CreateBarberDto>().ReverseMap();
            CreateMap<Barber, ResultBarberDto>().ReverseMap();
            CreateMap<Barber, ResultBarberDto>();
        }
    }
}
