using KuaforRandevuAPI.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace KuaforRandevuAPI.DataAccess.Configurations
{
    public class BarberServiceConfiguration : IEntityTypeConfiguration<BarberService>
    {
        public void Configure(EntityTypeBuilder<BarberService> builder)
        {
            builder
                .HasKey(x => new { x.BarberId, x.ServiceId });
            builder
                .HasOne(x => x.Barber)
                .WithMany(y => y.BarberServices)
                .HasForeignKey(z => z.BarberId);
            builder
                .HasOne(x=> x.Service)
                .WithMany(y=> y.BarberServices)
                .HasForeignKey(z=> z.ServiceId);
        }
    }
}
