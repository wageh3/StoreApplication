using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Shard.Dtos.Baskets
{
    public class BasketResponce
    {
        public string Id { get; set; }
        public List<BasketItemResponce> Items { get; set; }
    }
}
