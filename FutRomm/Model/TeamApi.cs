using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutRomm.Model
{
    public class TeamApi
    {
        public class TeamR
        {
            public int id { get; set; }
            public string name { get; set; }
            public string league { get; set; }
        }

        public class Pagination
        {
            public int countCurrent { get; set; }
            public int countTotal { get; set; }
            public int pageCurrent { get; set; }
            public int pageTotal { get; set; }
            public int itemsPerPage { get; set; }
        }

        public class TeamsResult
        {
            public Pagination pagination { get; set; }
            public List<TeamR> items { get; set; }
        }

    }
}
