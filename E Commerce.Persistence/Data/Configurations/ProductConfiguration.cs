using E_Commerce.Domain.Entities.ProductModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(propertyExpression: x => x.Name)
                .HasMaxLength(maxLength: 100);

            builder.Property(propertyExpression: x => x.Description)
                .HasMaxLength(maxLength: 500);

            builder.Property(propertyExpression: x => x.PictureUrl)
                .HasMaxLength(maxLength: 200);

            builder.Property(propertyExpression: x => x.Price)
                .HasPrecision(precision: 18, scale: 2);

            builder.HasOne(navigationExpression: x => x.ProductBrand)
                .WithMany()
                .HasForeignKey(foreignKeyExpression: x => x.BrandId);

            builder.HasOne(navigationExpression: x => x.ProductType)
                .WithMany()
                .HasForeignKey(foreignKeyExpression: x => x.TypeId);
        }

        
    }
}
