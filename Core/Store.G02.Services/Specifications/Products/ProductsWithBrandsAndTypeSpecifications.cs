using Store.G02.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Services.Specifications.Products
{
    public class ProductsWithBrandsAndTypeSpecifications : BaseSpecificatios<int, Product>
    {
        public ProductsWithBrandsAndTypeSpecifications(int id) : base(P => P.Id == id)
        {
            ApplyIncludes();
        }
        public ProductsWithBrandsAndTypeSpecifications(int? BrandId, int? TypeId) : base
            (
                P=> 
                (!BrandId.HasValue || P.BrandId == BrandId) 
                &&
                (!TypeId.HasValue || P.TypeId == TypeId)
            )
        {
            ApplyIncludes();
        }
        private void ApplyIncludes()
        {
            Includes.Add(p => p.Brand);
            Includes.Add(p => p.Type);
        }

    }
}
