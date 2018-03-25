using System.Collections.Generic;

namespace Synonyms.DataAccess.Interfaces
{
    public interface IRepository<T>
    {
        IList<T> GetAll();
        void Insert(T item);
    }
}
