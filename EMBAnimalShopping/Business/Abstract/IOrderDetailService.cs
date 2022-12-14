using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IOrderDetailService
    {
        IDataResult<List<OrderDetail>> GetList();

        IResult Add(OrderDetail orderDetail);
        IResult Delete(int orderDetailId);
        IResult Update(int orderDetailId);
    }
}
