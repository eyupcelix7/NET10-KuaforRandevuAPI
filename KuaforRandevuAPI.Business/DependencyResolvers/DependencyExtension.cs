using FluentValidation;
using KuaforRandevuAPI.Business.Abstract;
using KuaforRandevuAPI.Business.Concrete;
using KuaforRandevuAPI.Business.Mappings.Profiles;
using KuaforRandevuAPI.Business.ValidationRules.BarberRules;
using KuaforRandevuAPI.Business.ValidationRules.ReservationRules;
using KuaforRandevuAPI.Business.ValidationRules.ServiceRules;
using KuaforRandevuAPI.DataAccess.Context;
using KuaforRandevuAPI.DataAccess.Repositories.Abstract;
using KuaforRandevuAPI.DataAccess.Repositories.Concrete;
using KuaforRandevuAPI.Dtos.Barber;
using KuaforRandevuAPI.Dtos.Reservation;
using KuaforRandevuAPI.Dtos.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace KuaforRandevuAPI.Business.DependencyResolvers
{
    public static class DependencyExtension
    {
        public static void AddDependencies(this IServiceCollection services)
        {
            services.AddDbContext<BarberContext>(options =>
            {
                options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=BarberReservationAPI;Integrated Security=True;");
            });
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<BarberProfile>();
                cfg.AddProfile<ServiceProfile>();
                cfg.AddProfile<ReservationProfile>();
                cfg.AddProfile<BarberServiceProfile>();
                cfg.AddProfile<PaymentProfile>();
            });
            // Repositories
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IBarberRepository), typeof(BarberRepository));
            services.AddScoped(typeof(IServiceRepository), typeof(ServiceRepository));
            services.AddScoped(typeof(IReservationRepository), typeof(ReservationRepository));
            // Services
            services.AddScoped(typeof(IBarberService), typeof(BarberService));
            services.AddScoped(typeof(IServiceService), typeof(ServiceService));
            services.AddScoped(typeof(IReservationService), typeof(ReservationService));
            services.AddScoped(typeof(IPaymentService), typeof(PaymentService));

            // Fluent Validation
            // Barbers
            services.AddTransient<IValidator<CreateBarberDto>, CreateBarberValidator>();
            services.AddTransient<IValidator<UpdateBarberDto>, UpdateBarberValidator>();
            // Services
            services.AddTransient<IValidator<CreateServiceDto>, CreateServiceValidator>();
            services.AddTransient<IValidator<UpdateServiceDto>, UpdateServiceValidator>();
            // Reservations
            services.AddTransient<IValidator<CreateReservationDto>, CreateReservationValidator>();
            services.AddTransient<IValidator<UpdateReservationDto>, UpdateReservationValidator>();
            services.AddTransient<IValidator<UpdateReservationStatusDto>, UpdateReservationStatusValidator>();
        }
    }
}
