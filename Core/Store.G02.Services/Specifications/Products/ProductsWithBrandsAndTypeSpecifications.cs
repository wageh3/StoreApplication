using Store.G02.Domain.Entities.Products;
using Store.G02.Shard;
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
        public ProductsWithBrandsAndTypeSpecifications(ProductQueryParameters parameters) : base
            (
                P=> 
                (!parameters.BrandId.HasValue || P.BrandId == parameters.BrandId) 
                &&
                (!parameters.TypeId.HasValue || P.TypeId == parameters.TypeId)
                &&
                (string.IsNullOrEmpty(parameters.Search) || P.Name.ToLower().Contains(parameters.Search.ToLower()))
            )
        {
            ApplyPagination(parameters.PageSize, parameters.PageIndex);
            ApplySorting(parameters.Sort);

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
