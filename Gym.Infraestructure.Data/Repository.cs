using Gym.Domain.Interfaces;
using NMemory;
using NMemory.Tables;
using System;
using System.Linq;

namespace Gym.Infraestructure.Data
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        protected readonly Database Database;
        protected readonly ITable<T> Table;

        public Repository(GymDatabase db)
        {
            this.Database = db;
            this.Table = (ITable<T>)Database.Tables.FindTable(typeof(T));
        }

        public void Create(T entity)
        {
            Table.Insert(entity);
        }

        public void Delete(int id)
        {
            T entity = Table.FirstOrDefault(e => e.Id == id);
            Table.Delete(entity);
        }

        public T Get(int id)
        {
            return Table.FirstOrDefault(e => e.Id == id);
        }

        public void Update(T entity)
        {
            Table.Update(entity);
        }
    }
}
