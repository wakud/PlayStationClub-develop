using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlayStationClub.Data.Entity;

namespace PlayStationClub.Data.Configuration
{
    public class CategoryConfigurator : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(c => c.Name).IsRequired().HasMaxLength(20);
            builder.HasData(
                new Category { Id = 1, Name = "fight" },
                new Category { Id = 2, Name = "sport" },
                new Category { Id = 3, Name = "adventure" }
                );
        }
    }
}
