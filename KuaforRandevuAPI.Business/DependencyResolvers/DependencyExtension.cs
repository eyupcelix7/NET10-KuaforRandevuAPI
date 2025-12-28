using FluentValidation;
using KuaforRandevuAPI.Business.Abstract;
using KuaforRandevuAPI.Business.Concrete;
using KuaforRandevuAPI.Business.Mappings.Profiles;
using KuaforRandevuAPI.Business.ValidationRules.BarberRules;
using KuaforRandevuAPI.DataAccess.Context;
using KuaforRandevuAPI.DataAccess.Repositories.Abstract;
using KuaforRandevuAPI.DataAccess.Repositories.Concrete;
using KuaforRandevuAPI.Dtos.Barber;
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
            });
            // Repositories
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IBarberRepository), typeof(BarberRepository));
            // Services
            services.AddScoped(typeof(IBarberService), typeof(BarberService));
            services.AddScoped(typeof(IServiceService), typeof(ServiceService));

            services.AddTransient<IValidator<CreateBarberDto>, CreateBarberValidator>();
        }
    }
}
