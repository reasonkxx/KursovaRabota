using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KursovNBA.Models.NBASTATS
{
    public class Game
    {
       
        public int Id { get; set; }

        
        public DateTime Date { get; set; }

       
        public int HomeTeamId { get; set; }

        
        public int HomeTeamScore { get; set; }

        
        public int Season { get; set; }

       
        public int VisitorTeamId { get; set; }

        
        public int VisitorTeamScore { get; set; }
    }
}
