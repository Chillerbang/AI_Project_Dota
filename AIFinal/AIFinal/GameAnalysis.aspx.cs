using System;
using System.Collections.Generic;
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


                //Most Effective Communication
                TableHeaderRow rowTBLPreDiect = new TableHeaderRow();
                tblPredictedWords.Rows.Add(rowTBLPreDiect);
                TableHeaderCell tCellpd = new TableHeaderCell();
                tCellpd.Text = "Most Effective Words";
                // heading of table
                rowTBLPreDiect.Cells.Add(tCellpd);
                //add data
            }

            
            

           


        }
    }
}