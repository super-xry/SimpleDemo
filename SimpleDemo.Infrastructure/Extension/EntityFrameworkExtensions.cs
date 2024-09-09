using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace SimpleDemo.Infrastructure.Extension
{
    public static class EntityFrameworkExtensions
    {
        public static void BuildGuid(this ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entity.ClrType.GetProperties())
                {
                    if (property.PropertyType == typeof(Guid))
                    {
                        modelBuilder.Entity(entity.Name)
                            .Property(property.Name)
                            .ValueGeneratedNever()
                            .HasConversion(new ValueConverter<Guid, string>(
                                v => v.ToString("N"),
                                v => Guid.Parse(v)));
                    }
                }
            }
        }
    }
}