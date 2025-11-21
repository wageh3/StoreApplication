using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.G02.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Persistance.Data.Configurations
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(P => P.Name).HasColumnType("Varchar").HasMaxLength(256);
            builder.Property(P => P.Description).HasColumnType("Varchar").HasMaxLength(512);
            builder.Property(P => P.PictureUrl).HasColumnType("Varchar").HasMaxLength(256);
            builder.Property(P => P.Price).HasColumnType("decimal(18,2)");

            builder.HasOne(P => P.Brand)
                .WithMany()
                .HasForeignKey(P => P.BrandId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(P => P.Type)
                .WithMany()
                .HasForeignKey(P => P.TypeId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
