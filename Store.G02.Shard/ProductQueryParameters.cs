using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Shard
{
    public class ProductQueryParameters
    {
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public string? Sort { get; set; }
        public string? Search { get; set; }
        public int PageSize { get; set; } = 6;
        public int PageIndex { get; set; } = 1;
    }
}
