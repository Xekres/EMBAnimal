using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using Core.Utilities.Results.SuccessOrErrorDataResults;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class OrderDetailManager:IOrderDetailService
    {
        private readonly IOrderDetailDal _orderDetailDal;
        public OrderDetailManager(IOrderDetailDal orderDetailDal)
        {
            _orderDetailDal = orderDetailDal;
        }

        public IResult Add(OrderDetail orderDetail)
        {
            _orderDetailDal.Add(orderDetail);
            return new SuccessResult(Messages.OrderAdded);
        }

        public IResult Delete(int orderDetailId)
        {
            _orderDetailDal.Delete(new OrderDetail { Id = orderDetailId });
            return new SuccessResult(Messages.OrderDeleted);
        }

        public IDataResult<List<OrderDetail>> GetList()
        {
            return new SuccessDataResult<List<OrderDetail>>(_orderDetailDal.GetList().ToList());
        }

        public IResult Update(int orderDetailId)
        {
            _orderDetailDal.Update(new OrderDetail { Id = orderDetailId });
            return new SuccessResult(Messages.OrderUpdated);
        }
    }
}
