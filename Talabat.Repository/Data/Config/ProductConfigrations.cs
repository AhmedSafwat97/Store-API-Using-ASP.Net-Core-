using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Enities;

namespace Talabat.Repository.Data.Config
{
    internal class ProductConfigrations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(P => P.Name).IsRequired()
                                         .HasMaxLength(50);

            builder.Property(P => P.Description)
                                  .IsRequired()
                                  .HasMaxLength(100);

            builder.Property(P => P.PictureUrl).IsRequired();


            builder.Property(P => P.Price).HasColumnType("decimal(18 , 3)");



            //the Navigational Property  For Brand
            builder.HasOne(P => P.Brand).WithMany()
                .HasForeignKey(P => P.BrandId);


            //the Navigational Property  For Category
            builder.HasOne(P => P.Category)
                .WithMany().HasForeignKey(P => P.CategoryId);




        }

    }
}
