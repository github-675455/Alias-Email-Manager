using System.Collections.Generic;
using AliasManager.Models;

namespace AliasManager.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        T Add(T item);
        void Remove(int id);
        void Update(T item);
        T FindByID(int id);
        IList<T> FindAll();
    }
}

