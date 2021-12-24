using System;
using System.Collections.Generic;
using System.Text;

namespace eBroker_App_DataAccessLayer.Repository
{

   
    public class RepositoryImplementation<T> : IRepository<T> where T: class
    {
        private TraderEquityDBContext _traderEquityDBContext;
        public RepositoryImplementation(TraderEquityDBContext traderEquityDBContext)
        {
            _traderEquityDBContext = traderEquityDBContext;
        }

        public T Add(T obj)
        {
            _traderEquityDBContext.Set<T>().Add(obj);
            _traderEquityDBContext.SaveChanges();
            return obj;
        }

        public T Get(int id)
        {
            return _traderEquityDBContext.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _traderEquityDBContext.Set<T>();
        }

        public T Update(T t, params object[] key)
        {
            if (t == null)
            {
                return null;
            }

            T exist = _traderEquityDBContext.Set<T>().Find(key);
            if (exist != null)
            {
                _traderEquityDBContext.Entry(exist).CurrentValues.SetValues(t);
            }
            _traderEquityDBContext.SaveChanges();
            return exist;
        }

      
    }
}
