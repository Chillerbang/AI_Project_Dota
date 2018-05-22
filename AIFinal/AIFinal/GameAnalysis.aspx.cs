using AIFinal.App_Code;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AIFinal
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            bool data = true;
            // _______________________________________________ Adding Data _______________________________________________________ //
            //FileInfo[] filesinfo = new DirectoryInfo(Server.MapPath("~") + "audioFiles/Clips/").GetFiles().Where(f => (f.FullName.EndsWith(".wav")) && (f.FullName.Contains(gameID))).ToArray();

            if (data)
            {
                // _______________________________________________ Setting up table Headers _______________________________________________________ //

                // generation of EmotionsTablev -- gives the most emotions in a game
                TableHeaderRow headTopEmotion = new TableHeaderRow();
                tblEmotions.Rows.Add(headTopEmotion);
                TableHeaderCell tCell = new TableHeaderCell();
                TableHeaderCell tCell2 = new TableHeaderCell();
                tCell.Text = "Emotion Prevalent";
                tCell2.Text = "GameID";
                // heading of table
                headTopEmotion.Cells.Add(tCell);
                headTopEmotion.Cells.Add(tCell2);
                //add data
                FileInfo[] filesinfoMeta = new DirectoryInfo(Server.MapPath("~") + "ProcessedGames/").GetFiles().Where(f => (f.FullName.EndsWith(".emote"))).ToArray();
                List<string> gameDisplay = new List<string>();
                List<string> emotDisplay = new List<string>();
                
                foreach (FileInfo f in filesinfoMeta)
                {
                    var most = 0;
                    List<string> Emotions = new List<string>();
                    string[] emote = System.IO.File.ReadAllLines(f.FullName);
                    foreach (string matone in emote)
                    {
                        string[] words = matone.Split(';');
                        try
                        {
                            if (words[1] == "none")
                            {
                            }
                            else
                            {
                                Emotions.Add(words[1]);       
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                    var groupsWithCounts = from s in Emotions group s by s into g select new {Item = g.Key,Count = g.Count()};
                    var groupsSorted = groupsWithCounts.OrderByDescending(g => g.Count);
                    string mostFrequest = groupsSorted.First().Item;
                    // Save Emotion for game
                    string[] justName = f.Name.Split('.');

                    gameDisplay.Add(justName[0]);
                    emotDisplay.Add(mostFrequest);
                }

                // display in table
                int tableindexdisplayEmotioncount = 0;
                foreach (string s in gameDisplay)
                {
                    TableRow row = new TableRow();
                    TableCell cell1 = new TableCell();
                    TableCell cell2 = new TableCell();
                    cell1.Text = gameDisplay.ElementAt(tableindexdisplayEmotioncount);
                    cell2.Text = emotDisplay.ElementAt(tableindexdisplayEmotioncount);
                    row.Cells.Add(cell2);
                    row.Cells.Add(cell1);
                    tblEmotions.Rows.Add(row);
                    tableindexdisplayEmotioncount++;
                }
                



                //Event Table
                TableHeaderRow HeadTopEventEmote = new TableHeaderRow();
                tblGameEvents.Rows.Add(HeadTopEventEmote);
                TableHeaderCell tCellEE = new TableHeaderCell();
                TableHeaderCell tCell2EE = new TableHeaderCell();
                tCellEE.Text = "Event in game &nbsp &nbsp &nbsp &nbsp ";
                tCell2EE.Text = "Emotion";
                // heading of table
                HeadTopEventEmote.Cells.Add(tCellEE);
                HeadTopEventEmote.Cells.Add(tCell2EE);
                //add data

                //Emotion Word Table
                TableHeaderRow HeadWordEmotion = new TableHeaderRow();
                tblEmotionWord.Rows.Add(HeadWordEmotion);
                TableHeaderCell tCellEmote = new TableHeaderCell();
                TableHeaderCell tCellEmote2 = new TableHeaderCell();
                tCellEmote.Text = "Word said in game";
                tCellEmote2.Text = "Emotion";
                // heading of table
                HeadWordEmotion.Cells.Add(tCellEmote);
                HeadWordEmotion.Cells.Add(tCellEmote2);
                //add data

                //FileInfo[] filesinfoMeta2 = new DirectoryInfo(Server.MapPath("~") + "ProcessedGames/").GetFiles().Where(f => (f.FullName.EndsWith(".emote"))).ToArray();
                //List<string> Words = new List<string>();
                //List<string> emotDisplay2 = new List<string>();

                //foreach (FileInfo f in filesinfoMeta)
                //{
                //    var most = 0;
                //    List<string> Emotions = new List<string>();
                //    string[] emote = System.IO.File.ReadAllLines(f.FullName);
                //    foreach (string matone in emote)
                //    {
                //        string[] words = matone.Split(';');
                //        try
                //        {
                //            if (words[1] == "none")
                //            {
                //            }
                //            else
                //            {
                //                Emotions.Add(words[1]);
                //            }
                //        }
                //        catch (Exception ex)
                //        {
                //            throw ex;
                //        }
                //    }
                //    var groupsWithCounts = from s in Emotions group s by s into g select new { Item = g.Key, Count = g.Count() };
                //    var groupsSorted = groupsWithCounts.OrderByDescending(g => g.Count);
                //    string mostFrequest = groupsSorted.First().Item;
                //    // Save Emotion for game
                //    string[] justName = f.Name.Split('.');

                //    gameDisplay.Add(justName[0]);
                //    emotDisplay.Add(mostFrequest);
                //}



                //Most Effective Communication
                TableHeaderRow rowTBLPreDiect = new TableHeaderRow();
                tblPredictedWords.Rows.Add(rowTBLPreDiect);
                TableHeaderCell tCellpd = new TableHeaderCell();
                tCellpd.Text = "Most Effective emotion to play game";
                // heading of table
                rowTBLPreDiect.Cells.Add(tCellpd);
                //add data
                GoalAgent ga = new GoalAgent(Server.MapPath("~"));
                TableRow rowAgent = new TableRow();
                TableCell cellAgent = new TableCell();
                rowAgent.Cells.Add(cellAgent);
                cellAgent.Text = ga.mostFrequent;
                rowAgent.Cells.Add(cellAgent);
                tblPredictedWords.Rows.Add(rowAgent);
                rowTBLPreDiect = new TableHeaderRow();
                tblPredictedWords.Rows.Add(rowTBLPreDiect);
                tCellpd = new TableHeaderCell();
                tCellpd.Text = "The words that improve your gameplay";
                // heading of table
                rowTBLPreDiect.Cells.Add(tCellpd);
                foreach (string s in ga.words)
                {
                    TableRow rowAgent2 = new TableRow();
                    TableCell cellAgent2 = new TableCell();
                    rowAgent2.Cells.Add(cellAgent2);
                    cellAgent2.Text = s;
                    rowAgent2.Cells.Add(cellAgent2);
                    tblPredictedWords.Rows.Add(rowAgent2);
                }
            }

        }
    }
}