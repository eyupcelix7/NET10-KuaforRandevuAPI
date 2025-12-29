using KuaforRandevuAPI.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace KuaforRandevuAPI.DataAccess.Configurations
{
    public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder
                .HasOne(x => x.Barber)
                .WithMany(y => y.Reservations)
                .HasForeignKey(z => z.BarberId);
            builder
                .HasOne(x => x.Service)
                .WithMany(y => y.Reservations)
                .HasForeignKey(z => z.ServiceId);
        }
    }
}
