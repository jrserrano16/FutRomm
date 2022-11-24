using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutRomm.Model
{
    public class Player
    {
        public int id { get; set; }
        public String name { get; set; }
        public int age { get; set; }
        public String photo { get; set; }
        public String nation { get; set; }
        public int nation_id { get; set; }
        public String nation_logo { get; set; }
        public String club { get; set; }
        public int club_id { get; set; }
        public String club_logo { get; set; }
        public String league { get; set; }
        public int league_id { get; set; }
        public String league_logo { get; set; }
        public String foot { get; set; }
        public String position { get; set; }
    }
}
