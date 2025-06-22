using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Core.Dto
{
    public class OrderDto
    {
        public string BasketId { set; get; }
        public int DeliveryMethodId { set; get; }

        public AddressDto ShipToAddress { set; get; }
    }
}
