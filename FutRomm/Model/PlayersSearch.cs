using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutRomm.Model
{
    public class PlayersSearch
    {
        public class DefendingAttributes
        {
            public String interceptions { get; set; }
            public String headingAccuracy { get; set; }
            public String standingTackle { get; set; }
            public String slidingTackle { get; set; }
            public String defenseAwareness { get; set; }
        }

        public class DribblingAttributes
        {
            public String agility { get; set; }
            public String balance { get; set; }
            public String reactions { get; set; }
            public String ballControl { get; set; }
            public String dribbling { get; set; }
            public String composure { get; set; }
        }

        public class GoalkeeperAttributes
        {
            public int? diving { get; set; }
            public int? handling { get; set; }
            public int? kicking { get; set; }
            public int? positioning { get; set; }
            public int? reflexes { get; set; }
        }

        public class PlayerR
        {
            public String id { get; set; }
            public String resourceId { get; set; }
            public String resourceBaseId { get; set; }
            public String futBinId { get; set; }
            public String futWizId { get; set; }
            public string firstName { get; set; }
            public string lastName { get; set; }
            public string name { get; set; }
            public string commonName { get; set; }
            public String height { get; set; }
            public String weight { get; set; }
            public string birthDate { get; set; }
            public String age { get; set; }
            public String league { get; set; }
            public String nation { get; set; }
            public String club { get; set; }
            public String rarity { get; set; }
            public List<object> traits { get; set; }
            public List<object> specialities { get; set; }
            public string position { get; set; }
            public String skillMoves { get; set; }
            public String weakFoot { get; set; }
            public string foot { get; set; }
            public string attackWorkRate { get; set; }
            public string defenseWorkRate { get; set; }
            public String totalStats { get; set; }
            public String totalStatsInGame { get; set; }
            public string color { get; set; }
            public String rating { get; set; }
            public String ratingAverage { get; set; }
            public String pace { get; set; }
            public String shooting { get; set; }
            public String passing { get; set; }
            public String dribbling { get; set; }
            public String defending { get; set; }
            public String physicality { get; set; }
            public PaceAttributes paceAttributes { get; set; }
            public ShootingAttributes shootingAttributes { get; set; }
            public PassingAttributes passingAttributes { get; set; }
            public DribblingAttributes dribblingAttributes { get; set; }
            public DefendingAttributes defendingAttributes { get; set; }
            public PhysicalityAttributes physicalityAttributes { get; set; }
            public GoalkeeperAttributes goalkeeperAttributes { get; set; }
        }

        public class PaceAttributes
        {
            public String acceleration { get; set; }
            public String sprStringSpeed { get; set; }
        }

        public class Pagination
        {
            public String countCurrent { get; set; }
            public String countTotal { get; set; }
            public String pageCurrent { get; set; }
            public String pageTotal { get; set; }
            public String itemsPerPage { get; set; }
        }

        public class PassingAttributes
        {
            public String vision { get; set; }
            public String crossing { get; set; }
            public String freeKickAccuracy { get; set; }
            public String shortPassing { get; set; }
            public String longPassing { get; set; }
            public String curve { get; set; }
        }

        public class PhysicalityAttributes
        {
            public String jumping { get; set; }
            public String stamina { get; set; }
            public String strength { get; set; }
            public String aggression { get; set; }
        }

        public class PlayersResult
        {
            public Pagination pagination { get; set; }
            public List<PlayerR> items { get; set; }
        }

        public class ShootingAttributes
        {
            public String positioning { get; set; }
            public String finishing { get; set; }
            public String shotPower { get; set; }
            public String longShots { get; set; }
            public String volleys { get; set; }
            public String penalties { get; set; }
        }


    }
}
