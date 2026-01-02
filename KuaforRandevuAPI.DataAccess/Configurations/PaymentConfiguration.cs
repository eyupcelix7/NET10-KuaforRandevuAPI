using KuaforRandevuAPI.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace KuaforRandevuAPI.DataAccess.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder
                .HasOne(x => x.Reservation)
                .WithOne(x => x.Payment)
                .HasForeignKey<Payment>(x => x.ReservationId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(x => x.Barber)
                .WithMany(x=> x.Payments)
                .HasForeignKey(x => x.BarberId);
        }
    }
}
