using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using Entities.Concrete.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : EfEntityRepositoryBase<Product, EmbaContext>, IProductDal
    {
        public List<ProductDetailDto> GetProductDetails()
        {
            using (EmbaContext context = new EmbaContext())
            {
                var result = from p in context.Products
                             join c in context.Categories
                             on p.CategoryId equals c.Id
                             join b in context.Brands
                             on p.BrandId equals b.BrandId

                             select new ProductDetailDto
                             {
                                 Id = p.Id,

                                 CategoryName = c.CategoryName,
                                 BrandName = b.BrandName,
                                 ProductName = p.ProductName,

                                 UnitPrice = p.UnitPrice,

                                 ProductImageUrl=p.ProductImageUrl
                                 


                             };
                return result.ToList();

            }
        }

        public ProductDetailDto GetProductDetails(int productId)
        {
            using (EmbaContext context=new EmbaContext())
            {
                var result = from product in context.Products.Where(p => p.Id == productId)

                             join brand in context.Brands
                             on product.BrandId equals brand.BrandId

                             join category in context.Categories
                             on product.CategoryId equals category.Id

                             select new ProductDetailDto()
                             {
                                 Id = product.Id,
                                 BrandName = brand.BrandName,
                                 CategoryName = category.CategoryName,
                                 StockAmounth = product.StockAmount,
                                 UnitPrice = product.UnitPrice,
                                 ProductImageUrl = product.ProductImageUrl,
                                 ProductName = product.ProductName

                             };
                return result.SingleOrDefault();

            }
        }
    }
}
