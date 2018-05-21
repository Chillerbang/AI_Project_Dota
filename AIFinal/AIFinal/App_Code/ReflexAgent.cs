using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AIFinal.App_Code
{
    public class ReflexAgent
    {
        public ReflexAgent(string gameID, Emotion[] emotionArray, GameEvents ge)
        {
            // create reflex agent
            //Percepts
            bool winner = (ge.p.isRadiant == ge.p.radiant_win);


            //Condtion Rules


            // actuator (MetadataGeneration)
        }

    

        /*the following fucntion is w weighted sum of game Genral performance,
         * 
         * 
         * 
         * 
         * 
         * 
         */

        public float weightedPerfomanceSumGen(bool winner, int kdaoverTotal, float aveGoldoverTeam, float aveLHOverTotal, float damageOver, float goldEnd)
        {

            if (winner)
            {

            }
            else
            {

            }
        }

        /* this is game performance and emotion weighted
        * 
        * 
        * 
        */



        //final wighting

    }
}