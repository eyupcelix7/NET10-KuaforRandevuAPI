using AutoMapper;
using KuaforRandevuAPI.Dtos.Payment;
using KuaforRandevuAPI.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace KuaforRandevuAPI.Business.Mappings.Profiles
{
    public class PaymentProfile:Profile
    {
        public PaymentProfile()
        {
            CreateMap<Payment, ResultPaymentDto>().ReverseMap();
            CreateMap<Payment, CreatePaymentDto>().ReverseMap();
            CreateMap<Payment, UpdatePaymentDto>().ReverseMap();

            CreateMap<Payment, CreatePaymentDto>().ReverseMap();
            CreateMap<Payment, UpdatePaymentDto>().ReverseMap();
        }
    }
}
