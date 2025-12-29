using KuaforRandevuAPI.DataAccess.Configurations;
using KuaforRandevuAPI.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace KuaforRandevuAPI.DataAccess.Context
{
    public class BarberContext: DbContext
    {
        public DbSet<Barber> Barbers { get; set; }
        public DbSet<Reservation> Reservations { get; set; }    
        public BarberContext(DbContextOptions<BarberContext> options): base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BarberConfiguration());
            modelBuilder.ApplyConfiguration(new ReservationConfiguration());
            modelBuilder.ApplyConfiguration(new BarberServiceConfiguration());
        }
    }
}
