using Gym.Domain;
using Gym.Infraestructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Gym.Site.Controllers
{
    public class UserController : ApiController
    {
        public User Post([FromBody]User user)
        {
            Repository<User> repo = new Repository<User>(new GymDatabase());
            repo.Insert(user);

            return user;
        }
    }
}
