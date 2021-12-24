using System;
using System.Collections.Generic;
using System.Text;

namespace eBroker_App_DataAccessLayer.Repository
{
    public interface IRepository<T> where T : class
    {
        T Add(T obj);

        T Get(int id);

        IEnumerable<T> GetAll();


        T Update(T t, params object[] key);
    }
}
