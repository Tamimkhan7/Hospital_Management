using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        //object GenericRepository<T>();
        IGenericRepository<T> GetRepository<T>() where T : class;
        void Save();
    }
}
