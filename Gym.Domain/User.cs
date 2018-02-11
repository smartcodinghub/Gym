using Gym.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Domain
{
    public class User : Entity
    {
        public String Name { get; set; }
        public String Surnames { get; set; }
        public String Dni { get; set; }
    }
}
