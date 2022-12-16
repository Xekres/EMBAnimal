using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using Core.Utilities.Results.SuccessOrErrorDataResults;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class BasketManager : IBasketService
    {
        private readonly IBasketDal _basketDal;
        
        

        public BasketManager(IBasketDal basketDal)
        {
            _basketDal = basketDal;
            
        }

        public IResult Add(Basket basket)
        {
            _basketDal.Add(basket);
            return new SuccessResult(Messages.BasketItemAdded);
        }

        public IResult Delete(int basketId)
        {

            _basketDal.Delete(new Basket { Id = basketId });
            return new SuccessResult(Messages.BasketItemDeleted);
        }

        public IDataResult<List<Basket>> GetList()
        {
            
            return new SuccessDataResult<List<Basket>>(_basketDal.GetList().ToList());
        }
        //return new SuccessDataResult<List<Product>>(_productDal.GetList().ToList());
        //public IDataResult<List<BasketDetailDto>> GetBasketDetails()
        //{
        //    return new SuccessDataResult<List<BasketDetailDto>>(_basketDal.GetBasketDetails());
        //}



        public IResult Update(int basketId)
        {
            _basketDal.Update(new Basket { Id = basketId });
            return new SuccessResult(Messages.BasketItemUpdated);
        }

        IDataResult<List<Basket>> IBasketService.GetListByProductId(int productId)
        {
            return new SuccessDataResult<List<Basket>>(_basketDal.GetList(p => p.Id == productId).ToList());
        }
    }
}
