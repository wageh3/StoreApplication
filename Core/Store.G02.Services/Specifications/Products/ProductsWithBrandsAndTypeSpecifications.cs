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
        public ProductsWithBrandsAndTypeSpecifications(int? BrandId, int? TypeId, string? sort, string? search) : base
            (
                P=> 
                (!BrandId.HasValue || P.BrandId == BrandId) 
                &&
                (!TypeId.HasValue || P.TypeId == TypeId)
                &&
                (string.IsNullOrEmpty(search) || P.Name.ToLower().Contains(search.ToLower()))
            )
        {
           ApplySorting(sort);

            ApplyIncludes();
        }

        private void ApplySorting(string? sort)
        {
            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort.ToLower())
                {
                    case "priceasc":
                        AddOrderBy(P => P.Price);
                        break;
                    case "pricedesc":
                        AddOrderByDescending(P => P.Price);
                        break;
                    default:
                        AddOrderBy(P => P.Name);
                        break;
                }
            }
            else
            {
                AddOrderBy(P => P.Name);
            }
        }
        private void ApplyIncludes()
        {
            Includes.Add(p => p.Brand);
            Includes.Add(p => p.Type);
        }

    }
}
