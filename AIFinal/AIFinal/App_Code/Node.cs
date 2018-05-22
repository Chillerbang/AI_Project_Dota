using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AIFinal.App_Code
{

    public class Node
    {
        string MatchID;
        public Emotion[] eArray;
        public metaDataGen mdgen;
        public string MatID;

        public Node(string MatchID, Emotion[] emote, metaDataGen mdg)
        {
            this.MatchID = MatchID;
            eArray = emote;
            mdgen = mdg;
        }

    }
}