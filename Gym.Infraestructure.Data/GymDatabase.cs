using Gym.Domain;
using NMemory;
using NMemory.Indexes;
using NMemory.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Infraestructure.Data
{
    public class GymDatabase : Database
    {
        public ITable<User> Users { get; }

        public GymDatabase()
        {
            Table<User, int> users = this.Tables.Create(u => u.Id, new IdentitySpecification<User>(p => p.Id));
            users.CreateUniqueIndex(new RedBlackTreeIndexFactory(), u => u.Dni);

            this.Users = users;
        }
    }
}
