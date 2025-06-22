using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Core.Entities
{
    public class CustomerBasket
    {
        public CustomerBasket() { }
        public CustomerBasket(string Id)
        {
            Id = Id;
        }
        public string Id { get; set; }
        public List<BasketItem> BasketItems { set; get; } = new List<BasketItem>();

    }
}
