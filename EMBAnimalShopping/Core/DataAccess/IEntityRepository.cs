
using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess
{
    public interface IEntityRepository<T> where T : class, IEntity,new()
    {
        //DataAccess katmanına hizmet eden ve işlemlerin yapıldıgı yer.
        T Get(Expression<Func<T, bool>> filter);
        IList<T> GetList(Expression<Func<T, bool>> filter = null);
        //filtre boşsa bütün listeyi getir.
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}
