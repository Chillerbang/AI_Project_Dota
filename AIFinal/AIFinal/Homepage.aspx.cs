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
using System.IO;
using NAudio.Wave;

namespace AIFinal
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        // ________________________________ Not my Code ________________________________ //

        public static void TrimWavFile(string inPath, string outPath, TimeSpan cutFromStart, TimeSpan cutFromEnd)
        {
            using (WaveFileReader reader = new WaveFileReader(inPath))
            {
                using (WaveFileWriter writer = new WaveFileWriter(outPath, reader.WaveFormat))
                {
                    int bytesPerMillisecond = reader.WaveFormat.AverageBytesPerSecond / 1000;

                    int startPos = (int)cutFromStart.TotalMilliseconds * bytesPerMillisecond;
                    startPos = startPos - startPos % reader.WaveFormat.BlockAlign;

                    int endBytes = (int)cutFromEnd.TotalMilliseconds * bytesPerMillisecond;
                    endBytes = endBytes - endBytes % reader.WaveFormat.BlockAlign;
                    int endPos = (int)reader.Length - endBytes;

                    TrimWavFile(reader, writer, startPos, endPos);
                }
            }
        }

        private static void TrimWavFile(WaveFileReader reader, WaveFileWriter writer, int startPos, int endPos)
        {
            reader.Position = startPos;
            byte[] buffer = new byte[1024];
            while (reader.Position < endPos)
            {
                int bytesRequired = (int)(endPos - reader.Position);
                if (bytesRequired > 0)
                {
                    int bytesToRead = Math.Min(bytesRequired, buffer.Length);
                    int bytesRead = reader.Read(buffer, 0, bytesToRead);
                    if (bytesRead > 0)
                    {
                        writer.WriteData(buffer, 0, bytesRead);
                    }
                }
            }
        }

        // ________________________________ Not my Code ________________________________ //

        private static void SaveWaveFile(WaveFileReader reader, string outPath)
        {

        }

            protected void Page_Load(object sender, EventArgs e)
        {
            test.InnerHtml += "<font color=\"red\">";
        }

        protected void btnAddGame_Click(object sender, EventArgs e)
        {
            bool baudio = false;
            bool bJson = false;
            bool bIDgame = false;
            bool bplayerID = false;
            int playerID = 96166915;
            string strJson = "";
            bool gameIDTrue = false;
            bool playerIdTrue = false;
            test.InnerText = "";
            if (GameIDInput.Value != "")
            {
                try
                {
                    string gameID = GameIDInput.Value;
                    gameIDTrue = true;
                    var json = new WebClient().DownloadString("https://api.opendota.com/api/matches/" + gameID);
                    strJson = json.ToString();
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

                    // write Json
                    string gameIDWrite = GameIDInput.Value;
                    string JsonPath = HttpContext.Current.Server.MapPath("/JsonData");
                    File.WriteAllText(JsonPath + '\\' + gameIDWrite + ".json", strJson);
                    bJson = true;

                    // trimming that sweet audio 
                    // if the game data is real then we can prep the audio
                    if (gameIDTrue && playerIdTrue)
                    {
                        if (uploadAudioFile.HasFile)
                        {
                            string[] token = uploadAudioFile.FileName.Split('.');
                            string Extension = token[token.Length - 1];
                            if (Extension == "mp3")
                            {
                                baudio = true;

                                //getting mp3 to match game time
                                int durationGame = gi.md.duration;
                                uploadAudioFile.SaveAs(Server.MapPath("~") + "audioFiles/FullFile/" + gameID + ".mp3");
                                var inputStream = new FileStream(Server.MapPath("~") + "audioFiles/FullFile/" + gameID + ".mp3", FileMode.Open, FileAccess.Read, FileShare.None);

                                Mp3FileReader read = new Mp3FileReader(inputStream);
                                WaveFileWriter.CreateWaveFile(Server.MapPath("~") + "audioFiles/FullFile/" + gameID + ".wav", read);

                                //convert mp3 to wave
                                WaveFileReader wv = new WaveFileReader(Server.MapPath("~") + "audioFiles/FullFile/" + gameID + ".wav");
                                TimeSpan lenghtAudio = wv.TotalTime;
                                int audiolength = (int)Math.Round(lenghtAudio.TotalSeconds);

                                // cut begniing 

                                foreach (GameEvents.objectives objMusic in gi.o)
                                {
                                    
                                    //public static void TrimWavFile(string inPath, string outPath, TimeSpan cutFromStart, TimeSpan cutFromEnd)
                                    //TrimWavFile(Server.MapPath("~") + "audioFiles/FullFile/" + gameID + ".wma", Server.MapPath("~") + "audioFiles/Clips/" + gameID + "/" + objMusic.time + ".wma", objMusic.time + , );

                                    // cut 5 second clip before and after start team engagement



                                }
                                //save batch mp3 subfiles
                            }
                        }
                        else
                        {
                            test.InnerText += "No WMA audio file";
                        }
                    }


                }
                catch(WebException ex)
                {
                    if (gameIDTrue == false)
                    test.InnerHtml += "ERROR. invalid game ID";
                    else
                    {
                        if (playerIdTrue== false)
                        {
                            test.InnerHtml += "ERROR. invalid player ID" ;
                        }
                    }
                }
            }
            
            test.InnerText += "</font>";
            if (baudio && bJson && bIDgame)
            {
                test.InnerHtml = "<font color=\"greed\"> Agent Fired UP </font>";
                // write joson as file
                //start doing operations for program

                //Get teamfights

                //

            }
        }
    }
}