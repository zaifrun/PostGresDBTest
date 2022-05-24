using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostGresDBTest
{
    public class Actor
    {
        public string ActorID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public DateTime LastUpdate { get; set; }

        public Actor()
        {
           

        }
    }
}
