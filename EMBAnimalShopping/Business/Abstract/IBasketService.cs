using Core.Utilities.Results;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IBasketService
    {

        IDataResult<List<Basket>> GetList();
        IDataResult<List<Basket>> GetListByProductId(int productId);
        //IDataResult<List<BasketDetailDto>> GetBasketDetails();
        IResult Add(Basket basket);
        IResult Update(int basketId);
        IResult Delete(int basketId);



    }  
    
        
    
}
