using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace DataAccessLayer.Interfaces
{
    public interface IRepository<T> where T : class
    { 
        IEnumerable<T> GetAll();
        T GetItem(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);

    }
}
