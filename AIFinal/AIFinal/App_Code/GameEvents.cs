using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AIFinal.App_Code
{
    public class GameEvents
    {
        public MatchData md;
        public Player p;
        public List<objectives> o;

        public GameEvents()
        {
            md = new MatchData();
            p = new Player();
            o = new List<objectives>();
        }

        public class MatchData
        {
            public double match_id { get; set; }
            public int barracks_status_dire { get; set; }
            public int barracks_status_radiant { get; set; }
            public int dire_score { get; set; }
            public int duration { get; set; }
            public int radiant_score { get; set; }
            public bool radiant_win { get; set; }
            public int start_time { get; set; }
        }

        public class objectives
        {
            public int time { get; set; }
            public string who { get; set; }
            public string type { get; set; }
        }

        public class Player
        {
            public int account_id { get; set; }
            public int deaths { get; set; }
            public int denies { get; set; }
            public int[] dn_t { get; set; }
            public int gold { get; set; }
            public int gold_per_min { get; set; }
            public int[] gold_t { get; set; }
            public string personaname { get; set; }
            public int hero_damage { get; set; }
            public int hero_id { get; set; }
            public int kills { get; set; }
            public float lane_efficiency { get; set; }
            public int last_hits { get; set; }
            public int[] lh_t { get; set; }
            public int duration { get; set; }
            public int total_gold { get; set; }
            public int total_xp { get; set; }
            public float kills_per_min { get; set; }
            public int kda { get; set; }
        }


    }
}