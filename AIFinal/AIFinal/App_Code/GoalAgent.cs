using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace AIFinal.App_Code
{


    public class GoalAgent
    {
        public string mostFrequent = "";
        public List<string> words;

        //stocastic hill climb search
        public GoalAgent(string ServerPath)
        {
            // getting evaulation data
            FileInfo[] filesinfoMeta = new DirectoryInfo(ServerPath + "ProcessedGames/").GetFiles().Where(f => (f.FullName.EndsWith(".meta"))).ToArray();
            FileInfo[] filesInfoEmote = new DirectoryInfo(ServerPath + "ProcessedGames/").GetFiles().Where(f => (f.FullName.EndsWith(".emote"))).ToArray();
            Node LastNode = null;
            
            // create list
            list li = new list();
            // add nodes
            int count = 0;
            foreach (FileInfo f in filesInfoEmote)
            {
                Emotion[] emotionArray;
                string[] matarry = f.Name.Split('.');
                string matchID = matarry[0];
                string[] meta = File.ReadAllLines(filesinfoMeta[count].FullName);
                metaDataGen md = new metaDataGen(filesinfoMeta[count].FullName);
                string[] emote = File.ReadAllLines(f.FullName);
                emotionArray = new Emotion[emote.Length];
                int countArryIndexEmote = 0;
                foreach (string e in emote)
                {
                    emotionArray[countArryIndexEmote] = new Emotion();
                    string[] words = e.Split(';');
                    try
                    {
                        if (words[1] == "none")
                        {
                            emotionArray[countArryIndexEmote].ArrayWords = null;
                            emotionArray[countArryIndexEmote].intensity = 0;
                            emotionArray[countArryIndexEmote].emotionDetected = "none";
                            emotionArray[countArryIndexEmote].Mp3ParentName = words[0];
                        }
                        else
                        {
                            emotionArray[countArryIndexEmote].ArrayWords = words[3].Split('*');
                            double temp = 0;
                            string Intense = words[2].Replace('.', ',');
                            if (double.TryParse(Intense, out temp))

                                emotionArray[countArryIndexEmote].intensity = temp;
                            emotionArray[countArryIndexEmote].emotionDetected = words[1];
                            emotionArray[countArryIndexEmote].Mp3ParentName = words[0];
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    countArryIndexEmote++;
                }

                //create and add node
                Node ntemp = new Node(matchID, emotionArray, md);
                count++;
                if (count == filesinfoMeta.Length)
                {
                    LastNode = ntemp;
                }
                li.add(ntemp);
            }



            // run hill climb
            if (LastNode != null)
            {
                Node Final = li.DoHillClimb(LastNode);

                //get most emotions
                var most = 0;
                Emotion[] arrymotionBest;
                if (Final.eArray != null)
                {

                    arrymotionBest = Final.eArray;
                    List<string> strEmotion = new List<string>();
                    foreach (Emotion emotion in arrymotionBest)
                    {
                        if (emotion.emotionDetected != "none")
                        {

                            strEmotion.Add(emotion.emotionDetected);

                        }


                    }
                    var groupsCounts = from s in strEmotion group s by s into g select new { Item = g.Key, Count = g.Count() };
                    var groupsSorted = groupsCounts.OrderByDescending(g => g.Count);
                    mostFrequent = groupsSorted.First().Item;
                    words = new List<string>();
                    foreach (Emotion emotion in arrymotionBest)
                    {
                        if (emotion.emotionDetected == mostFrequent)
                        {
                            // save those words!
                            foreach (string s in emotion.ArrayWords)
                            {
                                string[] strarray = s.Split('*');
                                foreach (string slist in strarray)
                                {
                                    words.Add(slist);
                                }
                            }
                            
                        }
                    }
                }
            }
        }

            

    }
}