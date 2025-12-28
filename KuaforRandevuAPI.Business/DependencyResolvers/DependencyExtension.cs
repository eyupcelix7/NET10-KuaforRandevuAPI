using FluentValidation;
using KuaforRandevuAPI.Business.Abstract;
using KuaforRandevuAPI.Business.Concrete;
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
            services.AddScoped<BarberContext>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IBarberService), typeof(BarberService));

            services.AddTransient<IValidator<CreateBarberDto>, CreateBarberValidator>();
        }
    }
}
