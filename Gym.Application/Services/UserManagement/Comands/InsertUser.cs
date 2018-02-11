using Gym.Domain;
using Gym.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Application.Services.UserManagement
{
    public class InsertUser
    {
        public class InsertUserRequest : IRequest<InsertUserResult>
        {
            public String Name { get; set; }
            public String Surnames { get; set; }
            public String Dni { get; set; }
        }

        public class InsertUserResult
        {
            public int Id { get; set; }
            public String Name { get; set; }
            public String Surnames { get; set; }
            public String Dni { get; set; }

            public InsertUserResult(User user)
            {
                this.Id = user.Id;
                this.Name = user.Name;
                this.Surnames = user.Surnames;
                this.Dni = user.Dni;
            }
        }

        public class InsertUserHandler : IAsyncRequestHandler<InsertUserRequest, InsertUserResult>
        {
            private IRepository<User> repository;

            public InsertUserHandler(IRepository<User> repository)
            {
                this.repository = repository;
            }

            public async Task<InsertUserResult> Handle(InsertUserRequest message)
            {
                User user = new User()
                {
                    Name = message.Name,
                    Surnames = message.Surnames,
                    Dni = message.Dni
                };

                repository.Create(user);

                return new InsertUserResult(user);
            }
        }
    }
}
