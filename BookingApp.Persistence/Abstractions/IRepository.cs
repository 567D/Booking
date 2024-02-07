using System;

namespace BookingApp.Persistence.Abstractions
{
	public interface IRepository<T>
	{
        T GetById(string id);
        IEnumerable<T> GetAll();
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}

