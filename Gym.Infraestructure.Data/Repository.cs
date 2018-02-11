using NMemory;
using NMemory.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Infraestructure.Data
{
    public class Repository<T> where T : class
    {
        protected readonly Database Database;
        protected readonly ITable<T> Table;

        public Repository(GymDatabase db)
        {
            this.Database = db;
            this.Table = (ITable<T>)Database.Tables.FindTable(typeof(T));
        }

        public void Insert(T entity)
        {
            this.Table.Insert(entity);
        }
    }
}
