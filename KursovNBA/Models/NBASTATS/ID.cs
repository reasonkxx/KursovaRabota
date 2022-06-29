using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KursovNBA.Models.NBASTATS
{
    public class ID
    {
            public int id { get; set; }

            public string first_name { get; set; }
            
            public string last_name { get; set; }
            
            public string position { get; set; }
            
            public Team Team { get; set; }
            
    }
}
