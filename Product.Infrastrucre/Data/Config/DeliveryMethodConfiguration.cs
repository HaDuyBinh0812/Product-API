using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.Core.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Infrastrucre.Data.Config
{
    public class DeliveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.Property(x => x.Price).HasColumnType("decimal(18,2)");
            builder.HasData(
                new DeliveryMethod { Id = 1, ShortName="DHL", Desciption="Fastest Delivery",DeliveryTime="", Price =50 },
                new DeliveryMethod { Id = 2, ShortName="Aramex", Desciption="Get It With 3 days",DeliveryTime="", Price =30 },
                new DeliveryMethod { Id = 3, ShortName="Fedex", Desciption="Slower but cheap",DeliveryTime="", Price =20 },
                new DeliveryMethod { Id = 4, ShortName="Jumia", Desciption="Free",DeliveryTime="", Price =0 }
                );
        }
    }
}
