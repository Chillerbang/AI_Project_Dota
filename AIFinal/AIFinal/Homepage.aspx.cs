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

        // ________________________________ END Not my Code ________________________________ //

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
            int playerID = 203422649;
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
                    
                    foreach (var o in oj)
                    {
                        GameEvents.objectives oTemp = new GameEvents.objectives();
                        oTemp.time = o.time;
                        oTemp.who = o.type;
                        oTemp.type = o.key;
                        gi.o.Add(oTemp);
                    }

                    JArray jPlayer = JArray.Parse(JsoncClass.players.ToString()) as JArray;
                    dynamic joP = jPlayer;
                    
                    foreach (var p in joP)
                    {
                        GameEvents.Player oPTemp = new GameEvents.Player();
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
                            oPTemp.radiant_win = p.radiant_win;
                            oPTemp.isRadiant = p.isRadiant;
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
                                if (audiolength > durationGame)
                                {
                                    int CutStart = audiolength - durationGame;
                                    TimeSpan tCutStart = TimeSpan.FromSeconds(audiolength - durationGame);
                                    TimeSpan tCutEnd = TimeSpan.FromSeconds(0);
                                    TrimWavFile(Server.MapPath("~") + "audioFiles/FullFile/" + gameID + ".wav", Server.MapPath("~") + "audioFiles/FullFile/TR" + gameID + ".wav", tCutStart, tCutEnd);
                                }

                                //saving trunced audio
                                int counter = 0;
                                WaveFileReader wvTr = new WaveFileReader(Server.MapPath("~") + "audioFiles/FullFile/TR" + gameID + ".wav");
                                TimeSpan lenghtAudioTr = wvTr.TotalTime;
                                TimeSpan CutBeginXlip;
                                TimeSpan CutEndClip;
                                foreach (GameEvents.objectives objMusic in gi.o)
                                {
                                    if (objMusic.time > 5)
                                    {
                                        CutBeginXlip = TimeSpan.FromSeconds((double)objMusic.time - 5);
                                        CutEndClip = lenghtAudioTr - (CutBeginXlip + TimeSpan.FromSeconds(5));

                                        //save batch mp3 subfiles
                                        TrimWavFile(Server.MapPath("~") + "audioFiles/FullFile/TR" + gameID + ".wav", Server.MapPath("~") + "audioFiles/Clips/" + gameID + "-" + counter + ".wav", CutBeginXlip, CutEndClip);
                                        counter++;
                                    }
                                }

                                //--- Linking emotion to audioFile for agent metadata using IBM whatson read all music files 
                                FileInfo[] filesinfo = new DirectoryInfo(Server.MapPath("~") + "audioFiles/Clips/").GetFiles().Where(f => (f.FullName.EndsWith(".wav")) && (f.FullName.Contains(gameID))).ToArray();

                                //  Create emotion for each
                                Emotion[] eMotionsDetected = new Emotion[filesinfo.Length];
                                int countEmotionIndex = 0;
                                foreach (FileInfo f in filesinfo)
                                {

                                    //Set DataFrom IBM -- stoped working? why
                                    eMotionsDetected[countEmotionIndex] = new Emotion();

                                    //save data
                                    eMotionsDetected[countEmotionIndex].Mp3ParentName = f.Name;
                                    eMotionsDetected[countEmotionIndex].emotionDetected = "EDetctedPlace";
                                    eMotionsDetected[countEmotionIndex].intensity = -99;
                                    //eMotionsDetected[countEmotionIndex].delta = -99;
                                    eMotionsDetected[countEmotionIndex].ArrayWords = null;
                                    countEmotionIndex++;
                                }
                                string[] EmotionLine = new string[eMotionsDetected.Length];
                                int countEmotionWrite = 0;
                                foreach (Emotion wrtieEmotionFile in eMotionsDetected)
                                {
                                    string AllwordSaid = "";
                                    if (eMotionsDetected[countEmotionWrite].ArrayWords != null)
                                    {
                                        foreach (string word in eMotionsDetected[countEmotionWrite].ArrayWords)
                                        {
                                            AllwordSaid += word + "*";
                                        }
                                    }
                                    
                                    EmotionLine[countEmotionWrite] = eMotionsDetected[countEmotionWrite].Mp3ParentName + ";" + eMotionsDetected[countEmotionWrite].emotionDetected + ";" + eMotionsDetected[countEmotionWrite].intensity + ";" + AllwordSaid + ";";
                                    countEmotionWrite++;
                                }
                                test.InnerHtml += "<font color=\"green\"> Data Recorded successfully";
                                lblRedirect.InnerHtml = "<a href =\"GameAnalysis.aspx\">See Results<a>";
                                //File.WriteAllLines(Server.MapPath("~") + "ProcessedGames/" + gameID + ".emote", EmotionLine);
                                ReflexAgent reflex = new ReflexAgent(gameID, gi, Server.MapPath("~") + "ProcessedGames/" + gameID + ".emote", Server.MapPath("~") + "ProcessedGames/");
                            }
                            else
                            {
                                test.InnerHtml += "No WMA audio file";
                            }
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
            test.InnerHtml += "</font>";
            
        }
    }
}