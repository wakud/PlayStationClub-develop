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
    public class ReviewConfigurator : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.Property(r => r.Text).IsRequired().HasMaxLength(200);
            builder.Property(r => r.Rating).IsRequired();

            builder.HasData(
                new Review
                {
                    Id = 1,
                    Text = "text1",
                    Rating = 5,
                    ReceivedDate = new DateTime(2022,3,5,12,0,0)
                },
                new Review
                {
                    Id = 2,
                    Text = "text2",
                    Rating = 2,
                    ReceivedDate = new DateTime(2022,3,8,19,22,00)
                });
        }
    }
}
