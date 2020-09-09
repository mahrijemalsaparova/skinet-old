using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {// parametre olarak gelen product'ın özellikleri aşagıdaki şekilde olucak
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Description).IsRequired().HasMaxLength(180);
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
            builder.Property(p => p.PictureUrl).IsRequired();

            //parametre olarak gelen  her bir product'ın tek bir ProductBrand'ı vardır
            builder.HasOne(b => b.ProductBrand).WithMany()
            // ve bu pruduct tablosunun içinde bir ProductBrandId foreignkey tanımla
                .HasForeignKey(p => p.ProductBrandId);
            // Aynı şartlar ProductType içinde yaptık
            builder.HasOne(t => t.ProductType).WithMany()
                .HasForeignKey(p => p.ProductTypeId);

// bunları yaptıktan sonra StoreContext'e gidip bildirmemiz gerekiyor bu şartları
        }
    }
}