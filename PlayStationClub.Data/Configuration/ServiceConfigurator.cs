using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlayStationClub.Data.Entity;

namespace PlayStationClub.Data.Configuration
{
    public class ServiceConfigurator : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.Property(s => s.Name).IsRequired().HasMaxLength(50);
            builder.Property(s => s.Description).IsRequired();
            builder.Property(s => s.Price).IsRequired();

            //builder.HasData(
            //    new Service
            //    {
            //        Id = 1,
            //        Name = "s1",
            //        Description = "d1",
            //        Price = 10
            //    },
            //    new Service
            //    {
            //        Id = 2,
            //        Name = "s2",
            //        Description = "d2",
            //        Price = 20
            //    },
            //    new Service
            //    {
            //        Id = 3,
            //        Name = "s3",
            //        Description = "d3",
            //        Price = 30
            //    });
        }
    }
}
