using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AIFinal.App_Code;
using System.Net;

namespace AIFinal
{
    public partial class WebForm1 : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAddGame_Click(object sender, EventArgs e)
        {
            bool audio = false;
            bool Json = false;
            bool ID = false;
            int playerID = 101680545;

            bool gameIDTrue = false;
            bool playerIdTrue = false;

            if (GameIDInput.Value != "")
            {
                try
                {
                    string gameID = GameIDInput.Value;
                    gameIDTrue = true;
                    var json = new WebClient().DownloadString("https://api.opendota.com/api/matches/" + gameID);
                    test.InnerText = json.ToString();
                    var JsoncClass = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(json);
                    GameEvents gi = new GameEvents();
                    string temp = JsoncClass.match_id;
                    gi.md.match_id = double.Parse(temp);
                    gi.md.barracks_status_dire = JsoncClass.barracks_status_dire;
                    gi.md.barracks_status_radiant = JsoncClass.barracks_status_radiant;
                    gi.md.dire_score = JsoncClass.dire_score;
                    gi.md.duration = JsoncClass.duration;
                    gi.md.radiant_score = JsoncClass.radiant_score;
                    gi.md.radiant_win = JsoncClass.radiant_win;
                    gi.md.start_time = JsoncClass.start_time;
                    gi.md.radiant_score = JsoncClass.radiant_score;
                    JArray jObj = JArray.Parse(JsoncClass.objectives.ToString()) as JArray;
                    dynamic oj = jObj;
                    GameEvents.objectives oTemp = new GameEvents.objectives();
                    foreach (dynamic o in oj)
                    {
                        oTemp.time = o.time;
                        oTemp.who = o.type;
                        oTemp.type = o.key;
                        gi.o.Add(oTemp);
                    }
                    JArray jPlayer = JArray.Parse(JsoncClass.players.ToString()) as JArray;
                    dynamic joP = jPlayer;
                    GameEvents.Player oPTemp = new GameEvents.Player();
                    foreach (dynamic p in joP)
                    {
                        if (p.account_id == playerID)
                        {
                            playerIdTrue = true;
                            oPTemp.account_id = p.account_id;
                            oPTemp.deaths = p.deaths;
                            oPTemp.denies = p.denies;
                            oPTemp.gold = p.gold;
                            oPTemp.gold_per_min = p.gold_per_min;
                            oPTemp.hero_damage = p.hero_damage;
                            oPTemp.hero_id = p.hero_id;
                            oPTemp.kda = p.kda;
                            oPTemp.kills = p.kills;
                            oPTemp.kills_per_min = p.kills_per_min;
                            oPTemp.last_hits = p.last_hits;
                            oPTemp.total_gold = p.total_gold;
                            oPTemp.total_xp = p.total_xp;
                            oPTemp.lane_efficiency = p.lane_efficiency;
                            oPTemp.personaname = p.personaname;
                            // per minute data
                            JArray goldTime = JArray.Parse(p.gold_t.ToString()) as JArray;
                            JArray lhTime = JArray.Parse(p.lh_t.ToString()) as JArray;
                            JArray denieTime = JArray.Parse(p.dn_t.ToString()) as JArray;
                            //storing data
                            oPTemp.gold_t = goldTime.Select(jv => (int)jv).ToArray();
                            oPTemp.lh_t = lhTime.Select(jv => (int)jv).ToArray();
                            oPTemp.dn_t = denieTime.Select(jv => (int)jv).ToArray();
                            gi.p = oPTemp;
                        }
                    }
                    
                }catch(WebException ex)
                {
                    if (gameIDTrue == false)
                    test.InnerHtml = "<font color= \"red\">" + "ERROR. invalid game ID" + "</font>";
                    else
                    {
                        if (playerIdTrue== false)
                        {
                            test.InnerHtml = "<font color= \"red\">" + "ERROR. invalid player ID" + "</font>";
                        }
                    }
                }
            }
            // if the game data is real then we can prep the audio
            if (gameIDTrue && playerIdTrue)
            {
                if (uploadAudioFile.HasFile)
                {
                    string[] token = uploadAudioFile.FileName.Split('.');
                    string Extension = token[token.Length - 1];
                    if (Extension == "mp3")
                    {
                        audio = true;
                        // perform processing for MP3

                    }
                }
            }

            //if (uploadAudioFile.HasFile)
            //{
            //    string[] token = uploadAudioFile.FileName.Split('.');
            //    string Extension = token[token.Length - 1];
            //    if (Extension == "wma")
            //    {
            //        audio = true;
            //    }
            //}

            //test.InnerText = "";
            //if (audio)
            //{
            //    test.InnerText = "No audiio file";
            //}

            //if (Json)
            //{
            //    test.InnerText = "No Json file";
            //}

            //if (ID)
            //{
            //    test.InnerText = "No Json file";
            //}

            if (audio && Json && ID)
            {
                uploadAudioFile.SaveAs(Server.MapPath("~") + "audioFiles\\" + ID);
                // write joson as file
                //start doing operations for program

                //Get teamfights

                //

            }
        }
    }
}