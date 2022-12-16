using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using Entities.Concrete.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfBasketDal : EfEntityRepositoryBase<Basket, EmbaContext>, IBasketDal
    {
        public List<BasketDetailDto> GetBasketDetails()
        {
            using (EmbaContext context = new EmbaContext())
            {
                var result = from basket in context.Baskets
                             join product in context.Products
                             on basket.ProductId equals product.Id
                             
                             select new BasketDetailDto
                             {
                                 Id = basket.Id,
                                 ProductName = product.ProductName,
                                 Quantity = basket.Quantity,


                             };
                return result.ToList();
            }
        }
    }
}

