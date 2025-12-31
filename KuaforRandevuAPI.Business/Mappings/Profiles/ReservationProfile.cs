using AutoMapper;
using KuaforRandevuAPI.Dtos.Reservation;
using KuaforRandevuAPI.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace KuaforRandevuAPI.Business.Mappings.Profiles
{
    public class ReservationProfile : Profile
    {
        public ReservationProfile()
        {
            CreateMap<Reservation, ResultReservationDto>().ReverseMap();
            CreateMap<Reservation, CreateReservationDto>().ReverseMap();
            CreateMap<Reservation, UpdateReservationDto>().ReverseMap();

            CreateMap<CreateReservationDto, ResultReservationDto>().ReverseMap();
            CreateMap<UpdateReservationDto, ResultReservationDto>().ReverseMap();
        }
    }
}
